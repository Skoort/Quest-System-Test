using UnityEngine;

class FindObjective : Objective
{
	public string ItemName { get; }
	public int NumItemsToFind { get; }
	public bool KeepItemsAfterCompleted { get; set; }
	public ItemInfo Item { get; private set; }

	private Inventory _playerInventory;
	private int _itemAmount;

	public FindObjective(string itemName, int numItemsToFind, bool keepItems)
	{
		ItemName = itemName;
		NumItemsToFind = numItemsToFind;
		KeepItemsAfterCompleted = keepItems;
		Item = ItemDatabase.Instance.ItemWithName(ItemName);
	}

	public bool IsCompleted => _itemAmount >= NumItemsToFind;

	public void RegisterInventoryChange(string itemName, int amount)
	{
		if (_playerInventory == null)
		{
			_playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		}

		_itemAmount = _playerInventory.GetItemAmount(Item);
	}

	public void RegisterKill(string enemyName)
	{
	}

	public string ProgressText => $"Collect {ItemName}. {_itemAmount}/{NumItemsToFind}";
}
