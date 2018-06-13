using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {
	public float minDelayToPowerup;
	public float maxDelayToPowerup;

	public PowerupUser powerupUser;

	/*
		Indexes are powerup IDs, as defined in `PowerupUser`. For chances, a
		random value 0-1 inclusive is generated. If the value is less or equal 
		to the value for an index in `powerupChanceMaximum` and greater than 
		the value below, then that powerup is used.

		NOTE: If a new powerup is added, it should also be added to the list
		in `PowerupUser`.
	 */
	public GameObject[] powerupPrefabs;
	public float[] powerupChanceMaximum;

	private float timeSinceLastPowerup;
	private float currentDelayToNextPowerup;

	void Start() {
		currentDelayToNextPowerup = RandomDelay();
	}
	
	void FixedUpdate() {
		timeSinceLastPowerup += Time.deltaTime;

		if (currentDelayToNextPowerup <= timeSinceLastPowerup) {
			timeSinceLastPowerup = 0;
			currentDelayToNextPowerup = RandomDelay();
			InstantiatePowerup();
		}
	}

	private void InstantiatePowerup() {
		float randomNumber = Random.value;

		// `powerupID` should be set later, but if it isn't, pick evenly. 
		int powerupID = Random.Range(0, powerupPrefabs.Length);
		float randomValue = Random.value;

		// Disallow 0 in order to not give the first powerup a slight increase.
		while (randomValue == 0)
			randomValue = Random.value;

		for (int i = 0; i < powerupChanceMaximum.Length; i++) {
			if (
				(i == 0 ? 0 : powerupChanceMaximum[i - 1]) < randomValue
					&& randomValue <= powerupChanceMaximum[i]
			) {
				powerupID = i;
			}
		}
	
		GameObject prefab = powerupPrefabs[powerupID];

		// The prefab is automatically randomly placed by `Faller`.
		GameObject powerupObject = Instantiate(
			prefab,
			new Vector3(0, 0, 1),
			Quaternion.identity
		);

		Powerup powerup = powerupObject.GetComponent<Powerup>();
		powerup.powerupID = powerupID;
		powerup.powerupUser = powerupUser;

	}

	private float RandomDelay() {
		return Random.Range(
			minDelayToPowerup, 
			maxDelayToPowerup
		);
	}
}
