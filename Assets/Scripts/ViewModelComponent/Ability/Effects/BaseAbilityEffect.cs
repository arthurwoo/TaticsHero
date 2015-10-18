using UnityEngine;
using System.Collections;

public abstract class BaseAbilityEffect : MonoBehaviour {

	public abstract int Predict(Tile target);
	public abstract void Apply(Tile target);
}
