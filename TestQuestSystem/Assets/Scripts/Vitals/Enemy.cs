using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private static GameObject _player = null;

	[SerializeField] private HealthScript _health;
	[SerializeField] private Animator _anim;


	private void Awake()
	{
		if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
		if (_health == null) _health = GetComponent<HealthScript>();
		if (_anim == null) _anim = GetComponent<Animator>();

		_health.OnDamaged += PlayDamagedAnim;
		_health.OnDeath += Die;
	}

	private void PlayDamagedAnim()
	{
		if (_health.Health > 0)
		{ 
			_anim.SetTrigger("Damaged");
		}
	}

	private void Die()
	{
		_health.OnDeath -= Die;  // We don't want the player hitting the enemy's collider twice before it despawns and registering another kill.
		_anim.SetTrigger("Died");
		_player.GetComponent<QuestTracker>().RegisterKill(gameObject.name);
		StartCoroutine(Cleanup());
	}

	private IEnumerator Cleanup()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}
}
