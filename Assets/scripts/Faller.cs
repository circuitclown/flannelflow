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
	public float fadeSpeed;

	[HideInInspector]
	public bool isBumped = false;

	private bool isBumpedLeft;
	private bool isFading = false;
	private float fadingStartTime;

	private float missedThresholdWorldPoint;

	private SpriteRenderer spriteRenderer;

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

		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate() {
		float newPositionX = transform.position.x;
		bool isMissed = transform.position.y < missedThresholdWorldPoint;

		if (isFading) {
			Color newColor = spriteRenderer.color;
			newColor.a = (
			    1 - Mathf.Min(
					(Time.timeSinceLevelLoad - fadingStartTime) * fadeSpeed,
				    1
			    )
			);
			spriteRenderer.color = newColor;

			if (newColor.a == 0)
				Destroy(gameObject);
		}

		// Fading, soon-to-be-removed `Faller`s shouldn't also be bumped.
		if (!isFading && isBumped) {
			if (isBumpedLeft) {
				newPositionX 
					= transform.position.x - xMovementSpeed * Time.deltaTime;
			} else {
				newPositionX 
					= transform.position.x + xMovementSpeed * Time.deltaTime;
			}
		} 

		if (isMissed) {
			Destroy(gameObject);
			this.OnMissedThreshold();
			return;
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

	public void FadeAndDestroy() {
		isFading = true;
		fadingStartTime = Time.timeSinceLevelLoad;
	}

	abstract public void OnMissedThreshold();
	abstract public void OnCatch();
}
