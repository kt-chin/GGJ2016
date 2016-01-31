using UnityEngine;
using System.Collections;

public class ComboSystem : MonoBehaviour {

    // Variables
    public string key = "";
    public static System.Collections.Generic.Dictionary<string, System.Action> spells;
    private float timeUser = 0;
    public float comboLimit;
    public GameObject cloudPrefab;
    public string[] spellNames;

    // Use this for initialization
    void Start () {


    }

    public int DetectSpellCast(string myKey)
    {
        int lastFound = -1;
        for (int u = 0; u < spellNames.Length; u++)
        {

            if (spellNames[u].Substring(0, myKey.Length) == myKey)
            {
                return u;
            }
        }
        return lastFound;
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

        this.key = this.transform.GetChild(0).GetComponent<SpellHintScript>().key;

        //CHeck for invalid inputs
        if ((timeUser > comboLimit && key.Length >= 4 || key.Length == 5 ) && !spells.ContainsKey(key))
        {
            int spellID = DetectSpellCast(key);
            if (spellID == -1) spellID = 2;
            spells[spellNames[spellID]].Invoke();
            this.transform.GetChild(0).GetComponent<SpellHintScript>().key = "";
            timeUser = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            timeUser = 0;
        

        // We check if the accumulated string is the same as element combo
        if (spells == null || spells.Count == 0)
            GameMaster.randomizeSpells();
        if (spells.ContainsKey(key))
        {
            spells[key].Invoke();
            this.transform.GetChild(0).GetComponent<SpellHintScript>().key = "";
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            key = spellNames[2];
            airCombo();
            this.transform.GetChild(0).GetComponent<SpellHintScript>().key = "";
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
        if (this.transform.GetChild(0).GetComponent<SpellHintScript>().key == spellNames[2])
        {
            Debug.Log("Air Spell !");
            GameObject cloud = (GameObject)Instantiate(cloudPrefab, tryToSnap(new Vector3(this.transform.position.x + 15.0f, 15.0f, 0.0f), "Rock(Clone)"), new Quaternion());
            cloud.tag = "Obstacles";
            cloud.GetComponent<Animator>().enabled = false;
        }
        else
        {

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().waitingToDie = true;
            
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
