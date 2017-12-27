using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {


	public GameObject detectorPrefab;
	public GameObject linePrefab;
	//public EdgeCollider2D ec2d;

	List<Vector2> points;
	Detector d;
	LineRenderer lr;

	public void UpdateLine (Vector2 mousePos) {
		if (points == null) {
			points = new List<Vector2> ();
			if (Input.GetKey (KeyCode.LeftShift)) {
				mousePos[0] = Mathf.RoundToInt((mousePos[0] - 0.125f) / 0.25f) * 0.25f + 0.125f;
				mousePos[1] = Mathf.RoundToInt((mousePos[1] - 0.125f) / 0.25f) * 0.25f + 0.125f;
			}
			SetPoint (mousePos);
			return;
		}

		// check if the mouse has moved enough for us to insert a new point
		// if it has: insert point at mouse position
		if (Vector2.Distance (points [points.Count - 1], mousePos) > .1f) {
			SetPoint (mousePos);
		}
	}

	public List<Vector2> getPoints() {
		return points;
	}

	public void InitDetector () {
		GameObject dGO = Instantiate (detectorPrefab);
		d = dGO.GetComponent<Detector> ();
		d.SetPointList (points);
	}

	public void InitLine () {
		GameObject lGO = Instantiate (linePrefab);
		lr = lGO.GetComponent<LineRenderer> ();
	}

	public void DestroyLine () {
		Destroy (lr.gameObject);
	}

	void SetPoint (Vector2 point) 
	{
		points.Add (point);
		lr.positionCount = points.Count;
		lr.SetPosition (points.Count - 1, point);
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			for (int i = 0; i < lr.positionCount; ++i) {
				float dist = Vector2.Distance (mousePos, lr.GetPosition (i));
				if (dist < 0.2f) {
					Destroy (lr.gameObject);
					Destroy (d.gameObject);
					Destroy (this.gameObject);
					break;
				}
			}
		}
	}
}