using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats : MonoBehaviour {

	public int this[StatTypes s] {
		get {
			return _data[(int)s];
		}
		set {
			SetValue(s, value, true);
		}
	}
	int[] _data = new int[(int)StatTypes.Count];

	static Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string>();
	static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

	public static string WillChangeNotification(StatTypes type) {
		if (!_willChangeNotifications.ContainsKey (type))
			_willChangeNotifications.Add (type, string.Format ("Stats.{0}WillChange", type.ToString ()));

		return _willChangeNotifications[type];
	}

	public static string DidChangeNotification(StatTypes type) {
		if (!_didChangeNotifications.ContainsKey (type))
			_didChangeNotifications.Add (type, string.Format ("Stats.{0}DidChange", type.ToString ()));

		return _didChangeNotifications[type];
	}

	public void SetValue(StatTypes type, int value, bool allowExceptions) {
		int oldVlaue = this [type];
		if (oldVlaue == value)
			return;

		if (allowExceptions) {
			ValueChangeException exc = new ValueChangeException(oldVlaue, value);
			this.PostNotification (WillChangeNotification(type), exc);
			value = Mathf.FloorToInt (exc.GetModifiedValue());

			if(exc.toggle == false || value == oldVlaue)
				return;
		}

		_data [(int)type] = value;
		this.PostNotification (DidChangeNotification (type), oldVlaue);
	}
}
