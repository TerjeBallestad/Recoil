﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public float recoil, bulletSpeed, shootFrequency, reloadTime;
	public static float bulletCount, clipSize;
	float timeToReload;
	bool reloadTimeFinished, canShoot;
	public GameObject projectile;

	void Start(){
		clipSize = 20;
		bulletCount = clipSize;
		canShoot = true;

	}

	void Update(){
		
		if (bulletCount <= 0) {
			canShoot = false;
		}

		if (Input.GetMouseButton(0)){
			if (canShoot) {
				StartCoroutine ("Shoot");
			} else {
				StartCoroutine ("Reload");
			}
		}

		if (Input.GetKeyDown(KeyCode.R)){
			if (bulletCount < clipSize) {
				StartCoroutine ("Reload");
			}
		}
	}

	
	IEnumerator Shoot (){
		
		GameObject instantiatedProjectile = Instantiate (projectile, transform.position, transform.rotation);
		Rigidbody2D projectileRB = instantiatedProjectile.GetComponent<Rigidbody2D> ();
		projectileRB.velocity = transform.TransformDirection(new Vector3(bulletSpeed,0,0));

		yield return new WaitForSeconds (shootFrequency);
		bulletCount--;
	}



	IEnumerator Reload(){
		
		canShoot = false;
		yield return new WaitForSeconds (reloadTime);
		bulletCount = clipSize;
		canShoot = true;
	}
}