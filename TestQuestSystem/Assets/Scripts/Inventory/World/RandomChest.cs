using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChest : Chest
{
	[SerializeField] private int _numRandomItems = 3;

	private void GenerateRandomItems()
	{
		for (int i = 0; i < _numRandomItems; ++i)
		{
			int randIndex = Random.Range(0, ItemDatabase.Instance.Items.Count);
			var item = ItemDatabase.Instance.Items[randIndex];
			_inventory.AddItem(item);
		}
	}

	public override void Open()
	{
		if (!_isOpen && _player != null)
		{
			var playerInventory = _player.GetComponent<Inventory>();
			var playerQuestTracker = _player.GetComponent<QuestTracker>();
			if (playerInventory != null)
			{
				if (_isInfinite || !_openedOnce)
				{
					GenerateRandomItems();
				}
				foreach (var slot in _inventory.Items)
				{
					playerInventory.AddItem(slot.Item, slot.Quantity);
					playerQuestTracker.RegisterInventoryChange(slot.Item.Name, slot.Quantity);
				}
				_inventory.Clear();
				_animator.SetTrigger("Open");
				_isOpen = true;
				_openedOnce = true;
			}
		}
	}
}
