using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModalBar : GeneralBar {

	private static int barCount = 0;

	public ModalBar () {
		ModalBar.barCount++;
	}

	~ModalBar () {
		ModalBar.barCount--;
	}

	new void Start() {
		base.Start ();
		ci.RunCode (
@"
// external vars
external Event hitEvent;
external float scale;

[48, 50, 52, 53, 55, 57, 59, 60, 62, 64, 65, 67, 71, 72] @=> int notes[];

// function that makes sound
fun void hitBar(Event e) {

	ModalBar bar => NRev rvb => dac;
	rvb.mix(0.05);
	4 => bar.preset;
    0.2 => bar.stickHardness;
    0.2 => bar.strikePosition;
    0.5 => bar.vibratoGain;
    0 => bar.vibratoFreq;
    0.8 => bar.volume;
    0.5 => bar.directGain;
    0.3 => bar.masterGain;
    Std.mtof(notes[(scale * 14) $int]) => bar.freq; 
    .5 => bar.noteOn;
	.75::second => now;

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
		if (ModalBar.barCount > 2 && timeDiff < 0.2) {
			Destroy (this.gameObject);
		}
	}
}
