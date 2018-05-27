using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketOuterCollisionManager : MonoBehaviour {
	void Start () {}
	void Update () {}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		GameObject collidedObject = otherCollider.gameObject;

		if (collidedObject.tag == "flannel") {
			Faller collidedFaller = collidedObject.GetComponent<Faller>();

			collidedFaller.Bump(
				collidedObject.transform.position.x < transform.position.x
			);
		}
	}
}
