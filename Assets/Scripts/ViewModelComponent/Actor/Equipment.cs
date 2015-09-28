using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment : MonoBehaviour {

	public const string EquippedNotification = "Equipment.EquippedNotification";
	public const string UnEquippedNotification = "Equipment.UnEquippedNotification";

	public IList<Equippable> items {
		get {
			return _items.AsReadOnly();
		}
	}
	List<Equippable> _items = new List<Equippable>();

	public void Equip(Equippable item, EquipSlots slots) {
		UnEquip (slots);

		_items.Add (item);
		item.transform.SetParent (transform);
		item.slots = slots;
		item.OnEquip ();

		this.PostNotification (EquippedNotification, item);
	}

	public void UnEquip(Equippable item) {
		item.OnUnEquip ();
		item.slots = EquipSlots.None;
		item.transform.SetParent (transform);
		_items.Remove (item);

		this.PostNotification (UnEquippedNotification, item);
	}

	public void UnEquip(EquipSlots slots) {
		for (int i = _items.Count - 1; i >= 0; i--) {
			Equippable item = _items[i];
			if((item.slots & slots) != EquipSlots.None)
				UnEquip(item);
		}
	}
}
