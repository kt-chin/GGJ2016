using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class level5script : MonoBehaviour, IPointerClickHandler
{

    public GUISkin mySkin;
    public void OnPointerClick(PointerEventData data)
    {

        // reload the scene
        SceneManager.LoadScene("level5");

    }
    void OnGUI()
    {
        GUI.skin = mySkin;
        //GUI.skin = null;
    }
}
