﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndHighscoreResult : MonoBehaviour {
	void Start () {
		Text text = GetComponent<Text>();
		string scoreString;
		string highscoreString;

		int lastScore = PlayerPrefs.GetInt("last score");
		int highscore = PlayerPrefs.GetInt("highscore");
		bool isNewHighscore = PlayerPrefs.GetInt("is new highscore") == 1;

		scoreString = (
		    "Score: " + lastScore.ToString()
		);

		if (isNewHighscore) {
			highscoreString = (
				"Highscore: " + highscore.ToString()
			);
		} else {
			highscoreString = "New Highscore!";
		}

		text.text = scoreString + "\n" + highscoreString;
	}
}