using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageAbilityEffect : BaseAbilityEffect {

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
		return -damage;
	}

	protected override int OnApply (Tile target) {
		Unit defender = target.content.GetComponent<Unit> ();

		int value = Predict (target);
		value = Mathf.FloorToInt (value * UnityEngine.Random.Range (0.9f, 1.1f));
		value = Mathf.Clamp (value, minDamage, maxDamage);

		Stats s = defender.GetComponent<Stats> ();
		s [StatTypes.HP] += value;

		return value;
	}
}
