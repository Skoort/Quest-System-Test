using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class Quest
{
	public int Id { get => _id; private set { _id = value; } }
	public string Name { get => _name; private set { _name = value; } }
	public string Desc { get => _desc; private set { _desc = value; } }
	public string TurnInText { get => _turnInText; private set { _turnInText = value; } }
	public string Npc { get => _npc; private set { _npc = value; } }
	public int? UnlocksId { get => _unlocksId; private set { _unlocksId = value; } }
	public Objective[] Objectives { get => _objectives; private set { _objectives = value; } }
	public List<QuestReward> Rewards { get; private set; }
	public QuestHolder NpcQuestHolder { get; private set; }

	[SerializeField] private int _id;
	[SerializeField] private string _name;
	[SerializeField] private string _desc;
	[SerializeField] private string _turnInText;
	[SerializeField] private string _npc;
	[SerializeField] private int? _unlocksId;
	[SerializeField] private Objective[] _objectives;

	public bool IsCompleted => Objectives.All(x => x.IsCompleted);

	private Quest()
	{ 
	}

	public static Quest ReadFromFile(QuestHolder npcQuestHolder, string file)
	{
		var xml = XDocument.Parse(File.ReadAllText(file));
		var xmlQuest = xml.Element("Quest");
		var unlocksId = xmlQuest.Element("UnlocksId").Value;
		var xmlObjectives = xmlQuest.Element("Objectives").Elements("Objective");
		var xmlRewards = xmlQuest.Element("Rewards").Elements("Reward");
		var quest = new Quest()
		{
			Id = int.Parse(xmlQuest.Element("Id").Value),
			Name = xmlQuest.Element("Name").Value,
			Desc = xmlQuest.Element("Desc").Value,
			TurnInText = xmlQuest.Element("TurnInText").Value,
			Npc = xmlQuest.Element("Npc").Value,
			UnlocksId = String.IsNullOrEmpty(unlocksId)
				? (int?) null
				: int.Parse(unlocksId),
			Objectives = new Objective[xmlObjectives.Count()],
			Rewards = new List<QuestReward>(),
			NpcQuestHolder = npcQuestHolder
		};

		int i = 0;
		foreach (var xmlObjective in xmlObjectives)
		{
			quest.Objectives[i] = ObjectiveFactory.Load(xmlObjective);
			++i;
		}

		foreach (var xmlReward in xmlRewards)
		{
			var reward = new QuestReward();
			var xmlChoices = xmlReward.Elements("Choice");
			foreach (var xmlChoice in xmlChoices)
			{
				var itemName = xmlChoice.Element("ItemName").Value;
				var itemQuantity = int.Parse(xmlChoice.Element("Quantity").Value);
				reward.AddChoice(itemName, itemQuantity);
			}
			if (xmlChoices.Count() == 0)
			{
				var itemName = xmlReward.Element("ItemName").Value;
				var itemQuantity = int.Parse(xmlReward.Element("Quantity").Value);
				reward.AddChoice(itemName, itemQuantity);
			}
			quest.Rewards.Add(reward);
		}

		return quest;
	}
}
