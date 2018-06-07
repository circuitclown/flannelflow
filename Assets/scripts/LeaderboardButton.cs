using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.GameCenter;

public class LeaderboardButton : MonoBehaviour {
	public GameObject modalPrefab;
	public float timeoutLength;

	private bool leaderboardAuthenticationHasSucceeded;

	void Start () {}
	void Update() {}

	public void GoToLeaderboard() {
		// So far, it is `false`. It can change later.
		leaderboardAuthenticationHasSucceeded = false;

		Modal.ChangeOrOpenModal(
			modalPrefab, 
			"Loading...",
			transform.parent
		);

		Social.localUser.Authenticate(OnAuthenticate);

		Invoke("ShowFailureLeaderboard", timeoutLength);
	}

	void OnAuthenticate(bool wasSuccessful) {
		if (wasSuccessful && Social.localUser.authenticated) {
			leaderboardAuthenticationHasSucceeded = true;
			Modal.MaybeHideModal();
			GameCenterPlatform.ShowLeaderboardUI(
				"flannelflow_main", 
				UnityEngine.SocialPlatforms.TimeScope.AllTime
			);
		} else {
			leaderboardAuthenticationHasSucceeded = false;
			ShowFailureLeaderboard();
		}
	}

	private void ShowFailureLeaderboard() {
		if (leaderboardAuthenticationHasSucceeded)
			return;
		Modal.ChangeOrOpenModal(
			modalPrefab, 
			"Sorry!\nThere was an error opening the leaderboard. You may "
				+ "have to log in to GameCenter in Settings.",
			transform.parent
		);
	}

	private void OnAcceptLeaderboardFailure() {}
}
