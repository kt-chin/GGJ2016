using UnityEngine;

using System.Collections;

public class PlatformGenerator : MonoBehaviour {

    public GameObject platformPrefab;
    public GameObject rockPrefab;
    private GameObject LastPlatform;
    private int rockChance = 0;
    private int obstacleProtection = -1;


	// Use this for initialization
	void Start ()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        CreateInitialPlatform();
        for (int i = 0; i < 10; i++)
            CreatePlatform();
	}

    void CreateInitialPlatform()
    {
        var playerPos = GameObject.Find("Player").transform.position;
        GameObject newPlatform = (GameObject)Instantiate(platformPrefab, playerPos + new Vector3(0.0f, -2.0f, 0.0f), new Quaternion());
        newPlatform.transform.parent = this.transform;
        newPlatform.tag = "Ground";
        newPlatform.transform.localScale = new Vector3(2.5f + Random.value * 2.0f - 1.0f, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);
        LastPlatform = newPlatform;
    }

    void CreatePlatform()
    {
        if(obstacleProtection > 0)
        {
            if(obstacleProtection == 2)
                CreateNormalPlatform(5.0f);
            else
                CreateNormalPlatform();
            obstacleProtection--;
            return;
        }

        int myRandomValue = (int)(Random.value * 100);
        if (myRandomValue < rockChance)
        {
            CreateNormalPlatform(5.0f);
            CreateRock();
            rockChance = 0;
            obstacleProtection = 2;
        }
        else
        {
            CreateNormalPlatform();
            incrementChances();
        }
    }

    void incrementChances()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            rockChance += 15 + (int)(rockChance * 0.10f);
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
        {
            rockChance += 15 + (int)(rockChance * 0.10f);
        }
    }

    void CreateNormalPlatform(float xPos = -1.0f, float yPos = -1.0f)
    {


        if(yPos == -1.0f)
        yPos = Random.value * 8 - 4 - 1;
        while (yPos - LastPlatform.transform.position.y > 8.0f)
            yPos = Random.value * 8 - 4 - 1;
        if (xPos == -1.0f)
            xPos = Random.value * 8 - 2;
        GameObject newPlatform = (GameObject)Instantiate(platformPrefab, LastPlatform.transform.position + new Vector3(7.0f + xPos, -LastPlatform.transform.position.y + yPos, 0.0f), new Quaternion());
        newPlatform.transform.parent = this.transform;
        newPlatform.transform.localScale = new Vector3(2.5f + Random.value * 2.0f - 1.0f, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);
        newPlatform.tag = "Ground";
        LastPlatform = newPlatform;
    }

    void CreateRock()
    {
        GameObject newRock = (GameObject)Instantiate(rockPrefab, LastPlatform.transform.position + new Vector3(0.0f, 0.0f, 0.0f), new Quaternion());
        float rockHeight = newRock.GetComponent<Renderer>().bounds.size.y;
        newRock.transform.position = new Vector3(LastPlatform.transform.position.x, LastPlatform.transform.position.y + rockHeight/2, LastPlatform.transform.position.z); 
        newRock.transform.parent = this.transform;
    }



    // Update is called once per frame
    void Update () {
        if (LastPlatform == null || GameObject.FindGameObjectWithTag("Player") == null) return;
        var distanceToLastPlatform = LastPlatform.transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x;

        if (distanceToLastPlatform < 30.0f)
        {
            CreatePlatform();
        }

        var childTransforms = GetComponentInChildren<Transform>();
        foreach(Transform child in childTransforms)
        {
            if(child.position.x + 35.0f < GameObject.FindGameObjectWithTag("Player").transform.position.x)
            {
                Destroy(child.gameObject);
            }
        }

	}
}
