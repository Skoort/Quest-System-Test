using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRewardChoiceUi : MonoBehaviour
{
	[SerializeField] private QuestRewardButtonUi _rewardButtonPrefab = null;

	private List<QuestRewardButtonUi> _choices;
	private RewardTooltip _rewardTooltip;

	public bool IsChoiceMade { get; private set; }

	private void Awake()
	{
		_choices = new List<QuestRewardButtonUi>();
	}

	public QuestRewardChoiceUi Initialize(RewardTooltip rewardTooltip)
	{
		_rewardTooltip = rewardTooltip;
		return this;
	}

	public void AddSlot(SlotInfo slot)
	{
		var choice = Instantiate(_rewardButtonPrefab, this.transform).Initialize(slot, _rewardTooltip, this);
		_choices.Add(choice);
	}

	public void SelectChoice(QuestRewardButtonUi selectedChoice)
	{
		foreach (var choice in _choices)
		{
			choice.Select(choice == selectedChoice);
		}

		IsChoiceMade = true;
	}
}
