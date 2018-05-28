using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour {
	public List<Image> liveImages;

	void Start() {}
	void Update() {}

	public void LoseLife() {
		if (1 <= liveImages.Count) {
			liveImages[0].enabled = false;
			liveImages.RemoveAt(0);
		} else {
			SceneManager.LoadScene(2);
		}
	}
}
