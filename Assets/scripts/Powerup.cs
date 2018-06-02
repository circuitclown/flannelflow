using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Powerup : Faller {
	public PowerupUser powerupUser;
	public int powerupID;

	override public void OnMissedThreshold() {}

	override public void OnCatch() {
		powerupUser.UsePowerup(powerupID);
	}
}
