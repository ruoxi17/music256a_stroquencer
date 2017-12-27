using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

	public TrailRenderer tr;
	public ParticleSystem ps;

	int lastPosIndex = 0;
	int count = 0;
	float trTime = 0;
	ParticleSystem.MinMaxCurve prLifeTime = 0;
	List<Vector2>points;

	public void SetPointList(List<Vector2> pl) {
		this.points = pl;
	}

	void Move() {
		if (points != null) {
			int currPosIndex = (lastPosIndex + 1) % points.Count;
			if (currPosIndex == 0) {
				tr.time = trTime;

				var main = ps.main;
				main.startLifetime = prLifeTime;
				ps.Play ();

			}
			transform.position = new Vector3 (points [currPosIndex] [0], points [currPosIndex] [1], 0);
			lastPosIndex = currPosIndex;
			if (currPosIndex == points.Count - 1){
				tr.time = -0.1f;
				var main = ps.main;
				main.startLifetime = -100.0f;
				ps.Stop ();
			}
		}
	}

	void Start() {
		trTime = tr.time;
		prLifeTime = ps.main.startLifetime;
	}

	void Update() {
		if ((count ++) % 2 == 0)
			Move ();
	}

	/*
	void OnTriggerEnter(Collider col) {

		if (col.tag == "TestBar") {
			Vector2 hitPos = new Vector2 (transform.position [0], transform.position [1]);
			Vector2 barPos = new Vector2 (col.transform.position [0], col.transform.position [1]);
			int direction = hitPos [1] > barPos [1] ? 1 : -1;
			float distance = Vector2.Distance (hitPos, barPos);
			float scale = (direction * distance + col.transform.localScale [1] / 2) / col.transform.localScale [1];

			col.gameObject.GetComponent<Renderer>().material.color = Color.white;
			ChuckInstance ci = col.gameObject.GetComponent<ChuckInstance> ();
			int freqIndex = (int) (scale * 8);
			string code = string.Concat("SinOsc foo => dac;", freqs[freqIndex]);
			code = string.Concat(code, " => foo.freq; 100::ms => now;");

			ci.RunCode (@code);
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "TestBar") {
			col.gameObject.GetComponent<Renderer>().material.color = Color.gray;
		}
	
	}
	*/

}
