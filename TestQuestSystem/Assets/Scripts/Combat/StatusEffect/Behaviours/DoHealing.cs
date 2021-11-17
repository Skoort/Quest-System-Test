using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat Behaviours/DoHealing")]
public class DoHealing : ScriptableObject
{
	public void Heal(EntityGlue glue, int healing)
	{
		glue.EntityStats?.Heal(healing);
	}
}
