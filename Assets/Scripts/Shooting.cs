using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public float bulletSpeed, shotFrequency, reloadTime, bulletCount, clipSize, recoilForce;
	float timeToReload, timeSinceLastShot;
	bool reloadTimeFinished, canShoot = true;
	public GameObject projectile;
	public Vector2 recoil;
	GunRotation rotationScript;
	PlayerMovement movementScript;

	void Start(){
		timeSinceLastShot = shotFrequency;
		bulletCount = clipSize;
		rotationScript = transform.parent.parent.GetComponent<GunRotation> ();
		movementScript = transform.parent.parent.parent.GetComponent<PlayerMovement> ();
	}

	void Update(){

		recoil = Quaternion.AngleAxis (rotationScript.angle, Vector3.forward) * Vector3.right;

		timeSinceLastShot += 1 * Time.deltaTime;

	
		if (bulletCount <= 0) {
			canShoot = false;
		}
			
		if (Input.GetButton ("Fire1")) {
			if (bulletCount <= 0) {
				StartCoroutine ("Reload");
			} else if (shotFrequency <= timeSinceLastShot) {
				Shoot ();
				movementScript.shooting = true;

				timeSinceLastShot += 1 * Time.deltaTime;
			}
		} else {
			movementScript.shooting = false;
		}

//		if (Input.GetButtonUp ("Fire1")) {
//			movementScript.shooting = false;
//		}
		if (Input.GetButtonDown("Fire3") && bulletCount < clipSize){
				StartCoroutine ("Reload");
		}
	}

	void Shoot (){
		if (canShoot) {
			GameObject instantiatedProjectile = Instantiate (projectile, transform.position, transform.rotation);
			Rigidbody2D projectileRB = instantiatedProjectile.GetComponent<Rigidbody2D> ();
			projectileRB.velocity = transform.TransformDirection (new Vector3 (bulletSpeed, 0, 0));
			transform.root.GetComponent<PlayerMovement> ().recoil = recoil.normalized * recoilForce;
			bulletCount--;
			timeSinceLastShot = 0;
		}
	}



	IEnumerator Reload(){
		
		canShoot = false;
		yield return new WaitForSeconds (reloadTime);
		bulletCount = clipSize;
		canShoot = true;
	}
}
