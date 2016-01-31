using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Parallaxing : MonoBehaviour {

	public Transform[] Backgrounds; // a list of all backgrounds to be parallaxed 
	private float[] ParallaxScales; // proportion of cam movement
	public float smoothing = 1f;			//how smooth parallax will be

	private Transform Cam; //refrence to main cams transformation
	private Vector3 previousCamPosition; // stores previous position of the camera in the last frame

	// called before start used for refrences
	void Awake() {
		// set up cam ref
		Cam = Camera.main.transform;
	}


	// Use this for initialization
	void Start () {
        //store the previous frame at the current cams frame positon

        if (SceneManager.GetActiveScene().buildIndex <= 1)
            return;
        
            previousCamPosition = Cam.position;

		ParallaxScales = new float[Backgrounds.Length];

		//assigning corosponding paralax scales
		for (int i = 0; i < Backgrounds.Length; i++) {
			ParallaxScales[i] = Backgrounds[i].position.z*-1;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex <= 1)
            return;
        for (int i = 0; i < Backgrounds.Length; i++){
			// parallax is opposite of camramovement because previous frame multiplied by scale
			float parallax = (previousCamPosition.x -Cam.position.x) * ParallaxScales[i];

			//set a target x position which is the current position + the parallax
			float backgroundTargetPositionX = Backgrounds[i].position.x + parallax;

			//create a target position which is the backgrounds current position withi its target x position
			Vector3 backgroundTargetPosition = new Vector3(backgroundTargetPositionX, Backgrounds[i].position.y, Backgrounds[i].position.z);

			//lerp between current position and target position
			Backgrounds[i].position = Vector3.Lerp (Backgrounds[i].position, backgroundTargetPosition, smoothing * Time.deltaTime);
		}
		//set previous cam pos to the camara position at the end of fram
		previousCamPosition = Cam.position;
	}
}
