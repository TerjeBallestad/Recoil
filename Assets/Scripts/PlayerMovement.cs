using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody2D rigidBody;
	public float movementSpeed, startJumpForce, jumpForce;
	public static bool facingRight, canJump;
	public bool grounded;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	[HideInInspector]
	public Vector2 recoil = Vector2.zero;
	public bool shooting = false;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		facingRight = true;
		canJump = true;
		jumpForce = startJumpForce;
	}

	void Update(){

		// Initial Jump
//		if (Input.GetButtonDown("Jump") && canJump){
//			Jump (startJumpForce);
//			canJump = false;
//		}
		if (canJump) {
			if (Input.GetButtonDown ("Jump")){
				if (jumpForce == startJumpForce) {
					Jump (startJumpForce);
					jumpForce -= 0.05f;
				}
			} else if (Input.GetButton ("Jump") && jumpForce < startJumpForce) {
				Jump (jumpForce / 11);
				jumpForce -= 0.05f;
			} 

		}


		if (Input.GetButtonUp ("Jump")) {
			canJump = false;
		}
	}

	void FixedUpdate () {
		if (shooting) {
			canJump = false;
		}

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
			float recoilForce = transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Shooting> ().recoilForce;
			recoilForce *= -1;
			transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Shooting> ().recoilForce = recoilForce;
		}
	}

	void Move(float horizontal){
		rigidBody.velocity = new Vector2 (horizontal * movementSpeed + recoil.x, rigidBody.velocity.y + recoil.y);
		recoil = Vector2.zero;
	}

	void Jump(float jumpForce){

			rigidBody.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);

	}
}
