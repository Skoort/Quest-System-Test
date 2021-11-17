using UnityEngine.Events;

public interface IStatusEffect
{
	UnityEvent OnTargetFirstAffected { get; }
	UnityEvent OnStartTurn { get; }
	UnityEvent OnEndTurn { get; }
	int TurnDuration { get; }
}
