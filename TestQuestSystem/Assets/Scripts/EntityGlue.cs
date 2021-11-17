using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGlue : MonoBehaviour
{
	public Inventory Inventory { get; private set; }
	public EquipmentManager EquipmentManager { get; private set; }
	public EntityStats EntityStats { get; private set; }
	public EntityTurnManager EntityTurnManager { get; private set; }

	private void Awake()
	{
		Inventory = GetComponent<Inventory>().Initialize(this);
		EquipmentManager = GetComponent<EquipmentManager>().Initialize(this);
		EntityStats = GetComponent<EntityStats>().Initialize(this);
		EntityTurnManager = GetComponent<EntityTurnManager>().Initialize(this);
	}
}
