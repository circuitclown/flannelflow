using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.GameCenter;
using TheNextFlow.UnityPlugins;

public class LeaderboardButton : MonoBehaviour {	
	void Start () {}
	void Update() {}

	public void GoToLeaderboard() {
		Social.localUser.Authenticate(OnAuthenticate);
	}

	void OnAuthenticate(bool wasSuccessful) {
		if (wasSuccessful) {
			GameCenterPlatform.ShowLeaderboardUI(
				"flannelflow_main", 
				UnityEngine.SocialPlatforms.TimeScope.AllTime
			);
		} else {
			MobileNativePopups.OpenAlertDialog(
				"Sorry!", 
				"There was an error opening the leaderboard. Please try again "
					+ "later.", 
				"OK", 
				() => {}
			);
		}
	}

	private void OnAcceptLeaderboardFailure() {}
}
