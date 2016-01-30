using UnityEngine;
using System.Collections;

public class ComboSystem : MonoBehaviour {

    // Variables
    public string key = "";
    public static System.Collections.Generic.Dictionary<string, System.Action> spells;
    private float timeUser = 0;
    public float comboLimit;
    public GameObject cloudPrefab;
    private string[] spellNames;

    // Use this for initialization
    void Start () {


    }

    int DetectSpellCast()
    {
        for(int i = 1; i < key.Length; i ++)
        {
            int matches = 0;
            int lastFound = -1;
            for (int u = 0; u < spellNames.Length; u++)
            {

                if (spellNames[u].IndexOf(key.Substring(0, i)) != -1)
                {
                    matches++;
                    lastFound = u;
                }
            }
            if (matches <= 1 && lastFound != -1) return lastFound;
        }
        return 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (spellNames == null || spellNames.Length == 0 || spellNames[0] == null)
        {
                spellNames = GameMaster.spellNames;
                spells = new System.Collections.Generic.Dictionary<string, System.Action>()
          {
              {spellNames[0], () => fireCombo() },
              {spellNames[1], () => waterCombo() },
              {spellNames[2], () => airCombo() },
              {spellNames[3], () => earthCombo() }
          };
        }
        // We check if the time between keys is greater than specific amount
        timeUser += Time.deltaTime;

        //CHeck for invalid inputs
        if ((timeUser > comboLimit && key.Length >= 4 || key.Length == 5 ) && !spells.ContainsKey(key))
        {
            spells[spellNames[DetectSpellCast()]].Invoke();
            key = "";
            timeUser = 0;
        }

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

        if(Input.GetKeyDown(KeyCode.O))
        {
            key = spellNames[2];
            airCombo();
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

    void airCombo()
    {
        if (key == spellNames[2])
        {
            Debug.Log("Air Spell !");
            GameObject cloud = (GameObject)Instantiate(cloudPrefab, tryToSnap(new Vector3(this.transform.position.x + 15.0f, 15.0f, 0.0f), "Rock(Clone)"), new Quaternion());
            cloud.tag = "Obstacles";
            cloud.GetComponent<Animator>().enabled = false;
        }
        else
        {
            Debug.Log("Failed Air Spell !");
            GameObject cloud = (GameObject)Instantiate(cloudPrefab, new Vector3(this.transform.position.x , 15.0f, 0.0f), new Quaternion());
            cloud.tag = "Obstacles";
            cloud.GetComponent<Animator>().enabled = false;
        }
    }

    void earthCombo()
    {
        Debug.Log("Earth Spell !");
        //Todo earth spell effects
    }

    Vector3 tryToSnap(Vector3 vec, string target)
    {
        foreach(var tar in GameObject.FindGameObjectsWithTag("Obstacles"))
        {
            if (tar.name != target) continue;
            if(Mathf.Abs(vec.x -tar.transform.position.x) < 5.0f)
            {
                return new Vector3(tar.transform.position.x, vec.y, 0);
            } 
        }
        return vec;
    }

}
