using System;
using System.Xml.Linq;

public static class ObjectiveFactory
{
	public static Objective Load(XElement xObjective)
	{
		var objectiveType = xObjective.Element("Type").Value;

		Objective objective;
		switch (objectiveType)
		{
			case "Defeat Enemies":
			{
				objective = new KillObjective(
					xObjective.Element("EnemyName").Value,
					int.Parse(xObjective.Element("Amount").Value));
				break;
			}
			case "Collect Items":
			{
				objective = new FindObjective(
					xObjective.Element("ItemName").Value,
					int.Parse(xObjective.Element("Amount").Value),
					!bool.Parse(xObjective.Element("RequiredForTurnIn").Value));
				break;
			}
			default:
			{
				throw new NotImplementedException($"Objective type '{objectiveType}' is not implemented!");
			}
		}
		return objective;
	}
}
