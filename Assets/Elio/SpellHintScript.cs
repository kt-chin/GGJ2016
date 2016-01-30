using UnityEngine;
using System.Collections;

public class SpellHintScript : MonoBehaviour {

    public GameObject UP;
    public GameObject[] full_spell;
    
    int x;
    int i;
    int i_max;
    int max_x;
    int min_x;
    bool is_destroyed = true;

    // Use this for initialization
    void Start () {

        full_spell = new GameObject[5];
        i = 0;
        i_max = 4;

        x = 0;
        max_x = 8;
        min_x =0;
    }
	
	// Update is called once per frame
	void Update () {

        
        Create_Spell();
    }


    void Create_Spell()
    {
        
        if      (Input.GetKeyDown(KeyCode.UpArrow)){ if (is_destroyed == false) { clear_vector(); } full_spell[i] = (GameObject)Instantiate(UP, new Vector3(transform.parent.transform.position.x + x, transform.parent.transform.position.y + 3, 0), Quaternion.identity); increment_position();}
        else if (Input.GetKeyDown(KeyCode.DownArrow)) { if (is_destroyed == false) { clear_vector(); } full_spell[i] = (GameObject)Instantiate(UP, new Vector3(transform.parent.transform.position.x + x, transform.parent.transform.position.y + 3 , 0), Quaternion.Euler(0,0,180)); increment_position();}
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) { if (is_destroyed == false) { clear_vector(); } full_spell[i] = (GameObject)Instantiate(UP, new Vector3(transform.parent.transform.position.x + x, transform.parent.transform.position.y + 3, 0), Quaternion.Euler(0,0,90)); increment_position();}
        else if (Input.GetKeyDown(KeyCode.RightArrow)) { if (is_destroyed == false) { clear_vector(); } full_spell[i] = (GameObject)Instantiate(UP, new Vector3(transform.parent.transform.position.x + x, transform.parent.transform.position.y + 3, 0), Quaternion.Euler(0,0,-90)); increment_position(); }
        for (int u = 0; u < full_spell.Length; u++)
            if (full_spell[u] != null && full_spell[u].transform.parent != this.transform)
            { full_spell[u].transform.parent = this.transform;
              //if(GameObject.FindGameObjectWithTag("Player").GetComponent<)
              }
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

        for (int j = 0; j < 5; j++)
        {
            Destroy(full_spell[j]);
        }

        is_destroyed = true;
    }
}
