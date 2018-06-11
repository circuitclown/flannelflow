using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsStatus : MonoBehaviour {
	void Start () {
		UpdateCoinsAmount(Storage.GetNumber("coins"));
	}

	public void UpdateCoinsAmount(long newAmount) {
		/*
			This does not update `PlayerPrefs`, which should be updated 
			separately.
		 */

		Text text = GetComponent<Text>();
		long coinsAmount = newAmount;
		string coinsText = coinsAmount == 0 ? "no" : coinsAmount.ToString();
		text.text = "You have " + coinsText + " coins.";
	}
}