using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardButton : MonoBehaviour {
	public SceneTransitioner sceneTransitioner;
	
	void Start () {}
	void Update() {}

	public void GoToLeaderboard() {
		Social.localUser.Authenticate(OnAuthenticate);
	}

	void OnAuthenticate(bool wasSuccessful) {
		if (wasSuccessful) {
			Social.ShowLeaderboardUI();
		} else {
			TheNextFlow.UnityPlugins.MobileNativePopups.OpenAlertDialog(
				"Sorry!", 
				"There was an error opening the leaderboard. Please try again "
					+ "later.", 
				"OK", 
				null
			);
		}
	}
}
