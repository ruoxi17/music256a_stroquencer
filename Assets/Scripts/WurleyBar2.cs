using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WurleyBar2 : GeneralBar {

	private static int barCount = 0;

	public WurleyBar2 () {
		WurleyBar2.barCount++;
	}

	~WurleyBar2 () {
		WurleyBar2.barCount--;
	}

	new void Start() {
		base.Start ();
		ci.RunCode (
			@"
// external vars
external Event hitEvent;
external float scale;

//globals
[48, 50, 52, 53, 55, 57, 59, 60, 62, 64, 65, 67, 71, 72] @=> int notes[];
// function that makes sound
fun void hitBar(Event e) {

	Wurley bar => NRev rvb => dac;
	rvb.mix(0.02);
    Std.mtof(notes[(scale * 14) $int]) => bar.freq; 
    .1 => bar.noteOn;
	0.5::second => now;
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
		if (WurleyBar2.barCount > 2 && timeDiff < 0.2) {
			Destroy (this.gameObject);
		}
	}
}
