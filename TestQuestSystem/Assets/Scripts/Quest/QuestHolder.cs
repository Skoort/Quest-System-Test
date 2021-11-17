using System.IO;
using UnityEngine;

public class QuestHolder : MonoBehaviour
{
	[SerializeField] private QuestUi _questUi = null;

	[SerializeField] private string _questRelativePath = "Quests/";

	private bool _inProgress;
	private bool _isCompleted;

	private Quest _quest;

	[SerializeField] private GameObject _isAvailableIcon = null;
	[SerializeField] private GameObject _inProgressIcon = null;
	[SerializeField] private GameObject _isCompletedIcon = null;

	private void Awake()
	{
		EnableIcon(true, false, false);
	}

	public void Speak()
	{
		if (!_inProgress)
		{
			_quest = Quest.ReadFromFile(this, Path.Combine(Application.streamingAssetsPath, _questRelativePath));
			_questUi.LoadQuest(_quest, QuestProgress.NotStarted);
		} else
		if (_inProgress && !_isCompleted)
		{
			_questUi.LoadQuest(_quest, QuestProgress.InProgress);
		} else
		if (_isCompleted)
		{
			_questUi.LoadQuest(_quest, QuestProgress.ReadyToTurnIn);
		}
	}

	public void OnQuestAccepted()
	{
		_inProgress = true;
		EnableIcon(false, true, false);
	}

	public void OnQuestCompleted()
	{
		if (!_isCompleted)
		{ 
			_isCompleted = true;
			EnableIcon(false, false, true);
		}
	}

	public void OnQuestAbandoned()
	{
		_inProgress = false;
		_isCompleted = false;
		EnableIcon(true, false, false);
	}

	public void OnQuestTurnedIn()
	{
		EnableIcon(false, false, false);
		Destroy(this);
	}

	private void EnableIcon(bool isAvailable, bool inProgress, bool isCompleted)
	{
		_isAvailableIcon.SetActive(isAvailable);
		_inProgressIcon.SetActive(inProgress);
		_isCompletedIcon.SetActive(isCompleted);
	}
}
