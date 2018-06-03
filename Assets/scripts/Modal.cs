﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modal : MonoBehaviour {
	public float modalFadeDuration;
	public Text messageText;

	private float timeSinceStartedFading;
	private bool isFading;
	private CanvasGroup canvasGroup;

	public static bool isModalOpen;

	void Start() {
		canvasGroup = GetComponent<CanvasGroup>();
		isFading = true;
	}
	
	void FixedUpdate() {
		if (isFading) {
			timeSinceStartedFading += Time.deltaTime;

			if (modalFadeDuration <= timeSinceStartedFading) {
				canvasGroup.alpha = 1;
				isFading = false;
			} else {
				canvasGroup.alpha = Mathf.Lerp(
					0, 1, timeSinceStartedFading / modalFadeDuration
				);
			}
		}
	}

	public static void OpenModal(
		GameObject modalPrefab, 
		string message, 
		Transform parent
	) {
		if (isModalOpen)
			return;

		GameObject modalObject = Instantiate(
			modalPrefab, new Vector3(0, 0, 0), Quaternion.identity
		);

		Modal modal = modalObject.GetComponent<Modal>();
		modal.messageText.text = message;
		modalObject.transform.SetParent(parent);

		RectTransform modalRectTransform
			= modalObject.GetComponent<RectTransform>();
		modalRectTransform.offsetMin = new Vector2(0, 0);
		modalRectTransform.offsetMax = new Vector2(0, 0);

		isModalOpen = true;
	}
}
