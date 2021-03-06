﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandSelectionState : BaseAbilityMenuState {

	protected override void LoadMenu () {
		if (menuOptions == null) {
			menuTitle = "指令";
			menuOptions = new List<string>(3);
			menuOptions.Add("移动");
			menuOptions.Add("行动");
			menuOptions.Add("等待");
		}

		abilityMenuPanelController.Show (menuTitle, menuOptions);
		abilityMenuPanelController.SetLocked (0, turn.hasUnitMoved);
		abilityMenuPanelController.SetLocked (1, turn.hasUnitActed);
	}

	protected override void Confirm () {
		switch (abilityMenuPanelController.selection) {
		case 0:
			owner.ChangeState<MoveTargetState>();
			break;
		case 1:
			owner.ChangeState<CategorySelectionState>();
			break;
		case 2:
			owner.ChangeState<EndFacingState>();
			break;
		}
	}

	protected override void Cancel () {
		if (turn.hasUnitMoved && !turn.lockMove) {
			turn.UndoMove ();
			abilityMenuPanelController.SetLocked (0, false);
			SelectTile (turn.actor.tile.pos);
		} else {
			owner.ChangeState<ExploreState>();
		}
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
