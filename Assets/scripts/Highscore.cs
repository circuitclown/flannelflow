using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class Highscore : MonoBehaviour {
	public Score score;
	public Text highscoreText;

	[HideInInspector]
	public int highscore;

	private int originalHighscore;

	void Start() {
		if (PlayerPrefs.HasKey("highscore")) {
			originalHighscore = PlayerPrefs.GetInt("highscore");
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
			PlayerPrefs.SetInt("highscore", highscore);
			PlayerPrefs.SetInt("is new highscore", 1);
		} else {
			PlayerPrefs.SetInt("is new highscore", 0);
		}

		Social.localUser.Authenticate(OnAuthenticate);
		Social.ReportScore(highscore, "flannelflow-main", OnReportScore);
	}

	void OnAuthenticate(bool wasSuccessful) {}
	void OnReportScore(bool wasSuccessful) {}
}
