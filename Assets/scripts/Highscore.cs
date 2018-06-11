using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class Highscore : MonoBehaviour {
	public Score score;
	public Text highscoreText;

	[HideInInspector]
	public long highscore;

	private long originalHighscore;

	void Start() {
		if (PlayerPrefs.HasKey("highscore")) {
			originalHighscore = Storage.GetNumber("highscore");
			highscore = originalHighscore;
		} else {
			originalHighscore = -1;
			highscore = 0;
		}
	}
	
	void FixedUpdate() {
		if (score.currentScore <= originalHighscore) {
			highscoreText.text = "HS: " + originalHighscore.ToString();
		} else {
			highscore = score.currentScore;
			highscoreText.text = "New High Score!";
		}
	}

	void OnDestroy() {
		if (originalHighscore < highscore) {
			Storage.SetNumber("highscore", highscore);
			Storage.SetNumber("is new highscore", 1);
		} else {
			Storage.SetNumber("is new highscore", 0);
		}

		Social.localUser.Authenticate(OnAuthenticate);
	}

	void OnAuthenticate(bool wasSuccessful) {
		if (wasSuccessful)
			Social.ReportScore(highscore, "flannelflow_main", OnReportScore);
	}

	void OnReportScore(bool wasSuccessful) {}
}
