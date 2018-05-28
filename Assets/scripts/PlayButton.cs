using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {
	void Start () {}
	void Update() {}

	public void GoToGameScene() {
		SceneManager.LoadScene(1);
	}
}
