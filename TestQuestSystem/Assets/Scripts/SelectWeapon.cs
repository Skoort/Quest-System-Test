using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
	[SerializeField] private Inventory _inventory = null;
	[SerializeField] private Animator _animator = null;

	[SerializeField] private List<Weapon> _weapons = null;
	[SerializeField] private Transform _weaponAttachPoint = null;

	private Weapon _currPrefab;
	private Weapon _newPrefab;
	private Weapon _currWeapon;

	private uint _swapIndex = 0;  // This is a running counter we purposely allow to overflow.

	private bool _isSheathing = false;

	private void Awake()
	{
		if (_inventory == null) _inventory = GetComponent<Inventory>();
		if (_animator == null) _animator = GetComponent<Animator>();

		_newPrefab = null;  // Assign this to the first weapon.
	}

	private void Update()
    {
		if (_currWeaponState != SelectedWeaponState.Swinging && !GameInfo.Instance.IsInMenu)
		{ 
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				var item = _inventory.Items.Where(x => x.Item.Name == "Longsword").FirstOrDefault();
				if (item != null)
				{ 
					SheatheWeapon(item.Item.WeaponPrefab);
				}
			} else
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				var item = _inventory.Items.Where(x => x.Item.Name == "Heavy mace").FirstOrDefault();
				if (item != null)
				{
					SheatheWeapon(item.Item.WeaponPrefab);
				}
			} else
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				var item = _inventory.Items.Where(x => x.Item.Name == "Dagger").FirstOrDefault();
				if (item != null)
				{
					SheatheWeapon(item.Item.WeaponPrefab);
				}
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				SheatheWeapon(null);
			}

			if (Input.GetKeyDown(KeyCode.Mouse0)
			&& _currWeaponState != SelectedWeaponState.Sheathed
			&& !_isSheathing)
			{
				_animator.SetTrigger("Attack");
			}
		}
	}

	private void SheatheWeapon(Weapon newPrefab)
	{
		_newPrefab = newPrefab;
		if (!_isSheathing && _currPrefab != _newPrefab)
		{ 
			_animator.SetTrigger("Toggle Weapon");
			if (_currWeaponState != SelectedWeaponState.Sheathed)  // NOTE: The sheathed state can go directly to drawn.
			{
				_isSheathing = true;
			}
		}
	}

	// For GUI
	private void SpawnWeapon()
	{
		// NOTE: This is called if sheathing is cancelled, in which case _currPrefab != null. Don't spawn a new weapon in this case.
		if (_newPrefab != null && _currPrefab == null)
		{
			_currWeapon = Instantiate(_newPrefab, _weaponAttachPoint);
			_currPrefab = _newPrefab;
		}
	}

	// For GUI
	private void DespawnWeapon()
	{
		if (_currWeapon != null)
		{
			Destroy(_currWeapon.gameObject);
			_currWeapon = null;
			_currPrefab = null;
		}

		if (_newPrefab != null)
		{  // The user swapped to a different weapon.
			_animator.SetTrigger("Toggle Weapon");  // This draws the desired weapon.
		}

		_isSheathing = false;
	}

	enum SelectedWeaponState
	{ 
		Sheathed,
		Drawn,
		Swinging
	}

	private SelectedWeaponState _currWeaponState;

	// For GUI
	private void SetState(SelectedWeaponState state)
	{
		_currWeaponState = state;
	}

	// For GUI
	private void TurnOnHitField()
	{
		_currWeapon.TurnOnHitField();
	}

	// For GUI
	private void TurnOffHitField()
	{
		_currWeapon.TurnOffHitField();
	}
}
