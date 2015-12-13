﻿using UnityEngine;
using System.Collections;

public class KnockOutStatusEffect : StatusEffect {

	Unit owner;
	Stats stats;

	void Awake() {
		owner = GetComponentInParent<Unit> ();
		stats = owner.GetComponent<Stats> ();
	}

	void OnEnable() {
		owner.transform.localScale = new Vector3 (0.75f, 0.1f, 0.75f);
		this.AddObserver (OnTurnCheck, TurnOrderController.TurnCheckNotification, owner);
		this.AddObserver (OnStatCounterWillChange, Stats.WillChangeNotification (StatTypes.CTR), stats);
	}

	void OnDisable() {
		owner.transform.localScale = Vector3.one;
		this.RemoveObserver (OnTurnCheck, TurnOrderController.TurnCheckNotification, owner);
		this.RemoveObserver (OnStatCounterWillChange, Stats.WillChangeNotification (StatTypes.CTR), stats);
	}

	void OnTurnCheck(object sender, object args) {
		BaseException exc = args as BaseException;
		if (exc.defaultToggle == true)
			exc.FlipToggle ();
	}

	void OnStatCounterWillChange(object sender, object args) {
		ValueChangeException exc = args as ValueChangeException;
		if (exc.toValue > exc.fromValue)
			exc.FlipToggle ();
	}
}
