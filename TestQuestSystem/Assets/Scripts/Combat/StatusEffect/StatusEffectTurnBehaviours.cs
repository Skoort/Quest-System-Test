using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class StatusEffectTurnBehaviours
{
	public static void Test()
	{ 
	
	}

	public static void DoHealing(EntityGlue target, float healing)
	{
		target.EntityStats.Heal(healing);
	}

	public static void DoDamage(EntityGlue target, float damage)
	{
		target.EntityStats.Damage(damage);
	}

	public static void ApplyStatusEffect(EntityGlue entityGlue, StatusModEffect statusModEffect)
	{
		if (entityGlue?.EntityTurnManager != null)
		{
			entityGlue.EntityTurnManager.AddStatusEffect(statusModEffect);
		}
		if (entityGlue?.EntityStats != null)
		{
			entityGlue.EntityStats.maximumHealth -= statusModEffect.StatModifier.MaximumHealthChange;
			entityGlue.EntityStats.maximumEnergy -= statusModEffect.StatModifier.MaximumEnergyChange;
			entityGlue.EntityStats.maximumMagic -= statusModEffect.StatModifier.MaximumMagicChange;
			entityGlue.EntityStats.power -= statusModEffect.StatModifier.PowerChange;
			entityGlue.EntityStats.speed -= statusModEffect.StatModifier.SpeedChange;
			entityGlue.EntityStats.endurance -= statusModEffect.StatModifier.EnduranceChange;
			entityGlue.EntityStats.magicalAffinity -= statusModEffect.StatModifier.MagicalAffinityChange;
			entityGlue.EntityStats.defense -= statusModEffect.StatModifier.DefenseChange;
			entityGlue.EntityStats.primaryAttack -= statusModEffect.StatModifier.PrimaryAttackChange;
			entityGlue.EntityStats.offhandAttack -= statusModEffect.StatModifier.OffhandAttackChange;
		}
	}
}