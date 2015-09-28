using UnityEngine;
using System.Collections;

public abstract class HitRate : MonoBehaviour {

	public const string AutomaticHitCheckNotification = "HitRate.AutomaticHitCheckNotification";
	public const string AutomaticMissCheckNotification = "HitRate.AutomaticMissCheckNotification";
	public const string StatusCheckNotification = "HitRate.StatusCheckNotification";

	public abstract int Calculate(Unit attacker, Unit target);

	protected virtual bool AutomaticHit(Unit attacker, Unit target) {
		MatchException exc = new MatchException (attacker, target);
		this.PostNotification (AutomaticHitCheckNotification, exc);
		return exc.toggle;
	}

	protected virtual bool AutomaticMiss(Unit attacker, Unit target) {
		MatchException exc = new MatchException (attacker, target);
		this.PostNotification (AutomaticMissCheckNotification, exc);
		return exc.toggle;
	}

	protected virtual int AdjustForStatusEffects(Unit attacker, Unit target, int rate) {
		Info<Unit, Unit, int> args = new Info<Unit, Unit, int> (attacker, target, rate);
		this.PostNotification (StatusCheckNotification, args);
		return args.arg2;
	}

	protected virtual int Final(int evade) {
		return 100 - evade;
	}
}
