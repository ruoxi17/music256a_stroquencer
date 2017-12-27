using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecordBar : GeneralBar {

	private static int barCount = 0;
	private AudioSource aud;

	public RecordBar () {
		RecordBar.barCount++;
	}

	~RecordBar () {
		RecordBar.barCount--;
	}

	new void Start() {
		base.Start ();
		aud = GetComponent<AudioSource> ();
		/*
		ci.RunCode (
@"
// external vars
external Event hitEvent;
external Event startRecordEvent;
external Event endRecordEvent;
external float scale;
external float fileName;

0.0100 => fileName;

// globals
0 => int isRecording;

// function for recording - write adc to sndBuf
fun void recording(Event e) {
	while (true) {
		e => now;
		<<<""start recording..."">>>;
		1 => isRecording;
		adc => Gain g => WvOut2 out => blackhole;
		g.gain(0.6);
		now / 1::ms => fileName;
		me.dir() + fileName => string _capture;
		_capture => out.wavFilename;
		while (isRecording == 1) {
			0.4::second => now;
		}
		out.closeFile();
	}
}

// function for endRecord
fun void endRecording(Event e) {
	while (true) {
		e => now;
		0 => isRecording;
		<<<""finish recording!"">>>;
	}
}

// function that makes sound
fun void hitBar(Event e) {
	SndBuf sBuf => NRev rvb => dac;
	me.dir() + fileName + "".wav"" => sBuf.read;
	<<<fileName+"".wav"">>>;//<<<sBuf.length()>>>;
	rvb.mix(0.05);
	0 => sBuf.pos;
	1.5 * sBuf.length() => now;
}

fun void detectHit(Event e) {
	while (true) {
		e => now;
		spork ~ hitBar(e);
	}
}

spork ~ detectHit(hitEvent);

//spork ~ recording(startRecordEvent);
//spork ~ endRecording(endRecordEvent);

while (true) {
	1::second => now;
}
"
		);
		*/
	}

	new void OnMouseOver() {
		if (Input.GetKeyDown (KeyCode.C)) {
			GameObject newGO = Instantiate (this.gameObject);
			newGO.GetComponent<Renderer> ().material.color = matColor;
			newGO.GetComponent<AudioSource> ().clip = aud.clip;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			//ci.BroadcastEvent ("startRecordEvent");
			aud.clip = Microphone.Start("Built-in Microphone", false, 5, 22050);
		}
	}

	void OnMouseUp() {
		mouseUpTime = DateTime.Now;
		double timeDiff = (mouseUpTime - mouseDownTime).TotalSeconds;
		if (RecordBar.barCount > 1 && timeDiff < 0.2) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider col) {

		if (col.tag == "Detector") {
			gameObject.GetComponent<Renderer>().material.color = Color.white;
			AudioSource.PlayClipAtPoint(aud.clip, new Vector3(0,0,0));
		}
	}
}
