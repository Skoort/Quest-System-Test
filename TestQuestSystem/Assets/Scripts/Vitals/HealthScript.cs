using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
	[SerializeField] private float _health;
	[SerializeField] private float _maxHealth;
	public float Health { get => _health; private set { _health = value; } }
	public float MaxHealth { get => _maxHealth; private set { _maxHealth = value; } }
	
	public event Action OnDeath;
	public event Action OnDamaged;

	public void Damage(float damage)
	{	
		OnDamaged?.Invoke();

		Health -= damage;

		if (Health <= 0)
		{
			Health = 0;
			OnDeath?.Invoke();
		}
	}

	public void Heal(float healing)
	{
		Health += healing;

		if (Health > MaxHealth)
		{
			Health = MaxHealth;
		}
	}
}
