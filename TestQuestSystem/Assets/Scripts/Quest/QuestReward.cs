using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuestReward
{
	public List<SlotInfo> Choices { get; }

	public QuestReward()
	{
		Choices = new List<SlotInfo>();
	}

	public void AddChoice(string itemName, int quantity)
	{
		var item = ItemDatabase.Instance.Items.FirstOrDefault(x => x.Name == itemName);
		var slot = new SlotInfo(item, quantity);
		Choices.Add(slot);
	}
}
