using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flannel : Faller {
	[HideInInspector]
	public Lives lives;

	override public void OnMissedThreshold() {
		lives.LoseLife();
	}
}
