using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Party = System.Collections.Generic.List<UnityEngine.GameObject>;

public static class ExperienceManager {

	const float minLevelBonus = 1.5f;
	const float maxLevelBonus = 0.5f;

	public static void AwardExperience(int amount, Party party) {
		List<Rank> ranks = new List<Rank> (party.Count);
		for(int i = 0; i < party.Count; i++) {
			Rank r = party[i].GetComponent<Rank>();
			if(r != null)
				ranks.Add(r);
		}

		int min = int.MaxValue;
		int max = int.MinValue;
		for (int i = ranks.Count - 1; i >= 0; i--) {
			min = Mathf.Min (ranks[i].LVL, min);
			max = Mathf.Max (ranks[i].LVL, max);
		}

		float[] weights = new float[ranks.Count];
		float summedWeights = 0;
		for (int i = ranks.Count - 1; i >= 0; i--) {
			float percent = (float)(ranks[i].LVL - min) / (max - min);
			weights[i] = Mathf.Lerp(minLevelBonus, maxLevelBonus, percent);
			summedWeights += weights[i];
		}

		for(int i = ranks.Count - 1; i >= 0; i--) {
			int subAmount = Mathf.FloorToInt((weights[i] / summedWeights) * amount);
			ranks[i].EXP += subAmount;
		}
	}
}
