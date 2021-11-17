public interface Objective
{
	bool IsCompleted { get; }
	void RegisterKill(string enemyName);
	void RegisterInventoryChange(string itemName, int amount);
	string ProgressText { get; }
}
