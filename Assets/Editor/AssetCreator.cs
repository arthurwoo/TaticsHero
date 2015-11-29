using UnityEngine;
using UnityEditor;

public class AssetCreator {

	[MenuItem("Assets/Create/ConversationData")]
	public static void CreateConversationData() {
		ScriptableObjectUtility.CreateAsset<ConversationData> ();
	}

	[MenuItem("Assets/Create/UnitRecipe")]
	public static void CreateUnitRecipe() {
		ScriptableObjectUtility.CreateAsset<UnitRecipe> ();
	}

	[MenuItem("Assets/Create/AbilityCatalogRecipe")]
	public static void CreateAbilityCatalogRecipe() {
		ScriptableObjectUtility.CreateAsset<AbilityCatalogRecipe> ();
	}
}
