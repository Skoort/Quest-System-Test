using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private float _damage = 10;
	public float Damage => _damage;

	[SerializeField] private Collider _collider;

	private HashSet<HealthScript> _enemiesHitThisSwing;

	private void Awake()
	{
		if (_collider == null) _collider = GetComponent<Collider>();

		_enemiesHitThisSwing = new HashSet<HealthScript>();
	}

	public void TurnOnHitField()
	{
		_enemiesHitThisSwing.Clear();
		_collider.enabled = true;
	}

	public void TurnOffHitField()
	{
		_collider.enabled = false;
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			var parentObject = other.attachedRigidbody.gameObject;  // Following convention that the rigidbody is always on the root.
			var healthObject = parentObject.GetComponent<HealthScript>();  // The convention is that every enemy has a HealthScript in its root game object.

			Debug.Log($"Hit: {parentObject.name}");

			if (!_enemiesHitThisSwing.Contains(healthObject))
			{  // We have already damaged the enemy once this swing, don't do it again.
				Debug.Log($"Damaged the enemy!");
				_enemiesHitThisSwing.Add(healthObject);
				healthObject.Damage(_damage);
			}
		}
	}
}
