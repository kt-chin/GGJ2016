using UnityEngine;
using System.Collections;

public class ComboSystem : MonoBehaviour {

    // Variables
    public string key = "";
    public static System.Collections.Generic.Dictionary<string, System.Action> spells;
    private float timeUser = 0;
    public float comboLimit;
    private string[] spellNames;

    // Use this for initialization
    void Start () {


    }

    // Update is called once per frame
    void Update()
    {
        if (spellNames == null)
        {
                spellNames = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().spellNames;
                spells = new System.Collections.Generic.Dictionary<string, System.Action>()
          {
              {spellNames[0], () => fireCombo() },
              {spellNames[1], () => waterCombo() }
          };
        }
        // We check if the time between keys is greater than specific amount
        timeUser += Time.deltaTime;

        // Check if the user has stop input key during a specified time
        if (timeUser > comboLimit)
        {
            key = "";
            timeUser = 0;
        }

        // Check for the Key Input pressed, we check the four arrow keys
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            key += "U";
            timeUser = 0;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            key += "D";
            timeUser = 0;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            key += "L";
            timeUser = 0;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            key += "R";
            timeUser = 0;
        }

        // We check if the accumulated string is the same as element combo
        if (spells.ContainsKey(key))
        {
            spells[key].Invoke();
            key = "";
        }
    }


    void waterCombo()
    {
        Debug.Log("Water Spell !");
        //Todo Water spell effects
    }

    void fireCombo()
    {
        Debug.Log("Fire Spell !");
        //Todo fire spell effects
    }

}
