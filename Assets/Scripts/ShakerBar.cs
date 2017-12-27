using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShakerBar : GeneralBar {

	private static int barCount = 0;

	public ShakerBar () {
		ShakerBar.barCount++;
	}

	~ShakerBar () {
		ShakerBar.barCount--;
	}

	new void Start() {
		base.Start ();
		ci.RunCode (
@"
// external vars
external Event hitEvent;
external float scale;

// function that makes sound
fun void hitBar(Event e) {
    Shakers s => NRev rvb => dac;
	rvb.mix(0.05);
	scale * 130 + 10 => float myTime;
	0.6 => s.gain;
	1.0 => s.noteOn;
    myTime::ms => now;
    1.0 => s.noteOff;
    myTime::ms => now;   
    (scale * 22) $int => s.which;
}

// for trigger event
while (true) {
	hitEvent => now;
	spork ~ hitBar(hitEvent);
}		
"
		);
	}

	void OnMouseUp() {
		mouseUpTime = DateTime.Now;
		double timeDiff = (mouseUpTime - mouseDownTime).TotalSeconds;
		if (ShakerBar.barCount > 2 && timeDiff < 0.2) {
			Destroy (this.gameObject);
		}
	}
}
