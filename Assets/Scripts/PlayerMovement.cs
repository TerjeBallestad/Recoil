using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rigidBody;
	public float movementSpeed, maxSpeed;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");
		HandleMovement (horizontal);

		
	}

	void HandleMovement(float horizontal){
		if (rigidBody.velocity.x < maxSpeed && rigidBody.velocity.x > -maxSpeed) {
			rigidBody.AddForce (new Vector2 (horizontal * movementSpeed, 0f), ForceMode2D.Impulse);
		} 
	}
}
