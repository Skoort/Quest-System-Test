using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefaultAttack : AbilityAction
{
	public DefaultAttack() : base(TargetType.SINGLE | TargetType.ENEMY)
	{
	}

	public override void Behaviour(EntityGlue user, EntityGlue[] targets)
	{
		/*
		if (user?.EquipmentManager != null)
		{ 
			// We prefer primary weapons and only default to offhand weapons if we can't find one.
			var weapon = user.EquipmentManager.Get(EquipmentType.PRIMARY) 
					  ?? user.EquipmentManager.Get(EquipmentType.OFFHAND);
			if (weapon != null)
			{
				var target = targets.FirstOrDefault();
				if (target != null)
				{ 
			
				}
			}
		}
		*/
		if (user?.EntityStats != null)
		{
			var target = targets.FirstOrDefault();
			if (target != null)
			{
				var damage = user.EntityStats.primaryAttack;
				var random = damage * Random.Range(-0.15F, +0.15F);
				target.EntityStats.Damage(damage + random);
			}
		}
	}
}
