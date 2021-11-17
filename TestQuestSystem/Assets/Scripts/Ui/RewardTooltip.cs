using System;
using TMPro;
using UnityEngine;

public class RewardTooltip : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _nameTextbox;
	[SerializeField] TextMeshProUGUI _descTextbox;
	[SerializeField] int _maxDescriptionLength = 50;
	[SerializeField] float _requiredHoverTime = 0.5F;

	public bool IsVisible => gameObject.activeInHierarchy;

	public float RequiredHoverTime => _requiredHoverTime;

	public void SnapToMousePosition()
	{
		transform.position = Input.mousePosition;
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void LoadItemInfo(ItemInfo item)
	{
		_nameTextbox.text = item.Name;

		var desc = item.Desc;
		if (desc.Length <= _maxDescriptionLength)
		{
			_descTextbox.text = desc;
		}
		else
		{
			var substring = desc.Substring(0, Math.Min(desc.Length, _maxDescriptionLength + 1));  // Get one extra character just in case the word ends on the 30th character.
			var indexOfLastSpace = substring.LastIndexOf(' ');
			_descTextbox.text = item.Desc.Substring(0, indexOfLastSpace) + "...";
		}
	}
}
