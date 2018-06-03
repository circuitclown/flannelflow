using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.GameCenter;

public class LeaderboardButton : MonoBehaviour {
	public GameObject modalPrefab;

	void Start () {}
	void Update() {}

	public void GoToLeaderboard() {
		Social.localUser.Authenticate(OnAuthenticate);
	}

	void OnAuthenticate(bool wasSuccessful) {
		if (wasSuccessful && Social.localUser.authenticated) {
			GameCenterPlatform.ShowLeaderboardUI(
				"flannelflow_main", 
				UnityEngine.SocialPlatforms.TimeScope.AllTime
			);
		} else {
			Modal.OpenModal(
				modalPrefab, 
				"Sorry!\nThere was an error opening the leaderboard.",
				transform.parent
			);
		}
	}

	private void OnAcceptLeaderboardFailure() {}
}
