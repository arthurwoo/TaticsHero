using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatPanel : MonoBehaviour {

	public Panel panel;
	public Sprite allyBackground;
	public Sprite enemyBackground;
	public Image background;
	public Image avatar;
	public Text nameLabel;
	public Text hpLabel;
	public Text mpLabel;
	public Text lvLabel;

	public void Display(GameObject obj) {
		background.sprite = UnityEngine.Random.value > 0.5f ? enemyBackground : allyBackground;
		nameLabel.text = obj.name;
		Stats stats = obj.GetComponent<Stats> ();
		if (stats) {
			hpLabel.text = string.Format("{0}/{1}", stats[StatTypes.HP], stats[StatTypes.MHP]);
			mpLabel.text = string.Format("{0}/{1}", stats[StatTypes.MP], stats[StatTypes.MMP]);
			lvLabel.text = string.Format("Lv. {0}", stats[StatTypes.LVL]);
		}
	}
}
