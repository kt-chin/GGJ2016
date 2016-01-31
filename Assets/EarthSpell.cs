using UnityEngine;
using System.Collections;

public class EarthSpell : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Animator>().enabled = true;
        StartCoroutine(MyCloudCoroutine());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MyCloudCoroutine()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && this.transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x < 1.5f)
        {
            //Kill Player
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("Burning");
        }

        //GameObject.
        yield return new WaitForSeconds(1.30f);



        foreach (var obj in GameObject.FindGameObjectsWithTag("Obstacles"))
        {
            float pos = Mathf.Abs(this.transform.position.x - obj.transform.position.x);
            if (obj.name == "Vines(Clone)" && Mathf.Abs(this.transform.position.x - obj.transform.position.x) < 3.0f)
            {
                if (obj.GetComponent<BearTrap>().isDestroyed)
                    continue;

                Destroy(gameObject);
                obj.GetComponent<BearTrap>().destroyRock();
            }
        }
        Destroy(this.gameObject);
    }

}
