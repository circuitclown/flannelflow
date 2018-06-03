using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TheNextFlow.UnityPlugins;

public class LeaderboardButton : MonoBehaviour {	
	void Start () {}
	void Update() {}

	public void GoToLeaderboard() {
		MobileNativePopups.OpenAlertDialog(
			"Sorry!", 
			"There was an error opening the leaderboard. Please try again "
				+ "later.", 
			"OK", 
			OnAcceptLeaderboardFailure
		);

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

	private void OnAcceptLeaderboardFailure() {}
}
