using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveShopButton : MonoBehaviour {
	public SceneTransitioner sceneTransitioner;

	public void GoToEndScene() {
		sceneTransitioner.FadeOutAndTransition(2);
	}
}
