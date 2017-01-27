using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rigidBody;
	public float movementSpeed, startJumpForce, jumpForce;
	public static bool facingRight, canJump;
	bool grounded;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		facingRight = true;
		canJump = true;
		jumpForce = startJumpForce;
	}

	void Update(){

		// Initial Jump
		if (Input.GetButtonDown("Jump") && canJump){
			Jump (startJumpForce);
			canJump = false;
		}

		if (Input.GetButtonUp ("Jump")) {
			jumpForce = 0;
		}
	}

	void FixedUpdate () {
		// Check to see if player is on the ground
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		if (grounded && !canJump) {
			jumpForce = startJumpForce;
			canJump = true;
		}
		// Get the Input from controller or keyboard for horizontal movement
		float horizontal = Input.GetAxis ("Horizontal");
		// Takes the horizontal input and makes the player move
		HandleMovement (horizontal);
		// Flips the Player in the direction of movement
		Flip (horizontal);

		// If you keep holding the jump button, you will stay in the air a little
		if (Input.GetButton ("Jump")) {
			Jump (jumpForce / 11);
			jumpForce = jumpForce - 0.05f;
			if (!grounded) {
				canJump = false;
			}
		}

	}


	void HandleMovement(float horizontal){
		if (rigidBody.velocity.x < movementSpeed && rigidBody.velocity.x > -movementSpeed) {
			Move (horizontal);
		} else if (rigidBody.velocity.x > 0 && horizontal < 0) {
			Move (horizontal);
		} else if (rigidBody.velocity.x < 0 && horizontal > 0) {
			Move (horizontal);
		}
	}

	void Flip(float horizontal) {
		if (horizontal > 0 && ! facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	void Move(float horizontal){
		rigidBody.velocity = new Vector2 (horizontal * movementSpeed, rigidBody.velocity.y);
	}

	void Jump(float jumpForce){

			rigidBody.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);

	}
}
