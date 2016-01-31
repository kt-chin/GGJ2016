using UnityEngine;
using System.Collections;

public class FireSpell : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Animator>().enabled = true;
        StartCoroutine(MyCloudCoroutine());

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator MyCloudCoroutine()
    {
        yield return new WaitForSeconds(1.30f);
        GetComponent<Animator>().enabled = false;

        if(GameObject.FindGameObjectWithTag("Player") != null && this.transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x < 1.5f)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("Die");
        }

        foreach(var obj in GameObject.FindGameObjectsWithTag("Obstacles"))
        {
            float pos = Mathf.Abs(this.transform.position.x - obj.transform.position.x);
            if (obj.name == "Vines(Clone)" && Mathf.Abs(this.transform.position.x - obj.transform.position.x) < 3.0f)
            {
                if (obj.GetComponent<Vines>().isDestroyed)
                    continue;
                obj.GetComponent<Animator>().enabled = true;
                Destroy(gameObject);
                obj.GetComponent<Vines>().destroyVines();
            }
        }
        Destroy(this.gameObject);
    }

}
