using UnityEngine;
using System.Collections;

public class FullTypeHitRate : HitRate {

	public override int Calculate (Unit attacker, Unit target) {
		if (AutomaticMiss (attacker, target))
			return Final (100);

		return Final (0);
	}
}
