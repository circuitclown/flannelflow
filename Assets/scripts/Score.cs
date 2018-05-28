using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	/*
	    These values are used to create the score function of time, 
	    `s(t) = at^2 + bt + c`.
	 */
	public float scoreFunctionA;
	public float scoreFunctionB;
	public float scoreFunctionC;

	public Text scoreText;

	[HideInInspector]
	public int currentScore = 0;

	void Start() {}

	void FixedUpdate() {
		currentScore = ScoreForTime(Time.timeSinceLevelLoad);
		scoreText.text = currentScore.ToString();
	}

	void OnDestroy() {
		PlayerPrefs.SetInt("last score", currentScore);
		PlayerPrefs.SetInt(
			"coins", 
			PlayerPrefs.GetInt("coins") + currentScore
		);
	}

	int ScoreForTime(float time) {
		return (int) (
		    scoreFunctionA * Mathf.Pow(time, 2)
		   	    + scoreFunctionB * time
		   		+ scoreFunctionC
		);
	}
}
