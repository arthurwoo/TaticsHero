using UnityEngine;
using System.Collections;

public class HealAbilityEffect : BaseAbilityEffect {

	public override int Predict (Tile target) {
		Unit attacker = GetComponentInParent<Unit> ();
		Unit defender = target.content.GetComponent<Unit> ();
		return GetStat (attacker, defender, GetPowerNotification, 0);
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
