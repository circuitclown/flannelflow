using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OKButton : MonoBehaviour {
	public CanvasGroup modalCanvasGroup;
	public float modalFadeDuration;

	private float timeSinceStartedFading;
	private bool isFading = false;
	
	public void HideModal() {
		isFading = true;
	}

	void FixedUpdate() {
		if (isFading) {
			timeSinceStartedFading += Time.deltaTime;

			if (modalFadeDuration <= timeSinceStartedFading) {
				modalCanvasGroup.alpha = 0;
				isFading = false;
				Modal.isModalOpen = false;
				Destroy(modalCanvasGroup.gameObject);
			} else {
				modalCanvasGroup.alpha = Mathf.Lerp(
					1, 0, timeSinceStartedFading / modalFadeDuration
				);
			}
		}
	}
}
