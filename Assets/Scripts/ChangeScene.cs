using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	
	// Update is called once per frame
	public void ChangeToScene (string sceneToGo) {

        // We tell the application which scene to go
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToGo);
	}
}
