using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public ItemInfo Item { get; private set; }
	public EquipmentType EquipmentType { get; private set; }
	public IStatusEffect StatusEffect { get; private set; }
}
