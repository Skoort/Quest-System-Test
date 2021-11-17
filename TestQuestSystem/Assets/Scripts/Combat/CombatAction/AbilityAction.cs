using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityAction : ICombatAction
{
	public TargetType TargetType { get; }

	public abstract void Behaviour(EntityGlue user, EntityGlue[] targets);

	public AbilityAction(TargetType targetType)
	{
		TargetType = targetType;
	}
}
