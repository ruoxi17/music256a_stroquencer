using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WurleyBar : GeneralBar {

	private static int barCount = 0;

	public WurleyBar () {
		WurleyBar.barCount++;
	}

	~WurleyBar () {
		WurleyBar.barCount--;
	}

	new void Start() {
		base.Start ();
		ci.RunCode (
			@"
// external vars
external Event hitEvent;
external float scale;

//globals
48 => int baseNote;
// function that makes sound
fun void hitBar(Event e) {

	Wurley bar => NRev rvb => dac;
	rvb.mix(0.02);
    Std.mtof((scale * 24) $int + baseNote) => bar.freq; 
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
		if (WurleyBar.barCount > 2 && timeDiff < 0.2) {
			Destroy (this.gameObject);
		}
	}
}
