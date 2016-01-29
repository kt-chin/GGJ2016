using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	public float fireRate = 0;
	public int Damage = 10;
	public LayerMask whatToHit;
	
	public Transform BulletTrailPrefab;
	public Transform MuzzleFlashPrefab;
	public Transform HitPrefab;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;
	
	float timeToFire = 0;
	Transform firePoint;
	
	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No firePoint? WHAT?!");
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	

}