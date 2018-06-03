using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BasicAdPlayer : MonoBehaviour {
	public int gamesPerBasicAd;

	void Start () {
		Advertisement.Initialize("2602297");
	}

	public void MaybePlayAd() {
		int newGamesSinceBasicAd;

		if (gamesPerBasicAd <= PlayerPrefs.GetInt("games since basic ad")) {
			Advertisement.Show();
			newGamesSinceBasicAd = (
			    PlayerPrefs.GetInt("games since basic ad") - gamesPerBasicAd
			);

		} else {
			newGamesSinceBasicAd = (
				PlayerPrefs.GetInt("games since basic ad") + 1
			);
		}

		PlayerPrefs.SetInt("games since basic ad", newGamesSinceBasicAd);
	}
}
