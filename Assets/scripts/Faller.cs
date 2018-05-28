using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Faller : MonoBehaviour {
	public float zRotationSpeed;
	public float yMovementSpeed;
	public float minXLocationScreenPoint;
	public float maxXLocationScreenPoint;
	public float xMovementSpeed;
	public float missedThresholdScreenPoint;

	[HideInInspector]
	public bool isBumped = false;

	private bool isBumpedLeft;

	private float missedThresholdWorldPoint;

	void Start() {
		transform.position = Camera.main.ViewportToWorldPoint(
			new Vector2(
				Random.Range(minXLocationScreenPoint, maxXLocationScreenPoint),
				1F
			)
		);

		missedThresholdWorldPoint = Camera.main.ScreenToWorldPoint(
			new Vector2(
				0F, 
				Camera.main.ViewportToScreenPoint(
					new Vector2(
						0F,
						0F
					)
				).y + missedThresholdScreenPoint
			)
		).y;
	}

	void FixedUpdate() {
		float newPositionX;
		bool isMissed = transform.position.y < missedThresholdWorldPoint;

		if (isBumped && !isMissed) {
			if (isBumpedLeft) {
				newPositionX 
					= transform.position.x - xMovementSpeed * Time.deltaTime;
			} else {
				newPositionX 
					= transform.position.x + xMovementSpeed * Time.deltaTime;
			}
		} else if (isMissed) {
			Destroy(gameObject);
			this.OnMissedThreshold();
			return;
		} else {
			newPositionX = transform.position.x;
		}

		transform.Rotate(new Vector3(0, 0, zRotationSpeed * Time.deltaTime));
		transform.position = new Vector2(
			newPositionX,
			transform.position.y - yMovementSpeed * Time.deltaTime
		);
	}

	public void Bump(bool isLeft) {
		isBumped = true;
		isBumpedLeft = isLeft;
	}

	abstract public void OnMissedThreshold();
}
