using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestRewardsUi : MonoBehaviour
{
	[SerializeField] private Transform _rewardsUiRoot;

	[SerializeField] private QuestRewardChoiceUi _choicePrefab;
	[SerializeField] private QuestRewardButtonUi _rewardPrefab;

	[SerializeField] private RewardTooltip _rewardTooltip;

	public void Activate(bool isActive)
	{
		_rewardTooltip.Hide();
		gameObject.SetActive(isActive);
	}

	public void LoadQuest(Quest quest)
	{
		ClearPreviousQuestInfo();

		foreach (var reward in quest.Rewards)
		{
			if (reward.Choices.Count == 1)
			{
				var rewardButton = Instantiate(_rewardPrefab, _rewardsUiRoot.transform).Initialize(reward.Choices[0], _rewardTooltip, null);
			}
			else
			{
				var choiceUi = Instantiate(_choicePrefab, _rewardsUiRoot.transform).Initialize(_rewardTooltip);
				foreach (var choice in reward.Choices)
				{
					choiceUi.AddSlot(choice);
				}
			}
		}
	}

	private void ClearPreviousQuestInfo()
	{
		int numChildren = _rewardsUiRoot.childCount;
		for (int i = 0; i < numChildren; ++i)
		{
			Destroy(_rewardsUiRoot.GetChild(i).gameObject);
		}
	}

	public bool AreAllChoicesMade()
	{
		foreach (var choiceUi in _rewardsUiRoot.GetComponentsInChildren<QuestRewardChoiceUi>())
		{
			if (!choiceUi.IsChoiceMade) return false;
		}
		return true;
	}

	public IEnumerable<SlotInfo> GetSelectedRewards()
	{
		return _rewardsUiRoot.GetComponentsInChildren<QuestRewardButtonUi>().Where(x => x.IsSelected).Select(x => x.SlotInfo);
	}
}
