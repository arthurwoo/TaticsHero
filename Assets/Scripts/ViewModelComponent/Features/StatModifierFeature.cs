﻿using UnityEngine;
using System.Collections;

public class StatModifierFeature : Feature {

	public StatTypes type;
	public int amount;

	Stats stats {
		get {
			return _target.GetComponentInParent<Stats>();
		}
	}

	protected override void OnApply () {
		stats [type] += amount;
	}

	protected override void OnRemove () {
		stats [type] -= amount;
	}
}
