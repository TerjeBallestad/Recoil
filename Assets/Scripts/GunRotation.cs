using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour {

	Vector3 mousePosition;
	Vector3 objectPosition;
	[HideInInspector]
	public float angle;

	
	void Update () {
			ArmRotation ();
	}

	void ArmRotation(){
		mousePosition = Input.mousePosition;
		mousePosition.z = 5.23f; // The distance between the camera and the object
		objectPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		mousePosition.x = mousePosition.x - objectPosition.x;
		mousePosition.y = mousePosition.y - objectPosition.y;
		angle = Mathf.Atan2 (mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
		if (!PlayerMovement.facingRight)
			angle += 180;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
	}
}
