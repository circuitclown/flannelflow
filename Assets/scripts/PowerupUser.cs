using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupUser : MonoBehaviour {
	public Score score;
	public GameObject flannelPrefab;
	public FlannelManager flannelManager;
	public Basket basket;
	public float widenBasketGrowthDuration;

	/*
		Indexes correspond to powerup IDs, which are as follows:

		0: "x2",
		1: "freeze",
		2: "widen".

		NOTE: If a new powerup is added, it should also be added to the prefabs
		list in `PowerupManager`.
	 */
	public List<float> powerupDurations;

	private List<bool> activePowerups = new List<bool>();
	private List<float> timeSinceStartedPowerups = new List<float>();

	private float originalBasketWidth;

	public void Start() {
		for (int i = 0; i < powerupDurations.Count; i++) {
			activePowerups.Add(false);
			timeSinceStartedPowerups.Add(0);
		}
	}

	public void UsePowerup(int id) {
		// Active powerups shouldn't double-up.
		if (activePowerups[id] == true) {
			timeSinceStartedPowerups[id] = 0;
			return;
		}
		
		activePowerups[id] = true;
		timeSinceStartedPowerups[id] = 0;

		switch (id) {
		case 0:
			score.scoreFunctionC += score.currentScore;
			break;
		case 1:
			flannelPrefab.GetComponent<Flannel>().yMovementSpeed /= 2;

			GameObject[] flannelObjects = GameObject.FindGameObjectsWithTag(
				                              "flannel"
			                              );
			for (int i = 0; i < flannelObjects.Length; i++) {
				flannelObjects [i].GetComponent<Flannel>().yMovementSpeed /= 2;
			}

			flannelManager.delayFunctionA *= 2;
			flannelManager.delayFunctionB *= 2;
			flannelManager.delayFunctionC *= 2;

			break;
		case 2:
			// Resizing is actually done in `FixedUpdate`.
			originalBasketWidth = (
				basket.GetComponent<Transform>().localScale.x
			);
			break;
		}
	}

	void UnusePowerup(int id) {
		activePowerups[id] = false;
		timeSinceStartedPowerups[id] = 0;

		switch (id) {
		case 1:
			flannelPrefab.GetComponent<Flannel>().yMovementSpeed *= 2;

			GameObject[] flannelObjects = GameObject.FindGameObjectsWithTag(
				"flannel"
			);
			for (int i = 0; i < flannelObjects.Length; i++) {
				flannelObjects[i].GetComponent<Flannel>().yMovementSpeed *= 2;
			}

			flannelManager.delayFunctionA /= 2;
			flannelManager.delayFunctionB /= 2;
			flannelManager.delayFunctionC /= 2;

			break;
		}
	}

	void FixedUpdate() {
		for (int i = 0; i < activePowerups.Count; i++) {
			if (!activePowerups[i]) continue;

			timeSinceStartedPowerups[i] += Time.deltaTime;

			if (i == 2) {
				Transform basketTransform = basket.GetComponent<Transform>();
				float newWidth = 0;

				bool shouldBeGrowing = (
	                timeSinceStartedPowerups[i] < widenBasketGrowthDuration
	            );
				bool shouldBeShrinking = (
					powerupDurations[i] - widenBasketGrowthDuration 
						< timeSinceStartedPowerups[i]
				);
					
				if (shouldBeGrowing) {
					newWidth = originalBasketWidth * (
						Mathf.Lerp(
							1, 2, 
							timeSinceStartedPowerups[i]
								/ widenBasketGrowthDuration
						)
					);
				} else if (shouldBeShrinking) {
					newWidth = originalBasketWidth * (
						Mathf.Lerp(
							2, 1, 
							(timeSinceStartedPowerups[i] - (
								powerupDurations[i] - widenBasketGrowthDuration
							)) / widenBasketGrowthDuration
						)
					);
				} else {
					newWidth = originalBasketWidth * 2;
				}

				basketTransform.localScale = new Vector2(
					newWidth, basketTransform.localScale.y
				);
			}

			if (powerupDurations[i] <= timeSinceStartedPowerups[i])
				UnusePowerup(i);
		}
	}

	void OnDestroy() {
		for (int i = 0; i < activePowerups.Count; i++) {
			if (activePowerups[i])
				UnusePowerup(i);
		}
	}
}
