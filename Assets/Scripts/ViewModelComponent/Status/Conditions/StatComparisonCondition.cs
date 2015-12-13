using UnityEngine;
using System.Collections;
using System;

public class StatComparisonCondition : StatusCondition {

	public StatTypes type {
		get;
		private set;
	}

	public int value {
		get;
		private set;
	}

	public Func<bool> condition {
		get;
		private set;
	}

	Stats stats;

	void Awake() {
		stats = GetComponentInParent<Stats> ();
	}

	void OnDisable() {
		this.RemoveObserver (OnStatChanged, Stats.DidChangeNotification (StatTypes.HP), stats);
	}

	public void Init(StatTypes type, int value, Func<bool> condition) {
		this.type = type;
		this.value = value;
		this.condition = condition;
		this.AddObserver (OnStatChanged, Stats.DidChangeNotification (StatTypes.HP), stats);
	}

	public bool EqualTo() {
		return stats [type] == value;
	}

	public bool LessThan() {
		return stats [type] < value;
	}

	public bool LessThanOrEqualTo() {
		return LessThan () || EqualTo ();
	}

	public bool GreaterThan() {
		return stats [type] > value;
	}

	public bool GreaterThanOrEqualTo() {
		return GreaterThan () || EqualTo ();
	}

	void OnStatChanged(object sender, object args) {
		if (condition != null && !condition ())
			Remove ();
	}
}
