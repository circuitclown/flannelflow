using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndHighscoreResult : MonoBehaviour {
	void Start () {
		Text text = GetComponent<Text>();
		string scoreString;
		string highscoreString;

		long lastScore = Storage.GetNumber("last score");
		long highscore = Storage.GetNumber("highscore");
		bool isNewHighscore = Storage.GetNumber("is new highscore") == 1;

		scoreString = (
		    "Score: " + lastScore.ToString()
		);

		if (isNewHighscore) {
			highscoreString = "New Highscore!";
		} else {
			highscoreString = (
				"Highscore: " + highscore.ToString()
			);
		}

		text.text = scoreString + "\n" + highscoreString;
	}
}
