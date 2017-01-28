using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public float bulletSpeed, shootFrequency, reloadTime, bulletCount, clipSize, recoilForce;
	float timeToReload;
	bool reloadTimeFinished, canShoot;
	public GameObject projectile;
	public Vector2 recoil;
	GunRotation rotationScript;
	PlayerMovement movementScript;

	void Start(){
		clipSize = 15;
		bulletCount = clipSize;
		canShoot = true;
		rotationScript = transform.parent.parent.GetComponent<GunRotation> ();
		movementScript = transform.parent.parent.parent.GetComponent<PlayerMovement> ();
	}

	void Update(){

		recoil = Quaternion.AngleAxis (rotationScript.angle, Vector3.forward) * Vector3.right;
	
		if (bulletCount <= 0) {
			canShoot = false;
		}

		if (Input.GetButtonDown("Fire1")){
			InvokeRepeating ("Shoot", 0f, shootFrequency);
			movementScript.shooting = true;
		}
		if (Input.GetButton("Fire1")){
			if (bulletCount <= 0) {
				StartCoroutine ("Reload");
			}
		}

		if (Input.GetButtonUp ("Fire1")) {
			CancelInvoke ();
			movementScript.shooting = false;
		}
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
		}
	}



	IEnumerator Reload(){
		
		canShoot = false;
		yield return new WaitForSeconds (reloadTime);
		bulletCount = clipSize;
		canShoot = true;
	}
}
