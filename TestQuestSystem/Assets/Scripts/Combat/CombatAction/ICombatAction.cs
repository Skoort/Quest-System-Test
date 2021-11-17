using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatAction
{
	void Behaviour(EntityGlue user, EntityGlue[] targets);
	TargetType TargetType { get; }
}
