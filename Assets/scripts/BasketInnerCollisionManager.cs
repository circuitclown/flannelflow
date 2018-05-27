using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketInnerCollisionManager : MonoBehaviour {
	void Start () {}
	void Update () {}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		GameObject collidedObject = otherCollider.gameObject;

		if (collidedObject.tag == "flannel") {
			Faller collidedFaller = collidedObject.GetComponent<Faller>();

			if (!collidedFaller.isBumped)
				Destroy(collidedObject);
		}
	}
}
