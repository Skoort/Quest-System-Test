using UnityEngine;

[System.Serializable]
public class StatModifier
{
	[SerializeField] private float _maximumHealthChange = 0;
	[SerializeField] private float _maximumEnergyChange = 0;
	[SerializeField] private float _maximumMagicChange = 0;
	[SerializeField] private float _powerChange = 0;
	[SerializeField] private float _speedChange = 0;
	[SerializeField] private float _enduranceChange = 0;
	[SerializeField] private float _magicalAffinityChange = 0;
	[SerializeField] private float _defenseChange = 0;
	[SerializeField] private float _primaryAttackChange = 0;
	[SerializeField] private float _offhandAttackChange = 0;

	public float MaximumHealthChange => _maximumHealthChange;
	public float MaximumEnergyChange => _maximumEnergyChange;
	public float MaximumMagicChange => _maximumMagicChange;
	public float PowerChange => _powerChange;
	public float SpeedChange => _speedChange;
	public float EnduranceChange => _enduranceChange;
	public float MagicalAffinityChange => _magicalAffinityChange;
	public float DefenseChange => _defenseChange;
	public float PrimaryAttackChange => _primaryAttackChange;
	public float OffhandAttackChange => _offhandAttackChange;
}
