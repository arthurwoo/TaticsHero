using UnityEngine;
using UnityEditor;

public class AssetCreator {

	[MenuItem("Assets/Create/ConversationData")]
	public static void CreateConversationData() {
		ScriptableObjectUtility.CreateAsset<ConversationData> ();
	}
}
