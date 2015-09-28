using UnityEngine;
using System.Collections;

public class RectTransformAnchorPositionTweener : Vector3Tweener {

	RectTransform rt;

	protected override void Awake() {
		base.Awake ();
		rt = transform as RectTransform;
	}

	protected override void OnUpdate(object sender, System.EventArgs e) {
		base.OnUpdate (sender, e);
		rt.anchoredPosition = currentValue;
	}
}
