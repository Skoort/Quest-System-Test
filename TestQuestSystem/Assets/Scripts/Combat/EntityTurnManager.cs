using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTurnManager : MonoBehaviour
{
	private EntityGlue _entityGlue;

	private List<IStatusEffect> _statusEffects;

	private bool _hasTurn;
	private bool _isTurnComplete;

	private void Awake()
	{
		_statusEffects = new List<IStatusEffect>();	
	}

	public EntityTurnManager Initialize(EntityGlue entityGlue)
	{
		_entityGlue = entityGlue;
		return this;
	}

	public void AddStatusEffect(IStatusEffect effect)
	{
		_statusEffects.Add(effect);
		effect.OnTargetFirstAffected?.Invoke();
	}

	public void RemStatusEffect(IStatusEffect effect)
	{
		_statusEffects.Remove(effect);  // Should probably be based on duration.
	}

	public int HasStatusEffect(IStatusEffect effect)
	{
		int c = 0;
		foreach (var statusEffect in _statusEffects)
		{
			if (statusEffect == effect)
			{
				++c;
			}
		}
		return c;
	}

	public IEnumerator GiveTurn()
	{
		ResolveStartOfTurn();
		yield return null;

		while (!_isTurnComplete)
		{ 
			// The logic should be that something will control what's going on and mark when it finishes.
		}

		ResolveEndOfTurn();
		yield return null;
	}
	
	private void ResolveStartOfTurn()
	{
		_hasTurn = true;
		_statusEffects.ForEach(x => x.OnStartTurn?.Invoke());
	}

	private void ResolveEndOfTurn()
	{
		_hasTurn = false;
		_statusEffects.ForEach(x => x.OnEndTurn?.Invoke());
	}
}
