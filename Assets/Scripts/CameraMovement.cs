using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Transform initialTarget;
	Transform target;
	Vector3 moveTo;
	Vector3 velocity;

	void Start () {
		target = initialTarget;
	}

	void LateUpdate () {

		moveTo = new Vector3 (target.position.x, target.position.y, - 10f);
		transform.position = Vector3.SmoothDamp (transform.position, moveTo, ref velocity, 0.2f);
	}
}
