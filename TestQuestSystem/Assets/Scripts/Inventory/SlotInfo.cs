using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotInfo
{
	[SerializeField] private ItemInfo _item = null;
	[SerializeField] private int _quantity = 0;

	public SlotInfo(ItemInfo item, int quantity) 
	{
		_item = item;
		_quantity = quantity;
	}

	public ItemInfo Item => _item;
	public int Quantity { get { return _quantity; } set { _quantity = value; } }

	public int AvailableQuantity => Item.MaxStack - Quantity;
}
