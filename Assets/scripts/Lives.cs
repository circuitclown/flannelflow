using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour {
	public List<Image> liveImages;
	public SceneTransitioner sceneTransitioner;

	void Start() {}
	void Update() {}

	public void LoseLife() {
		if (2 <= liveImages.Count) {
			liveImages[0].enabled = false;
			liveImages.RemoveAt(0);
		} else {
			sceneTransitioner.FadeOutAndTransition(2);
		}
	}
}
