using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDatabase
{
	public List<ItemInfo> Items { get; private set; }
	
	public ItemInfo ItemWithName(string name)
	{
		return Items.FirstOrDefault(x => x.Name == name);
	}

	private ItemDatabase()  // Purposefully private.
	{
		Items = new List<ItemInfo>();
	}

	public void Register(ItemInfo item)
	{
		if (!Items.Contains(item))
		{ 
			Items.Add(item);
		}
	}

	private static ItemDatabase _instance;
	public static ItemDatabase Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ItemDatabase();
			}
			return _instance;
		}
	}
}
