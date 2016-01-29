using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offSetX = 2; // used to counteract errors

	public bool hasARightBuddy = false; //checks for instation
	public bool hasALeftBuddy = false;

	public bool reverseScale = false; // used for none tilable sprites

	private float spriteWidth = 0.0f; // stores sprite width
	private Camera cam;
	private Transform myTransform;

	void Awake(){
		cam = Camera.main;
		myTransform = transform;

	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		//do we need buddies if not do nothing
		if (hasALeftBuddy == false || hasARightBuddy == false) {
			//calculate the cameras extent meaning half the widht of what the camera can see in world coordinates
			float camHorizantalExtent = cam.orthographicSize * Screen.width/Screen.height;

			// calculate X position where the camara can see the edge of the sprite
			float edgeVisablePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizantalExtent;
			float edgeVisablePositionLeft  = (myTransform.position.x - spriteWidth/2) + camHorizantalExtent;

			//checking can we see the edge of element then calling make new buddy if we can
			if(cam.transform.position.x >= edgeVisablePositionRight - offSetX && hasARightBuddy == false)
			{
				MakeNewBudy(1);
				hasARightBuddy = true;
			}
			else if (cam.transform.position.x <= edgeVisablePositionLeft + offSetX && hasALeftBuddy == false)
			{
				MakeNewBudy(-1);
				hasALeftBuddy = true;
			}


		}
	
	}
	// a function that creates a budy on the corrosponding side
	void MakeNewBudy (int rightOrLeft){
		//calculates new position for new buddy
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
		//instatiiating our new buddy and storing it in a variable 
		Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation)as Transform;

		//if not tilable reverse the size x to remove tile seems
		if (reverseScale == true) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
		}
		newBuddy.parent = myTransform.parent;
		if(rightOrLeft > 0 ){
		newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		}
		else{
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}
	}
}
