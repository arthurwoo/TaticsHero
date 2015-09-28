using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionSelectionState : BaseAbilityMenuState {

	public static int category;
	string[] whiteMagicOptions = new string[] {"治疗", "复活", "圣光"};
	string[] blackMagicOptions = new string[] {"火球", "冰球", "闪电"};

	protected override void LoadMenu () {
		if (menuOptions == null)
			menuOptions = new List<string> (3);

		if (category == 0) {
			menuTitle = "白魔法";
			SetOptions (whiteMagicOptions);
		} else {
			menuTitle = "黑魔法";
			SetOptions (blackMagicOptions);
		}

		abilityMenuPanelController.Show (menuTitle, menuOptions);
	}

	protected override void Confirm () {
		turn.hasUnitActed = true;
		if (turn.hasUnitMoved)
			turn.lockMove = true;
		owner.ChangeState<CommandSelectionState> ();
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
