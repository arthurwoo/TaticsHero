using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class InflictAbilityEffect : BaseAbilityEffect {

	public string statusName;
	public int duration;

	public override int Predict (Tile target) {
		return 0;
	}

	public override void Apply (Tile target) {
		Type statusType = Type.GetType (statusName);
		if (statusType == null || !statusType.IsSubclassOf (typeof(StatusEffect))) {
			Debug.LogError("Invalid Status Type");
			return;
		}

		MethodInfo mi = typeof(Status).GetMethod ("Add");
		Type[] types = new Type[]{statusType, typeof(DurationStatusCondition)};
		MethodInfo construted = mi.MakeGenericMethod (types);

		Status status = target.content.GetComponent<Status> ();
		object retValue = construted.Invoke (status, null);

		DurationStatusCondition condition = retValue as DurationStatusCondition;
		condition.duration = duration;
	}
}
