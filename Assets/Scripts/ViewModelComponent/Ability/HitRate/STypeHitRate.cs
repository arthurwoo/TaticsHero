using UnityEngine;
using System.Collections;

public class STypeHitRate : HitRate {

	public override int Calculate (Unit attacker, Unit target) {
		if (AutomaticHit (attacker, target))
			return Final (0);
		
		if (AutomaticMiss (attacker, target))
			return Final (100);

		int res = GetResistance (target);
		res = AdjustForStatusEffects (attacker, target, res);
		res = AdjustForRelativeFacing (attacker, target, res);
		res = Mathf.Clamp (res, 0, 100);
		return Final (res);
	}

	int GetResistance(Unit target) {
		Stats s = GetComponentInParent<Stats> ();
		return s [StatTypes.RES];
	}

	int AdjustForRelativeFacing(Unit attacker, Unit target, int rate) {
		switch (attacker.GetFacing (target)) {
		case Facings.Front:
			return rate;
		case Facings.Side:
			return rate - 10;
		default:
			return rate - 20;
		}
	}
}
