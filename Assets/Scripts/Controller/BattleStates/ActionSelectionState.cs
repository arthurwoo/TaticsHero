using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionSelectionState : BaseAbilityMenuState {

	public static int category;
	AbilityCatalog catalog;

	protected override void LoadMenu () {
		catalog = turn.actor.GetComponentInChildren<AbilityCatalog> ();
		GameObject container = catalog.GetCategory (category);
		menuTitle = container.name;

		int count = catalog.AbilityCount (container);
		if (menuOptions == null)
			menuOptions = new List<string> ();
		else
			menuOptions.Clear ();

		bool[] locks = new bool[count];
		for (int i = 0; i < count; i++) {
			Ability ability = catalog.GetAbility(category, i);
			AbilityMagicCost cost = ability.GetComponent<AbilityMagicCost>();
			if(cost)
				menuOptions.Add(string.Format("{0}:{1}", ability.name, cost.amount));
			else
				menuOptions.Add(ability.name);
			locks[i] = !ability.CanPerform();
		}

		abilityMenuPanelController.Show (menuTitle, menuOptions);

		for (int i = 0; i < count; i++)
			abilityMenuPanelController.SetLocked (i, locks [i]);
	}

	protected override void Confirm () {
		turn.ability = catalog.GetAbility (category, abilityMenuPanelController.selection);
		owner.ChangeState<AbilityTargetState> ();
	}

	protected override void Cancel () {
		owner.ChangeState<CategorySelectionState> ();
	}

	void SetOptions(string[] options) {
		menuOptions.Clear ();
		for (int i = 0; i < options.Length; i++) 
			menuOptions.Add (options [i]);
	}

	public override void Enter () {
		base.Enter ();
		statPanelController.ShowPrimary (turn.actor.gameObject);
	}
	
	public override void Exit () {
		base.Exit ();
		statPanelController.HidePrimary ();
	}
}
