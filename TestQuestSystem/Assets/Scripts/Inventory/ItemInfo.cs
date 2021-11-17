using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ItemInfo", menuName = "Inventory/New ItemInfo")]
public class ItemInfo : ScriptableObject
{
	[SerializeField] private string _name = "Name";
	[SerializeField] private string _desc = "Desc";
	[SerializeField] private int _maxStack = 1;
	[SerializeField] private bool _isWeapon = false;
	[SerializeField] private Weapon _weaponPrefab = null;
	[SerializeField] private bool _isConsumable = false;
	[SerializeField] private int _worth = 0;
	[SerializeField] private Sprite _sprite = null;

	public string Name => _name;
	public string Desc => _desc;
	public int MaxStack => _maxStack;
	public bool IsWeapon => _isWeapon;
	public Weapon WeaponPrefab => _weaponPrefab;
	public bool IsConsumable => _isConsumable;
	public int Worth => _worth;
	public Sprite Sprite => _sprite;

	private void Awake()
	{
		ItemDatabase.Instance.Register(this);
	}

	private void OnEnable()
	{
		ItemDatabase.Instance.Register(this);
	}
}
