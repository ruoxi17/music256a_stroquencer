using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathCreator : MonoBehaviour {

	public GameObject pathPrefab;
	Path activePath;
	Vector2 startPos;
	Vector2 endPos;

	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			GameObject pathGO = Instantiate (pathPrefab);
			activePath = pathGO.GetComponent<Path> ();
			activePath.InitLine ();
			startPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}

		if (Input.GetMouseButtonUp (1)) {
			endPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (Vector2.Distance (startPos, endPos) < 0.5f && activePath.getPoints().Count < 20) {
				activePath.DestroyLine ();
				Destroy (activePath.gameObject);
			} 
			else {
				activePath.InitDetector ();
				activePath = null;
			}
		}

		if (activePath != null) {
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			activePath.UpdateLine (mousePos);
		}
	}
}
