using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUi : MonoBehaviour
{
	[SerializeField] private QuestTracker _tracker = null;

	[SerializeField] private Transform _questWindow = null;
	[SerializeField] private Button _acceptButton = null;
	[SerializeField] private Button _abandonButton = null;
	[SerializeField] private Button _turnInButton = null;

	[SerializeField] private QuestRewardsUi _rewardsUi = null;
	[SerializeField] private Transform _descriptionWindow = null;
	[SerializeField] private Text _nameText = null;
	[SerializeField] private Text _descText = null;
	[SerializeField] private Text _turnInText = null;

	private Quest _quest;

	public void LoadQuest(Quest quest, QuestProgress status)
	{
		GameInfo.Instance.IsInMenu = true;
		Cursor.lockState = CursorLockMode.None;

		_quest = quest;

		_questWindow.gameObject.SetActive(true);

		switch (status)
		{
			case QuestProgress.NotStarted:
			{
				LoadNotStartedQuest();
				break;
			}
			case QuestProgress.InProgress:
			{
				LoadInProgressQuest();
				break;
			}
			case QuestProgress.ReadyToTurnIn:
			{
				LoadReadyToTurnInQuest();
				break;
			}
		}
	}

	private void LoadNotStartedQuest()
	{
		_nameText.text = _quest.Name;
		_descText.text = _quest.Desc;
		EnablePanels(turnIn: false);
		EnableButtons(true, false, false);
	}

	private void LoadInProgressQuest()
	{
		_nameText.text = _quest.Name;
		_descText.text = _quest.Desc;
		EnablePanels(turnIn: false);
		EnableButtons(false, true, false);
	}

	private void LoadReadyToTurnInQuest()
	{
		_nameText.text = _quest.Name;
		_turnInText.text = _quest.TurnInText;
		EnablePanels(turnIn: true);
		EnableButtons(false, true, true);

		_rewardsUi.LoadQuest(_quest);
	}

	private void EnableButtons(bool accept, bool abandon, bool turnIn)
	{
		_acceptButton.gameObject.SetActive(accept);
		_abandonButton.gameObject.SetActive(abandon);
		_turnInButton.gameObject.SetActive(turnIn);
	}

	private void EnablePanels(bool turnIn)
	{
		_rewardsUi.Activate(turnIn);
		_descriptionWindow.gameObject.SetActive(!turnIn);
	}

	#region ---Editor hooks-------
	public void CloseWindow()
	{
		GameInfo.Instance.IsInMenu = false;
		Cursor.lockState = CursorLockMode.Locked;

		_questWindow.gameObject.SetActive(false);
		EnableButtons(false, false, false);
	}

	public void AbandonQuest()
	{
		_tracker.RemQuest(_quest, wasTurnedIn:false);
		CloseWindow();
	}

	public void AcceptQuest()
	{
		_tracker.AddQuest(_quest);
		CloseWindow();
	}

	public void TurnInQuest()
	{
		if (_rewardsUi.AreAllChoicesMade())
		{
			var chosenRewards = _rewardsUi.GetSelectedRewards();
			
			_tracker.RemQuest(_quest, wasTurnedIn:true, chosenRewards);
			CloseWindow();
		}
	}
	#endregion
}
