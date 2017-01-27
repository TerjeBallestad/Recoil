using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public float recoil, bulletSpeed, shootFrequency, reloadTime;
	public float bulletCount, clipSize;
	float timeToReload;
	bool reloadTimeFinished, canShoot;
	public GameObject projectile;

	void Start(){
		clipSize = 15;
		bulletCount = clipSize;
		canShoot = true;

	}

	void Update(){
		
		if (bulletCount <= 0) {
			canShoot = false;
		}

		if (Input.GetButtonDown("Fire1")){
				InvokeRepeating("Shoot",0f,shootFrequency);
		}
		if (Input.GetButton("Fire1")){
			if (bulletCount <= 0) {
				StartCoroutine ("Reload");
			}
		}

		if (Input.GetButtonUp ("Fire1")) {
			CancelInvoke ();
		}
		if (Input.GetButtonDown("Fire3")){
			if (bulletCount < clipSize) {
				StartCoroutine ("Reload");
			}
		}
	}

	
	void Shoot (){
		if (canShoot) {
			GameObject instantiatedProjectile = Instantiate (projectile, transform.position, transform.rotation);
			Rigidbody2D projectileRB = instantiatedProjectile.GetComponent<Rigidbody2D> ();
			projectileRB.velocity = transform.TransformDirection (new Vector3 (bulletSpeed, 0, 0));
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
