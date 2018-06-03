using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlannelManager : MonoBehaviour {
	public GameObject flannelPrefab;
	public Lives lives;

	/*
	    These values are used to create the delay function of flannels,
	    `d(f) = a(1/f)^2 + b(1/f) + c`.
	 */
	public float delayFunctionA;
	public float delayFunctionB;
	public float delayFunctionC;

	public float initialDelay;

	private int numberOfFlannels = 0;
	private float timeSinceLastFlannel;
	private float currentDelayToNextFlannel;

	/*
	    NOTE: See `FlannelCell` for the list of IDs and colors.

		Indexes correspond to flannel IDs.
	 */
	public Sprite[] flannelSprites;

	void Start() {
		currentDelayToNextFlannel = initialDelay;

		flannelPrefab.GetComponent<SpriteRenderer>().sprite = flannelSprites[
			PlayerPrefs.GetInt("selected flannel")
		];
	}
	
	void FixedUpdate() {
		timeSinceLastFlannel += Time.deltaTime;

		if (currentDelayToNextFlannel <= timeSinceLastFlannel) {
			InstantiateFlannel();
			timeSinceLastFlannel = 0;
			numberOfFlannels++;
			currentDelayToNextFlannel = DelayToNextFlannelForFlannels(
				numberOfFlannels
			);
			Debug.Log("a" + delayFunctionA + "b" + delayFunctionB + "C" + delayFunctionC + "D" + currentDelayToNextFlannel);
		}
	}

	void InstantiateFlannel() {
		/*
		   Flannels are automatically, randomly placed by `Flannel` when 
		   created.
		 */
		GameObject flannelObject = Instantiate(
			flannelPrefab,
			new Vector3(0, 0, 1),
			Quaternion.identity
		) as GameObject;

		Flannel flannel = flannelObject.GetComponent<Flannel>();
		flannel.lives = lives;
	}

	float DelayToNextFlannelForFlannels(int numberOfFlannels) {
		return (
			delayFunctionA * Mathf.Pow((1 / numberOfFlannels), 2)
				+ delayFunctionB * (1 / numberOfFlannels)
				+ delayFunctionC
		);
	}
}
