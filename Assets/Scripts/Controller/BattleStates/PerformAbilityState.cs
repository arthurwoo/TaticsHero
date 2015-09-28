using UnityEngine;
using System.Collections;

public class PerformAbilityState : BattleState {

	public override void Enter () {
		base.Enter ();
		turn.hasUnitActed = true;
		if (turn.hasUnitMoved)
			turn.lockMove = true;
		StartCoroutine (Animate ());
	}

	IEnumerator Animate() {
		//TODO play animations
		yield return null;
		TemporaryAttackExample ();

		if (turn.hasUnitMoved)
			owner.ChangeState<EndFacingState> ();
		else
			owner.ChangeState<CommandSelectionState> ();
	}

	void TemporaryAttackExample() {
		for(int i = 0; i < turn.targets.Count; i++) {
			GameObject obj = turn.targets[i].content;
			Stats stats = obj != null ? obj.GetComponentInChildren<Stats>() : null;
			if(stats != null) {
				stats[StatTypes.HP] -= 50;
				if(stats[StatTypes.HP] <= 0)
					Debug.Log("KO'd Unit", obj);
			}
		}
	}
}
