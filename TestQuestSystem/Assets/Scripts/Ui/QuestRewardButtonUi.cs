using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestRewardButtonUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private QuestRewardChoiceUi _choiceUi;

	[SerializeField] private Image _image = null;
	[SerializeField] private TextMeshProUGUI _quantityLabel = null;
	[SerializeField] private Transform _selectedOverlay = null;

	private RewardTooltip _rewardTooltip = null;
	[SerializeField] private float _hoverTimeForTooltip = 1F;
	private float _hoverTime;

	public bool IsSelected => _selectedOverlay.gameObject.activeInHierarchy;
	public SlotInfo SlotInfo { get; private set; }

	public QuestRewardButtonUi Initialize(SlotInfo slotInfo, RewardTooltip rewardTooltip, QuestRewardChoiceUi parentChoice = null)
	{
		_rewardTooltip = rewardTooltip;

		_choiceUi = parentChoice;
		Select(parentChoice == null);

		SlotInfo = slotInfo;
		_image.sprite = SlotInfo.Item.Sprite;
		_quantityLabel.text = SlotInfo.Quantity.ToString();

		return this;
	}

	public void Select(bool isSelected)
	{
		_selectedOverlay.gameObject.SetActive(isSelected);
	}

	private void Update()
	{
		if (_isHovering)
		{
			_hoverTime += Time.deltaTime;

			if (!_rewardTooltip.IsVisible && _hoverTime >= _rewardTooltip.RequiredHoverTime)
			{
				_rewardTooltip.LoadItemInfo(SlotInfo.Item);
				_rewardTooltip.SnapToMousePosition();  // Because FollowMouse doesn't work while disabled.
				_rewardTooltip.Show();
			}
		}
	}

	#region --Called from editor------------
	public void Click()
	{
		_choiceUi?.SelectChoice(this);
	}

	private bool _isHovering;

	public void OnPointerExit(PointerEventData eventData)
	{
		_isHovering = false;
		_hoverTime = 0;
		_rewardTooltip.Hide();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_isHovering = true;
	}

	#endregion
}
