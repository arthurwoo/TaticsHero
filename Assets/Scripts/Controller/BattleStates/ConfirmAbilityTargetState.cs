using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfirmAbilityTargetState : BattleState {

	List<Tile> tiles;
	AbilityArea aa;
	int index = 0;

	AbilityEffectTarget[] targeters;

	public override void Enter () {
		base.Enter ();
		aa = turn.ability.GetComponent<AbilityArea> ();
		tiles = aa.GetTilesInArea (board, pos);
		board.SelectTiles (tiles);
		FindTargets ();
		RefreshPrimaryStatPanel (turn.actor.tile.pos);

		if (turn.targets.Count > 0) {
			hitSuccessIndicator.Show ();
			SetTarget (0);
		}
	}

	public override void Exit () {
		base.Exit ();
		board.DeSelectTiles (tiles);
		statPanelController.HidePrimary ();
		statPanelController.HideSecondary ();
		hitSuccessIndicator.Hide ();
	}

	protected override void OnMove (object sender, InfoEventArgs<Point> e) {
		if (e.info.y > 0 || e.info.x > 0)
			SetTarget (index + 1);
		else
			SetTarget (index - 1);
	}

	protected override void OnFire (object sender, InfoEventArgs<int> e) {
		if (e.info == 0) {
			if (turn.targets.Count > 0)
				owner.ChangeState<PerformAbilityState> ();
		} else {
			owner.ChangeState<AbilityTargetState>();
		}
	}

	void FindTargets() {
		turn.targets = new List<Tile> ();
		targeters = turn.ability.GetComponentsInChildren<AbilityEffectTarget> ();
		for(int i = 0; i < tiles.Count; i++) {
			if(IsTarget(tiles[i], targeters))
				turn.targets.Add (tiles[i]);
		}
	}

	bool IsTarget(Tile tile, AbilityEffectTarget[] list) {
		for (int i = 0; i < list.Length; i++) {
			if(list[i].IsTarget(tile))
				return true;
		}
		return false;
	}

	void SetTarget(int target) {
		index = target;
		if (index < 0)
			index = turn.targets.Count - 1;
		if (index >= turn.targets.Count)
			index = 0;

		if (turn.targets.Count > 0) {
			RefreshSecondaryStatPanel (turn.targets [index].pos);
			UpdateHitSuccessIndicator ();
		}
	}

	void UpdateHitSuccessIndicator() {
		int chance = 0;
		int amount = 0;
		Tile target = turn.targets [index];

		for(int i = 0; i < targeters.Length; i++) {
			if(targeters[i].IsTarget(target)) {
				HitRate hitRate = targeters[i].GetComponent<HitRate>();
				chance = hitRate.Calculate(target);

				BaseAbilityEffect effect = targeters[i].GetComponent<BaseAbilityEffect>();
				amount = effect.Predict(target);
				break;
			}
		}

		hitSuccessIndicator.SetStats (chance, amount);
	}
}
