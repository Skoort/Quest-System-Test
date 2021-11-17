using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
	private List<int> _participants;
	private int _activeParticipant;

	public void BeginBattle(List<int> participants)
	{
		_participants = participants;
		ReorderBasedOnInitiative();
	}

	// Called at the start of a battle and if an enemy or player is resurrected or joins the fight.
	public void AddParticipant()
	{ 
	
	}

	// Called if an enemy or player dies.
	public void RemParticipant()
	{ 
	
	}

	private void ReorderBasedOnInitiative()
	{ 
	
	}
}
