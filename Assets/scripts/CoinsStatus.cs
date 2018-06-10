using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsStatus : MonoBehaviour {
	void Start () {
		UpdateCoinsAmount(PlayerPrefs.GetInt("coins"));
	}

	public void UpdateCoinsAmount(int newAmount) {
		/*
			This does not update `PlayerPrefs`, which should be updated 
			separately.
		 */

		Text text = GetComponent<Text>();
		int coinsAmount = newAmount;
		string coinsText = coinsAmount == 0 ? "no" : coinsAmount.ToString();
		text.text = "You have " + coinsText + " coins.";
	}
}