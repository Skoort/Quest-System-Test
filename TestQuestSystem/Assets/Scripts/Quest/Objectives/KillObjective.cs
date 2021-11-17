public class KillObjective : Objective
{
	private int _numKills;

	public string EnemyName { get; }
	public int NumEnemiesToDefeat { get; }

	public KillObjective(string enemyName, int numEnemiesToDefeat)
	{
		EnemyName = enemyName;
		NumEnemiesToDefeat = numEnemiesToDefeat;
	}
	
	public bool IsCompleted => _numKills >= NumEnemiesToDefeat;

	public void RegisterKill(string name)
	{
		if (name == EnemyName)
		{
			++_numKills;
		}
	}

	public void RegisterInventoryChange(string itemName, int change)
	{ 
	}

	public string ProgressText => $"Defeat {EnemyName}. {_numKills}/{NumEnemiesToDefeat}";
}
