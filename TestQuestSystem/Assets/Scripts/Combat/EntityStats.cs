using System;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
	private EntityGlue _entityGlue;

	public EntityStats Initialize(EntityGlue entityGlue)
	{
		_entityGlue = entityGlue;
		return this;
	}

	public event Action OnDeath;
	public event Action OnDamaged;

	public void Damage(float damage)
	{
		OnDamaged?.Invoke();

		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			OnDeath?.Invoke();
		}
	}

	public void Heal(float healing)
	{
		currentHealth += healing;

		if (currentHealth > maximumHealth)
		{
			currentHealth = maximumHealth;
		}
	}

	public float maximumHealth;
	public float currentHealth;
	public float maximumEnergy;
	public float currentEnergy;
	public float maximumMagic;
	public float currentMagic;
	public float power;
	public float speed;
	public float endurance;
	public float magicalAffinity;
	public float defense;
	public float primaryAttack;
	public float offhandAttack;
}
