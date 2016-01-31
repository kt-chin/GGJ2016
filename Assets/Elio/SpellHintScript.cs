using UnityEngine;
using System.Collections;

public class SpellHintScript : MonoBehaviour {

    public GameObject ArrowsContainer;
    public GameObject DOWN;
    public GameObject UP_HINT;
    public GameObject[] full_spell;
    public GameObject[] Hint_spells;
    public string key = "";

    string lightningStrike;

    
    int x;
    int i;
    int i_max;
    int max_x;
    int min_x;
    bool is_destroyed = true;

    


    // Use this for initialization
    void Start () {

        

        full_spell = new GameObject[5];
        Hint_spells = new GameObject[4];

        i = 0;
        i_max = 4;

        x = -4;
        max_x = 4;
        min_x =-4;

        
        
    }
	
	// Update is called once per frame
	void Update () {
        

        Create_Spell();

       

      

    }


    void Create_Spell()
    {
        int spellID = gameObject.transform.parent.GetComponent<ComboSystem>().DetectSpellCast();
        string spellName = "";
        if (spellID != -1)
            spellName = gameObject.transform.parent.GetComponent<ComboSystem>().spellNames[spellID];


        if      (Input.GetKeyDown(KeyCode.UpArrow)){ if (is_destroyed == false) { clear_vector(); } key += "U";  full_spell[i] = (GameObject)Instantiate(ArrowsContainer.transform.GetChild(0).GetChild(0).gameObject, new Vector3(transform.parent.transform.position.x + x, transform.parent.transform.position.y + 3, 0), Quaternion.identity); increment_position();}
        else if (Input.GetKeyDown(KeyCode.DownArrow)) { if (is_destroyed == false) { clear_vector(); } key += "D"; full_spell[i] = (GameObject)Instantiate(ArrowsContainer.transform.GetChild(0).GetChild(1).gameObject, new Vector3(transform.parent.transform.position.x + x, transform.parent.transform.position.y + 3 , 0), Quaternion.Euler(0,0,180)); increment_position();}
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) { if (is_destroyed == false) { clear_vector(); } key += "L"; full_spell[i] = (GameObject)Instantiate(ArrowsContainer.transform.GetChild(0).GetChild(2).gameObject, new Vector3(transform.parent.transform.position.x + x, transform.parent.transform.position.y + 3, 0), Quaternion.Euler(0,0,90)); increment_position();}
        else if (Input.GetKeyDown(KeyCode.RightArrow)) { if (is_destroyed == false) { clear_vector(); } key += "R"; full_spell[i] = (GameObject)Instantiate(ArrowsContainer.transform.GetChild(0).GetChild(3).gameObject, new Vector3(transform.parent.transform.position.x + x, transform.parent.transform.position.y + 3, 0), Quaternion.Euler(0,0,-90)); increment_position(); }
        for (int u = 0; u < full_spell.Length; u++)
            if (full_spell[u] != null && full_spell[u].transform.parent != this.transform)
            { full_spell[u].transform.parent = this.transform;}
    }

   

    

    void increment_position() {

        if (x < max_x && i < i_max)
        {  x = x + 2;
            i++;
        }

        else {

            x = min_x;
            i = 0;
            is_destroyed = false; 
        }

    }


    void clear_vector()
    {
        key = "";

        for (int j = 0; j < 5; j++)
        {
            Destroy(full_spell[j]);
        }

        is_destroyed = true;
    }
}
