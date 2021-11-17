using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New StatusModEffect", menuName = "Status Effects/StatusModEffect")]
public class StatusModEffect : ScriptableObject, IStatusEffect
{
	[SerializeField]
	protected StatModifier _statModifier = null;
	[SerializeField]
	private int _turnDuration = 0;
	[SerializeField]
	private UnityEvent _onStartTurn;
	[SerializeField]
	private UnityEvent _onEndTurn;
	[SerializeField]
	private UnityEvent _onFirstAffected;

	public int TurnDuration => _turnDuration;

	public StatModifier StatModifier => _statModifier;

	public UnityEvent OnTargetFirstAffected => _onFirstAffected;

	public UnityEvent OnStartTurn => _onStartTurn;

	public UnityEvent OnEndTurn => _onEndTurn;
}
