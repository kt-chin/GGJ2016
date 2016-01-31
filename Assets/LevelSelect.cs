using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData data)
    {


        // reload the scene
        SceneManager.LoadScene("LevelSelector");

    }

}
