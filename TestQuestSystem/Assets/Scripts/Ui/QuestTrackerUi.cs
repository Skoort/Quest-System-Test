using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTrackerUi : MonoBehaviour
{
	[SerializeField] private Transform _questElementPrefab = null;
	[SerializeField] private Transform _objectiveElementPrefab = null;

	public void AddQuest(Quest quest)
	{
		var newQuestElement = Instantiate(_questElementPrefab, this.transform);
		newQuestElement.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest.Name;
		foreach (var objective in quest.Objectives)
		{
			var newObjectiveElement = Instantiate(_objectiveElementPrefab, newQuestElement);
			newObjectiveElement.GetComponent<TextMeshProUGUI>().text = $"- {objective.ProgressText}";
		}
	}

	public void RemQuest(Quest quest)
	{
		foreach (var childTextMesh in transform.GetComponentsInChildren<TextMeshProUGUI>())
		{
			if (childTextMesh.gameObject.name == "Quest Name Text" && childTextMesh.text == quest.Name)
			{
				Destroy(childTextMesh.gameObject.transform.parent.gameObject);
			}
		}
	}

	public void UpdateQuest(Quest quest)
	{
		foreach (var childTextMesh in transform.GetComponentsInChildren<TextMeshProUGUI>())
		{
			if (childTextMesh.gameObject.name == "Quest Name Text" && childTextMesh.text == quest.Name)
			{
				var questElement = childTextMesh.gameObject.transform.parent;
				for (int i = 1; i < questElement.childCount; ++i)
				{
					var objectiveElement = questElement.GetChild(i);
					objectiveElement.GetComponent<TextMeshProUGUI>().text = $"- {quest.Objectives[i-1].ProgressText}";
				}
			}
		}
	}
}
