using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopButton : MonoBehaviour {
	public SceneTransitioner sceneTransitioner;

	public void GoToShopScene() {
		sceneTransitioner.FadeOutAndTransition(3);
	}
}
