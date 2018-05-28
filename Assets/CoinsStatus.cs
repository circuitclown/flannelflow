using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsStatus : MonoBehaviour {
	void Start () {
		Text text = GetComponent<Text>();
		int coinsAmount = PlayerPrefs.GetInt("coins");
		string coinsText = coinsAmount == 0 ? "no" : coinsAmount.ToString();
		text.text = "You have " + coinsText + " coins.";
	}
}