﻿using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardCreator : MonoBehaviour {

	[SerializeField] GameObject tileViewPrefab;
	[SerializeField] GameObject tileSelectionIndicatorPrefab;
	[SerializeField] int width = 10;
	[SerializeField] int depth = 10;
	[SerializeField] int height = 8;
	[SerializeField] Point pos;
	[SerializeField] LevelData levelData;
	Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

	Transform marker {
		get {
			if (_marker == null) {
				GameObject instance = Instantiate(tileSelectionIndicatorPrefab) as GameObject;
				instance.transform.parent = transform;
				_marker = instance.transform;
			}
			return _marker;
		}
	}
	Transform _marker;

	public void Grow() {
		GrowSingle (pos);
	}

	public void Shrink() {
		ShrinkSingle (pos);
	}

	public void GrowArea() {
		Rect r = RandomRect ();
		GrowRect (r);
	}

	public void ShrinkArea() {
		Rect r = RandomRect ();
		ShrinkRect (r);
	}

	public void UpdateMarker() {
		Tile t = tiles.ContainsKey (pos) ? tiles [pos] : null;
		marker.localPosition = t != null ? t.center : new Vector3 (pos.x, 0, pos.y);
	}

	public void Clear() {
		for (int i = transform.childCount - 1; i >= 0; i--)
			DestroyImmediate (transform.GetChild (i).gameObject);	
		tiles.Clear ();
	}

	public void Save() {
		string filePath = Application.dataPath + "/Resources/Levels";
		if (!Directory.Exists (filePath))
			CreateSaveDirectory ();

		LevelData board = ScriptableObject.CreateInstance<LevelData> ();
		board.tiles = new List<Vector3> (tiles.Count);
		foreach (Tile t in tiles.Values)
			board.tiles.Add (new Vector3 (t.pos.x, t.height, t.pos.y));

		string fileName = string.Format ("Assets/Resources/Levels/{1}.asset", filePath, name);
		AssetDatabase.CreateAsset (board, fileName);
	}

	public void Load() {
		Clear ();
		if (levelData == null)
			return;

		foreach (Vector3 v in levelData.tiles) {
			Tile t = Create();
			t.Load (v);
			tiles.Add (t.pos, t);
		}
	}

	void CreateSaveDirectory() {
		string filePath = Application.dataPath + "/Resources";
		if (!Directory.Exists (filePath))
			AssetDatabase.CreateFolder ("Assets", "Resources");
		filePath += "/Levels";
		if (!Directory.Exists (filePath))
			AssetDatabase.CreateFolder ("Assets/Resources", "Levels");
		AssetDatabase.Refresh ();
	}

	Rect RandomRect() {
		int x = Random.Range (0, width);
		int y = Random.Range (0, depth);
		int w = Random.Range (1, width - x + 1);
		int h = Random.Range (1, height - y + 1);
		return new Rect (x, y, w, h);
	}

	void GrowRect(Rect rect) {
		for (int y = (int)rect.yMin; y < (int)rect.yMax; y++) {
			for (int x = (int)rect.xMin; x < (int)rect.xMax; x++) {
				Point p = new Point(x, y);
				GrowSingle(p);
			}
		}
	}

	void ShrinkRect(Rect rect) {
		for (int y = (int)rect.yMin; y < (int)rect.yMax; y++) {
			for (int x = (int)rect.xMin; x < (int)rect.xMax; x++) {
				Point p = new Point(x, y);
				ShrinkSingle(p);
			}
		}
	}

	Tile Create() {
		GameObject instance = Instantiate (tileViewPrefab) as GameObject;
		instance.transform.parent = transform;
		return instance.GetComponent<Tile> ();
	}

	Tile GetOrCreate(Point p) {
		if (tiles.ContainsKey (p))
			return tiles [p];

		Tile t = Create ();
		t.Load (p, 0);
		tiles.Add (p, t);

		return t;
	}

	void GrowSingle(Point p) {
		Tile t = GetOrCreate (p);
		if (t.height < height)
			t.Grow ();
	}

	void ShrinkSingle(Point p) {
		if (!tiles.ContainsKey (p))
			return;

		Tile t = tiles [p];
		t.Shrink ();

		if (t.height <= 0) {
			tiles.Remove (p);
			DestroyImmediate (t.gameObject);
		}
	}
}
