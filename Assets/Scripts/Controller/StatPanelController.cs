using UnityEngine;
using System.Collections;

public class StatPanelController : MonoBehaviour {

	const string ShowKey = "Show";
	const string HideKey = "Hide";

	[SerializeField] StatPanel primaryPanel;
	[SerializeField] StatPanel secondaryPanel;

	Tweener primaryTransition;
	Tweener secondrayTransition;

	void Start() {
		if (primaryPanel.panel.CurrentPosition == null)
			primaryPanel.panel.SetPosition (HideKey, false);
		if (secondaryPanel.panel.CurrentPosition == null)
			secondaryPanel.panel.SetPosition (HideKey, false);
	}

	public void ShowPrimary(GameObject obj) {
		primaryPanel.Display (obj);
		MovePanel (primaryPanel, ShowKey, ref primaryTransition);
	}

	public void HidePrimary() {
		MovePanel (primaryPanel, HideKey, ref primaryTransition);
	}

	public void ShowSecondary(GameObject obj) {
		secondaryPanel.Display (obj);
		MovePanel (secondaryPanel, ShowKey, ref secondrayTransition);
	}

	public void HideSecondary() {
		MovePanel (secondaryPanel, HideKey, ref secondrayTransition);
	}

	void MovePanel(StatPanel obj, string pos, ref Tweener t) {
		Panel.Position target = obj.panel [pos];
		if (obj.panel.CurrentPosition != target) {
			if(t != null && t != null)
				t.Stop();
			t = obj.panel.SetPosition(pos, true);
			t.duration = 0.5f;
			t.equation = EasingEquations.EaseOutQuad;
		}
	}
}
