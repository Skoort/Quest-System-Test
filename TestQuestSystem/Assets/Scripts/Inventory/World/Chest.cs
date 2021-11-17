using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
	[SerializeField] protected Inventory _inventory = null;
	[SerializeField] protected Animator _animator = null;
	[SerializeField] protected bool _isInfinite = false;
	protected Transform _player;

	protected bool _isOpen = false;
	protected bool _openedOnce = false;

	private void Awake()
	{
		if (_inventory == null) _inventory = GetComponent<Inventory>();
		if (_animator == null) _animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_player = other.transform;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			_player = null;

			if (_isOpen)
			{
				_animator.SetTrigger("Close");
				_isOpen = false;
			}
		}
	}

	public virtual void Open()
	{
		if (!_isOpen && _player != null)
		{
			var playerInventory = _player.GetComponent<Inventory>();
			var playerQuestTracker = _player.GetComponent<QuestTracker>();
			if (playerInventory != null)
			{
				foreach (var slot in _inventory.Items)
				{
					playerInventory.AddItem(slot.Item, slot.Quantity);
					playerQuestTracker.RegisterInventoryChange(slot.Item.Name, slot.Quantity);
				}

				if (!_isInfinite)
				{ 
					_inventory.Clear();
				}
				_animator.SetTrigger("Open");
				_isOpen = true;
				_openedOnce = true;
			}
		}
	}
}
