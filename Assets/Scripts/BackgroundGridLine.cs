using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGridLine : MonoBehaviour {

	public GameObject gridLinePrefab;

	bool gridOn = false;
	float width = 0.02f;

	void Start () {
		float currX = 0.0f;
		float currY = 0.0f;
		while (currX < 15) {
			GameObject glGO = Instantiate (gridLinePrefab);
			LineRenderer lr = glGO.GetComponent<LineRenderer> ();
			lr.positionCount = 2;
			lr.SetPosition (0, new Vector2 (currX, 400));
			lr.SetPosition (1, new Vector2 (currX, -400));
			GameObject glGO2 = Instantiate (gridLinePrefab);
			LineRenderer lr2 = glGO2.GetComponent<LineRenderer> ();
			lr2.positionCount = 2;
			lr2.SetPosition (0, new Vector2 (-currX, 400));
			lr2.SetPosition (1, new Vector2 (-currX, -400));
			currX += 0.5f;
			lr.startWidth = 0.0f;
			lr.endWidth = 0.0f;
			lr2.startWidth = 0.0f;
			lr2.endWidth = 0.0f;
		}

		while (currY < 10) {
			GameObject glGO = Instantiate (gridLinePrefab);
			LineRenderer lr = glGO.GetComponent<LineRenderer> ();
			lr.positionCount = 2;
			lr.SetPosition (0, new Vector2 (400, currY));
			lr.SetPosition (1, new Vector2 (-400, currY));
			GameObject glGO2 = Instantiate (gridLinePrefab);
			LineRenderer lr2 = glGO2.GetComponent<LineRenderer> ();
			lr2.positionCount = 2;
			lr2.SetPosition (0, new Vector2 (400, -currY));
			lr2.SetPosition (1, new Vector2 (-400, -currY));
			currY += 0.5f;
			lr.startWidth = 0.0f;
			lr.endWidth = 0.0f;
			lr2.startWidth = 0.0f;
			lr2.endWidth = 0.0f;
		}
	}

	void Update	() {
		if (Input.GetKeyDown (KeyCode.G)) {
			gridOn = !gridOn;
			width = 0.02f;
			if (!gridOn)
				width = 0.0f;
			GameObject[] lines = GameObject.FindGameObjectsWithTag ("GridLine");
			foreach (GameObject line in lines) {
				line.GetComponent<LineRenderer> ().startWidth = width;
				line.GetComponent<LineRenderer> ().endWidth = width;
			}
		}
	}
}
