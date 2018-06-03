using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour {
	public float distanceFromBottom;
	public float scaleOfScreenWidth;
	public float xMovementSpeed;

	void Start() {
		Vector2 screenPointLocation = Camera.main.ViewportToScreenPoint(
			new Vector2(0.5F, 0F)
		);

		screenPointLocation.y += distanceFromBottom;

		Vector2 worldPointLocation = Camera.main.ScreenToWorldPoint(
			screenPointLocation
		);

		/*
			I don't know why, but this redundant `Vector2` creation makes it
			visible before it is moved.
		 */
		transform.position = new Vector2(
			worldPointLocation.x,
			worldPointLocation.y
		);
	}

	void FixedUpdate() {
		if (Input.touchCount > 0) {
			MoveBasedOnTouch(Input.GetTouch(0));
		}
	}

	private void MoveBasedOnTouch(Touch touch) {
		Vector2 touchWorldPoint = Camera.main.ScreenToWorldPoint(
			touch.position
		);

		float newPositionX;
		float xMovement = xMovementSpeed * Time.deltaTime;

		if (
			xMovement < Mathf.Abs(
				touchWorldPoint.x - transform.position.x
			)
		) {
			if (touchWorldPoint.x > transform.position.x) {
				newPositionX
				= transform.position.x + xMovement;
			} else {
				newPositionX
				= transform.position.x - xMovement;
			}
		} else {
			/*
				If it can't reach the point through normal speed, just place it
				there. This improves accuracy and also prevents possible 
				jittering.
			 */
			newPositionX = touchWorldPoint.x;
		}

		transform.position = new Vector2(
			newPositionX,
			transform.position.y
		);
	}
}
