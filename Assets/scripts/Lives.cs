using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour {
	public List<Image> liveImages;
	public SceneTransitioner sceneTransitioner;
	public BasicAdPlayer basicAdPlayer;


	void Start() {}
	void Update() {}

	public void LoseLife() {
		liveImages[0].enabled = false;
		liveImages.RemoveAt(0);

		if (liveImages.Count <= 0) {
			sceneTransitioner.FadeOutAndTransition(2);
			basicAdPlayer.MaybePlayAd();
		}
	}
}
