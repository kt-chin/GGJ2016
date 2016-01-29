using UnityEngine;
using System.Collections;

public class MoveTrail2 : MonoBehaviour {

	public int movespeed = 230;

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * movespeed);
	}
}
