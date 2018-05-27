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

	void Start () {}
	void FixedUpdate () {
		currentScore = ScoreForFixedTime(Time.fixedTime);
		scoreText.text = currentScore.ToString();
	}

	int ScoreForFixedTime (float fixedTime) {
		return (int) (
		    scoreFunctionA * Mathf.Pow(fixedTime, 2)
		   	    + scoreFunctionB * fixedTime
		   		+ scoreFunctionC
		);
	}
}
