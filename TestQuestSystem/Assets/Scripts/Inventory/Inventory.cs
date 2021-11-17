using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	private EntityGlue _entityGlue;

	[SerializeField] private List<SlotInfo> _items;

	public Inventory Initialize(EntityGlue entityGlue)
	{
		_entityGlue = entityGlue;
		return this;
	}

	public void AddItem(ItemInfo item, int amount = 1)
	{
		int amountLeft = amount;
		foreach (var slot in GetMatchingSlots(item))
		{
			int amountAdded = Math.Min(amountLeft, slot.AvailableQuantity);
			slot.Quantity += amountAdded;
			amountLeft -= amountAdded;
		}

		while (amountLeft > 0)
		{
			int amountAdded = Math.Min(amountLeft, item.MaxStack);
			_items.Add(new SlotInfo(item, amountAdded));
			amountLeft -= amountAdded;
		}
	}

	public int RemItem(ItemInfo item, int amount = 1)
	{
		int totalRemoved = 0;

		foreach (var slot in GetMatchingSlots(item).ToList())  // Make a copy so that we don't loop over a modified collection.
		{
			int amountRemoved = Math.Min(amount - totalRemoved, slot.Quantity);
			slot.Quantity -= amountRemoved;
			if (slot.Quantity == 0)
			{
				_items.Remove(slot);
			}
			totalRemoved += amountRemoved;
		}

		return amount - totalRemoved;
	}

	private IEnumerable<SlotInfo> GetMatchingSlots(ItemInfo item)
	{
		return _items.Where(x => x.Item == item);
	}

	public bool HasItem(ItemInfo item, int amount)
	{
		return GetItemAmount(item) >= amount;
	}

	public int GetItemAmount(ItemInfo item)
	{
		return GetMatchingSlots(item).Sum(x => x.Quantity);
	}


	public SlotInfo GetSlot(string name)
	{
		return _items.Where(x => x.Item.Name == name).FirstOrDefault();
	}

	public IEnumerable<SlotInfo> Items => _items;

	public void Clear()
	{
		_items = new List<SlotInfo>();
	}
}
