using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class gameStart : MonoBehaviour , IPointerClickHandler
{

	public void OnPointerClick (PointerEventData data) {


        // reload the scene
       SceneManager.LoadScene("Level1");
    
    }

}
