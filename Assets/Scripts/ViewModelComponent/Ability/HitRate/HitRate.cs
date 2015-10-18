using UnityEngine;
using System.Collections;

public abstract class HitRate : MonoBehaviour {

	public const string AutomaticHitCheckNotification = "HitRate.AutomaticHitCheckNotification";
	public const string AutomaticMissCheckNotification = "HitRate.AutomaticMissCheckNotification";
	public const string StatusCheckNotification = "HitRate.StatusCheckNotification";

	protected Unit attacker;

	public abstract int Calculate(Tile target);

	protected virtual void Start() {
		attacker = GetComponentInParent<Unit> ();
	}

	protected virtual bool AutomaticHit(Unit target) {
		MatchException exc = new MatchException (attacker, target);
		this.PostNotification (AutomaticHitCheckNotification, exc);
		return exc.toggle;
	}

	protected virtual bool AutomaticMiss(Unit target) {
		MatchException exc = new MatchException (attacker, target);
		this.PostNotification (AutomaticMissCheckNotification, exc);
		return exc.toggle;
	}

	protected virtual int AdjustForStatusEffects(Unit target, int rate) {
		Info<Unit, Unit, int> args = new Info<Unit, Unit, int> (attacker, target, rate);
		this.PostNotification (StatusCheckNotification, args);
		return args.arg2;
	}

	protected virtual int Final(int evade) {
		return 100 - evade;
	}
}
