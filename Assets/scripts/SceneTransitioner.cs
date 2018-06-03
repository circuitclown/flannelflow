using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour {
	public float fadeDuration;

	private Image image;
	private Transform transform;
	private float timeSinceStartedFade;
	private bool isFading;
	private bool isFadingOut;
	private int sceneToTransitionTo = -1;

	void Start() {
		image = GetComponent<Image>();
		transform = GetComponent<Transform>();

		FadeIn();
	}

	void FadeIn() {
		if (!(isFading && !isFadingOut)) {
			transform.SetAsLastSibling();
			isFading = true;
			timeSinceStartedFade = 0;
			isFadingOut = false;
		}
	}

	public void FadeOutAndTransition(int sceneToTransitionTo) {
		if (!(isFading && isFadingOut)) {
			transform.SetAsLastSibling();
			isFading = true;
			timeSinceStartedFade = 0;
			isFadingOut = true;
			this.sceneToTransitionTo = sceneToTransitionTo;
		}
	}

	void FixedUpdate() {
		timeSinceStartedFade += Time.deltaTime;

		if (isFading) {
			float newAlpha = 1;

			if (!isFadingOut) {
				if (fadeDuration <= timeSinceStartedFade) {
					newAlpha = 0;
					transform.SetAsFirstSibling();
				} else {
					newAlpha = Mathf.Lerp(
						1, 0, timeSinceStartedFade / fadeDuration
					);
				}
			} else {
				if (fadeDuration <= timeSinceStartedFade) {
					newAlpha = 1;
					if (sceneToTransitionTo != -1) {
						SceneManager.LoadScene(sceneToTransitionTo);
					}
				} else {
					newAlpha = Mathf.Lerp(
						0, 1, timeSinceStartedFade / fadeDuration
					);
				}
			}

			image.color = new Color(
				image.color.r, image.color.g, image.color.b, newAlpha
			);
		}
	}
}
