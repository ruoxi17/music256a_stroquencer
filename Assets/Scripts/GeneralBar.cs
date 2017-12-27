using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneralBar : MonoBehaviour {

	protected ChuckInstance ci;
	protected DateTime mouseDownTime = DateTime.Now;
	protected DateTime mouseUpTime;
	protected Color matColor;

	private Renderer rend;


	//float [] freqs = {261.63f, 293.66f, 329.63f, 349.23f, 392.00f, 440.0f, 493.88f, 523.25f};

	protected void Start() {
		rend = GetComponent<Renderer> ();
		ci = GetComponent<ChuckInstance> ();
		matColor = rend.material.color;
		GetComponent<ParticleSystem> ().Stop ();
	}

	void OnMouseEnter() {
		rend.material.color = Color.white;
		// Debug.Log (barCount);
	}

	void OnMouseDrag() {
		Vector2 newPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		float length = transform.localScale [1];
		float offSetX = newPos [0] - transform.position [0];
		float offSetY = newPos [1] - transform.position [1];
		if (Mathf.Abs (offSetY) / length > 0.3 || Mathf.Abs (offSetX) / length > 0.3) { // drag to rotate
			float angle = Mathf.Atan2(offSetY, offSetX) * Mathf.Rad2Deg;
			if(Input.GetKey(KeyCode.LeftShift)) {
				angle = Mathf.RoundToInt(angle / 15) * 15;
			}
			transform.eulerAngles = new Vector3 (0, 0, angle-90);
		} else{ // drag to move
			if (Input.GetKey (KeyCode.LeftShift)) {
				newPos[0] = Mathf.RoundToInt((newPos[0] - 0.125f) / 0.25f) * 0.25f + 0.125f;
				newPos[1] = Mathf.RoundToInt((newPos[1] - 0.125f) / 0.25f) * 0.25f + 0.125f;
			}
			transform.position = newPos;
		} 
	}

	void OnMouseExit() {
		rend.material.color = matColor;
	}

	void OnMouseDown() {
		mouseDownTime = DateTime.Now;
	}

	protected void OnMouseOver() {
		if (Input.GetKeyDown (KeyCode.C)) {
			GameObject newGO = Instantiate (this.gameObject);
			newGO.GetComponent<Renderer> ().material.color = matColor;
		} 
	}

	void OnTriggerEnter(Collider col) {

		if (col.tag == "Detector") {
			Vector2 hitPos = new Vector2 (col.transform.position [0], col.transform.position [1]);
			Vector2 barPos = new Vector2 (transform.position [0], transform.position [1]);
			int direction = hitPos [1] > barPos [1] ? 1 : -1;
			float distance = Vector2.Distance (hitPos, barPos);
			float scale = (direction * distance + transform.localScale [1] / 2) / transform.localScale [1];

			gameObject.GetComponent<Renderer>().material.color = Color.white;

			/*
			int freqIndex = (int) (scale * 8);
			if (freqIndex == 8)
				freqIndex--;
			*/
			GetComponent<ParticleSystem> ().Play ();
			ci.SetFloat ("scale", scale);
			ci.BroadcastEvent ("hitEvent");
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "Detector") {
			gameObject.GetComponent<Renderer>().material.color = matColor;
			GetComponent<ParticleSystem> ().Stop ();
		}

	}

}
