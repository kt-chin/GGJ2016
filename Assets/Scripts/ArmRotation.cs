using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	public int rotationOffSet = 0;

	// Update is called once per frame
	void Update () {

		//subtracting the position of the player from that of the mouse position
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position; 

		//normalizing the vector. Meaning that the sum of the vector will be equal to one.
		difference.Normalize ();

		float rotationZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
		transform.rotation = Quaternion.Euler (0f, 0f, rotationZ + rotationOffSet);
	

	}
}
