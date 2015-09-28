using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Movement : MonoBehaviour {

	public int range {
		get {
			return stats[StatTypes.MOV];
		}
	}
	public int jumpHeight {
		get {
			return stats[StatTypes.JMP];
		}
	}
	protected Stats stats;
	protected Unit unit;
	protected Transform jumper;

	protected virtual void Start() {
		stats = GetComponent<Stats> ();
	}

	protected virtual void Awake() {
		unit = GetComponent<Unit> ();
		jumper = transform.FindChild ("Jumper");
	}

	public virtual List<Tile> GetTilesInRange(Board board) {
		List<Tile> retValue = board.Search (unit.tile, ExpandSearch);
		Filter (retValue);

		return retValue;
	}

	public abstract IEnumerator Traverse (Tile tile);

	protected virtual bool ExpandSearch(Tile from, Tile to) {
		return (from.distance + 1) <= range;
	}

	protected virtual void Filter(List<Tile> tiles) {
		for(int i = tiles.Count - 1; i >= 0; i--) {
			if(tiles[i].content != null)
				tiles.RemoveAt(i);
		}
	}

	protected virtual IEnumerator Turn(Directions dir) {
		TransformLocalEulerTweener t = (TransformLocalEulerTweener)transform.RotateToLocal (dir.ToEuler (), 0.25f, EasingEquations.EaseInOutQuad);

		if (Mathf.Approximately (t.startValue.y, 0f) && Mathf.Approximately (t.endValue.y, 270f))
			t.startValue = new Vector3 (t.startValue.x, 360f, t.startValue.z);
		else if (Mathf.Approximately (t.startValue.y, 270f) && Mathf.Approximately (t.endValue.y, 0f))
			t.endValue = new Vector3 (t.startValue.x, 360f, t.startValue.z);

		unit.dir = dir;

		while (t != null)
			yield return null;
	}
}
