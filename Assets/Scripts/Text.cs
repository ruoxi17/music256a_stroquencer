using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.H)) {
			if (GetComponent<Text> ().transform.position [0] == 20) {
				GetComponent<Text> ().transform.position = new Vector3 (100, 0, 0);
			} else {
				GetComponent<Text> ().transform.position = new Vector3 (20, 0, 0);
			}
		}
	}
}
