using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour {
	public int distanceFromBottom;
	public int scaleOfScreenWidth;
	public int xMovementSpeed;
	public float noXMovementThreshold;

	void Start() {
		Vector2 screenPointLocation = Camera.main.ViewportToScreenPoint(
			new Vector2(0.5F, 0F)
		);

		screenPointLocation.y += distanceFromBottom;

		transform.position = Camera.main.ScreenToWorldPoint(
			screenPointLocation
		);
	}

	void FixedUpdate() {
		Vector2 touchWorldPoint = Camera.main.ScreenToWorldPoint(
			Input.mousePosition
		);

		if (
			Mathf.Abs(touchWorldPoint.x - transform.position.x) 
				> noXMovementThreshold
		) {
			float newPositionX;

			if (touchWorldPoint.x > transform.position.x) {
				newPositionX
					= transform.position.x + xMovementSpeed * Time.deltaTime;
			} else {
				newPositionX
					= transform.position.x - xMovementSpeed * Time.deltaTime;
			}

			transform.position = new Vector2(
				newPositionX,
				transform.position.y
			);
		}
	}
}
