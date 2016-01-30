using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    public bool isDestroyed = false;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Animator>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void destroyRock()
    {
        if(!isDestroyed)
        {
            isDestroyed = true;
            StartCoroutine(MyCoroutine());
        }
    }


    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(0.520f);
        GetComponent<Animator>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        yield return new WaitForSeconds(8.0f);
        Destroy(gameObject);
    }


}
