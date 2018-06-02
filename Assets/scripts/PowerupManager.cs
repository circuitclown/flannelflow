using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {
	public float minDelayToPowerup;
	public float maxDelayToPowerup;

	public PowerupUser powerupUser;

	/*
		Indexes are powerup IDs, as defined in `PowerupUser`.

		NOTE: If a new powerup is added, it should also be added to the list
		in `PowerupUser`.
	 */
	public GameObject[] powerupPrefabs;

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

		GameObject prefab = null;
		int powerupID = Random.Range(0, powerupPrefabs.Length);
		prefab = powerupPrefabs[powerupID];

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
