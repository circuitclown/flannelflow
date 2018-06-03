using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {
	public SceneTransitioner sceneTransitioner;
	
	void Start () {}
	void Update() {}

	public void GoToGameScene() {
		sceneTransitioner.FadeOutAndTransition(1);
	}
}
