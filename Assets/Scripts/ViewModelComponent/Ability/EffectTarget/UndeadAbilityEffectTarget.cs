using UnityEngine;
using System.Collections;

public class UndeadAbilityEffectTarget : AbilityEffectTarget {

	public bool toggle;

	public override bool IsTarget (Tile tile) {
		if(tile == null || tile.content == null)
			return false;

		bool hasComponent = tile.content.GetComponent<Undead>() != null;
		if (hasComponent != toggle)
			return false;

		Stats s = tile.content.GetComponent<Stats> ();
		return s != null && s [StatTypes.HP] > 0;
	}
}
