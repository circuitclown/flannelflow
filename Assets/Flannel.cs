using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flannel : MonoBehaviour {
	public float zRotationSpeed;
	public int yMovementSpeed;
	public float minXLocationScreenPoint;
	public float maxXLocationScreenPoint;
	public float xMovementSpeed;

	[HideInInspector]
	public bool isBumped = false;
	private bool isBumpedLeft;

	void Start() {
		transform.position = Camera.main.ViewportToWorldPoint(
			new Vector2(
				Random.Range(minXLocationScreenPoint, maxXLocationScreenPoint),
				1F
			)
		);
	}

	void FixedUpdate() {
		float newPositionX;

		if (isBumped) {
			if (isBumpedLeft) {
				newPositionX 
					= transform.position.x - xMovementSpeed * Time.deltaTime;
			} else {
				newPositionX 
					= transform.position.x + xMovementSpeed * Time.deltaTime;
			}
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
}
