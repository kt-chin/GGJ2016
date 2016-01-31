using UnityEngine;

using System.Collections;

public class PlatformGenerator : MonoBehaviour {

    public GameObject platformPrefab;
    public GameObject obstaclesPrefab;
    private GameObject LastPlatform;
    private int obstacleChance = 0;
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
        int chosenPlatform = (int)(Random.value * platformPrefab.transform.childCount);
        GameObject platform = platformPrefab.transform.GetChild(chosenPlatform).gameObject;
        
        var playerPos = GameObject.Find("Player").transform.position;
        GameObject newPlatform = (GameObject)Instantiate(platform, playerPos + new Vector3(0.0f, -2.0f, 0.0f), new Quaternion());
        newPlatform.transform.parent = this.transform;
        newPlatform.tag = "Ground";
        if(chosenPlatform != 1)
            newPlatform.transform.localScale = new Vector3(2.5f + Random.value * 2.0f - 1.0f, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);
        LastPlatform = newPlatform;
    }

    void CreatePlatform()
    {
        int currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (obstacleProtection > 0)
        {
            if(obstacleProtection == 2)
                CreateNormalPlatform(7.0f);
            else
                CreateNormalPlatform();
            obstacleProtection--;
            return;
        }

        int randomElement = Mathf.RoundToInt(Random.value) * currentLevel;

        int myRandomValue = (int)(Random.value * 100);
        if (myRandomValue < obstacleChance)
        {
            CreateNormalPlatform(5.0f, 4.0f);
            if (randomElement == 0)
            {
                CreateRock();
            }
            else if (randomElement == 1)
            {
                CreateVines();
            }
            else if (randomElement == 2)
            {
                CreateFieryThing();
            }
            else if (randomElement == 3)
            {
                CreateBearTrap();
            }
            obstacleChance = 0;
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
        int currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        obstacleChance += 15 + (int)(obstacleChance * 0.10f);
        
    }

    void CreateNormalPlatform(float xPos = -1.0f, float xLength = -1.0f)
    {
        int chosenPlatform = (int)(Random.value * platformPrefab.transform.childCount);
        GameObject platform = platformPrefab.transform.GetChild(chosenPlatform).gameObject;
        if (xPos != -1.0f)
            platform = platformPrefab.transform.GetChild(0).gameObject;
        
        float yPos = Random.value * 8 - 4 - 1;
        while (yPos - LastPlatform.transform.position.y > 8.0f)
            yPos = Random.value * 8 - 4 - 1;
        if (xPos == -1.0f)
            xPos = Random.value * 8;


        GameObject newPlatform = (GameObject)Instantiate(platform, LastPlatform.transform.position + new Vector3(7.0f + xPos, -LastPlatform.transform.position.y + yPos, 0.0f), new Quaternion());
        newPlatform.transform.parent = this.transform;
        if(xLength != -1)
            newPlatform.transform.localScale = new Vector3(xLength, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);
        else if(chosenPlatform != 1)
            newPlatform.transform.localScale = new Vector3(2.5f + Random.value * 2.0f - 1.0f, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);

        newPlatform.tag = "Ground";
        LastPlatform = newPlatform;
    }

    void CreateRock()
    {
        GameObject newRock = (GameObject)Instantiate(obstaclesPrefab.transform.GetChild(0).gameObject, LastPlatform.transform.position, new Quaternion());
        float sizeY = newRock.GetComponent<Renderer>().bounds.size.y / 2;
        newRock.transform.position = new Vector3(LastPlatform.transform.position.x, LastPlatform.transform.position.y, LastPlatform.transform.position.z);
        newRock.transform.parent = this.transform;
    }
    void CreateVines()
    {
        GameObject newRock = (GameObject)Instantiate(obstaclesPrefab.transform.GetChild(1).gameObject, LastPlatform.transform.position, new Quaternion());
        float sizeY = newRock.GetComponent<Renderer>().bounds.size.y / 2;
        newRock.transform.position = new Vector3(LastPlatform.transform.position.x, LastPlatform.transform.position.y, LastPlatform.transform.position.z);
        newRock.transform.parent = this.transform;
    }
    void CreateFieryThing()
    {
        GameObject newRock = (GameObject)Instantiate(obstaclesPrefab.transform.GetChild(2).gameObject, LastPlatform.transform.position, new Quaternion());
        float sizeY = newRock.GetComponent<Renderer>().bounds.size.y / 2;
        newRock.transform.position = new Vector3(LastPlatform.transform.position.x, LastPlatform.transform.position.y + sizeY, LastPlatform.transform.position.z);
        newRock.transform.parent = this.transform;
    }
    void CreateBearTrap()
    {
        GameObject newRock = (GameObject)Instantiate(obstaclesPrefab.transform.GetChild(3).gameObject, LastPlatform.transform.position, new Quaternion());
        float sizeY = newRock.GetComponent<Renderer>().bounds.size.y / 2;
        newRock.transform.position = new Vector3(LastPlatform.transform.position.x, LastPlatform.transform.position.y + 15, LastPlatform.transform.position.z);
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
