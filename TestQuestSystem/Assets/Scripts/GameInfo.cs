using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
	public static GameInfo Instance { get; private set; }

    public bool IsInMenu { get; set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
			Debug.LogError("Attempted to create multiple instances of singleton GameInfo!");
		}
	}
}
