using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
	[SerializeField] private QuestTrackerUi _questTrackerUi = null;

	[SerializeField] private List<Quest> _activeQuests;

	private void Awake()
	{
		_activeQuests = new List<Quest>();
	}

	public void AddQuest(Quest quest)
	{
		_questTrackerUi.AddQuest(quest);

		quest.NpcQuestHolder.OnQuestAccepted();
		_activeQuests.Add(quest);

		RegisterInventoryChange(null, 0);  // This just causes us to check if we already meet the requirements.
	}

	public void RemQuest(Quest quest, bool wasTurnedIn, IEnumerable<SlotInfo> rewards = null)
	{
		_questTrackerUi.RemQuest(quest);

		if (wasTurnedIn)
		{
			quest.NpcQuestHolder.OnQuestTurnedIn();
			RemoveRequiredItemsFromPlayersInventory(quest);
			AddRewardsToPlayersInventory(rewards);

			// NOTE: For now this is good. Put this inside of both loops for Add/RemoveItemsFromPlayersInventory if the 
			// Objective.RegisterInventoryChange doesn't work directly with the player's inventory.
			RegisterInventoryChange(null, 0);
		}
		else
		{
			quest.NpcQuestHolder.OnQuestAbandoned();
		}
		_activeQuests.Remove(quest);
	}

	private void RemoveRequiredItemsFromPlayersInventory(Quest quest)
	{
		var objectiveItems = quest.Objectives
			.Where(x => (x as FindObjective) != null)
			.Select(x => (FindObjective)x);
		
		var inventory = transform.GetComponent<Inventory>();
		if (inventory)
		{
			foreach (var objective in objectiveItems)
			{
				if (!objective.KeepItemsAfterCompleted)
				{ 
					inventory.RemItem(objective.Item, objective.NumItemsToFind);
				}
			}
		}
		else
		{
			Debug.LogError("QuestTracker could not find player's Inventory component. They should both be on the root of the player game object.");
		}
	}

	private void AddRewardsToPlayersInventory(IEnumerable<SlotInfo> rewards)
	{
		var inventory = transform.GetComponent<Inventory>();
		if (inventory)
		{
			foreach (var reward in rewards)
			{
				inventory.AddItem(reward.Item, reward.Quantity);
			}
		}
		else
		{
			Debug.LogError("QuestTracker could not find player's Inventory component. They should both be on the root of the player game object.");
		}
	}

	public void RegisterKill(string enemyName)
	{
		RegisterEvent(x => x.RegisterKill(enemyName));
	}

	public void RegisterInventoryChange(string itemName, int amount)
	{
		RegisterEvent(x => x.RegisterInventoryChange(itemName, amount));
	}

	private void RegisterEvent(Action<Objective> f)
	{
		foreach (var quest in _activeQuests)
		{
			foreach (var objective in quest.Objectives)
			{
				f?.Invoke(objective);

				_questTrackerUi.UpdateQuest(quest);  // TODO: Add a check that progress was actually done before calling this.
				
				if (quest.IsCompleted)
				{
					quest.NpcQuestHolder.OnQuestCompleted();
				}
			}
		}
	}
}
