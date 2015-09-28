using UnityEngine;
using System.Collections;

public class MultValueModifier : ValueModifier {

	public readonly float toMutiply;

	public MultValueModifier(int sortOrder, float toMutiply):base(sortOrder) {
		this.toMutiply = toMutiply;
	}

	public override float Modify (float fromValue, float toValue) {
		return toValue * toMutiply;
	}
}
