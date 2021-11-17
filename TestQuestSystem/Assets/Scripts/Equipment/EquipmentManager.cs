using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	private EntityGlue _entityGlue;

	private Dictionary<EquipmentType, Equipment> _equipmentMap;

	public void Awake()
	{
		_equipmentMap = new Dictionary<EquipmentType, Equipment>();
	}

	public EquipmentManager Initialize(EntityGlue entityGlue)
	{
		_entityGlue = entityGlue;
		return this;
	}

	public void Equip(Equipment equipment)
	{
		if (_entityGlue?.Inventory)
		{
			if (_equipmentMap.TryGetValue(equipment.EquipmentType, out var alreadyEquipped))
			{
				OnUnequip(alreadyEquipped);
			}
			_equipmentMap[equipment.EquipmentType] = equipment;
			OnEquip(equipment);
		}
	}

	public void Unequip(EquipmentType equipmentType)
	{
		if (_entityGlue?.Inventory)
		{
			if (_equipmentMap.TryGetValue(equipmentType, out var alreadyEquipped))
			{
				OnUnequip(alreadyEquipped);
				_equipmentMap.Remove(alreadyEquipped.EquipmentType);
			}
		}
	}

	private void OnUnequip(Equipment equipment)
	{
		_entityGlue.Inventory.AddItem(equipment.Item);
		_entityGlue.EntityTurnManager?.RemStatusEffect(equipment.StatusEffect);
	}

	private void OnEquip(Equipment equipment)
	{
		_entityGlue.Inventory.RemItem(equipment.Item);
		_entityGlue.EntityTurnManager?.AddStatusEffect(equipment.StatusEffect);
	}

	public Equipment Get(EquipmentType equipmentType)
	{
		if (_equipmentMap.TryGetValue(equipmentType, out var alreadyEquipped))
		{
			return alreadyEquipped;
		}
		else
		{
			return null;
		}
	}
}
