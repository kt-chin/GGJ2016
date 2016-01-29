using UnityEngine;
using System.Collections;

public class FadingScript : MonoBehaviour {

	public Texture2D fadingtexture;
	public float fadespeed;

	private int drawdepth = -1000;
	private float alpha = 1;
	private int fadedirection = -1;

	void OnGUI(){

		alpha += fadedirection * fadespeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawdepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadingtexture);
	}
	public float startFade (int direction){
		fadedirection = direction;
			return(fadespeed);
	}
	void OnLevelLoaded(){
		startFade (-1);
	}
}
