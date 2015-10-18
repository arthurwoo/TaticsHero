using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageAbilityEffect : BaseAbilityEffect {

	public const string GetAttackNotification = "DamageAbilityEffect.GetAttackNotification";
	public const string GetDefenseNotification = "DamageAblityEffect.GetDefenseNotification";
	public const string GetPowerNotification = "DamageAbilityEffect.GetPowerNotification";
	public const string TweakDamageNotification = "DamageAbilityEffect.TweakDamageNotification";

	const int minDamage = -999;
	const int maxDamage = 999;

	public override int Predict (Tile target) {
		Unit attacker = GetComponentInParent<Unit> ();
		Unit defender = target.content.GetComponent<Unit> ();

		int attack = GetStat (attacker, defender, GetAttackNotification, 0);
		int defense = GetStat (attacker, defender, GetDefenseNotification, 0);

		int damage = attack - (defense / 2);
		damage = Mathf.Max (1, damage);

		int power = GetStat (attacker, defender, GetPowerNotification, 0);

		damage = power * damage / 100;
		damage = Mathf.Max (1, damage);

		damage = GetStat (attacker, defender, TweakDamageNotification, damage);

		damage = Mathf.Clamp (damage, minDamage, maxDamage);
		return damage;
	}

	public override void Apply (Tile target) {
		Unit defender = target.content.GetComponent<Unit> ();

		int value = Predict (target);
		value *= Mathf.FloorToInt (UnityEngine.Random.Range (0.9f, 1.1f));
		value = Mathf.Clamp (value, minDamage, maxDamage);

		Stats s = defender.GetComponent<Stats> ();
		s [StatTypes.HP] -= value;
	}

	int GetStat(Unit attacker, Unit defender, string notification, int startValue) {
		var mods = new List<ValueModifier> ();
		var info = new Info<Unit, Unit, List<ValueModifier>> (attacker, defender, mods);
		this.PostNotification (notification, info);
		mods.Sort ();

		float value = startValue;
		for (int i = 0; i < mods.Count; i++)
			value = mods[i].Modify(startValue, value);

		int retValue = Mathf.FloorToInt (value);
		retValue = Mathf.Clamp (retValue, minDamage, maxDamage);

		return retValue;
	}
}
