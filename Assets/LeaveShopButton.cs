using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveShopButton : MonoBehaviour {
	public void GoToEndScene() {
		SceneManager.LoadScene(2);
	}
}
