using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
	[SerializeField] private Transform _lookRoot;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F) && !GameInfo.Instance.IsInMenu)
		{
			if (Physics.Raycast(_lookRoot.position, _lookRoot.forward, out var hitInfo, float.PositiveInfinity, ~LayerMask.GetMask("Player", "First Person")))
			{
				Debug.Log("Hit " + hitInfo.transform.gameObject?.name);

				var chestComponent = hitInfo.rigidbody?.transform?.GetComponent<Chest>();
				if (chestComponent != null)
				{
					chestComponent.Open();	
				}

				var questHolderComponent = hitInfo.rigidbody?.transform?.GetComponent<QuestHolder>();
				if (questHolderComponent != null && (hitInfo.point - transform.position).magnitude <= 2.5)
				{
					questHolderComponent.Speak();
				}
			}
		}
	}
}
