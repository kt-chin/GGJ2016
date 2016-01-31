using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

	public static GameMaster GM;
    public static string[] spellNames;
    public static int spellNumber = 4;
    public AudioClip[] playerSound;
    public AudioSource audioSource;
    public bool playerDead = false;
    private bool playedDeath = false;
    public static GameObject lastPlatformHit;
    public bool pregame = true;

    void Start()
    {
        if (GM == null)
        {
			GM = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
           playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
        spellNames = new string[spellNumber];
        audioSource = GetComponent<AudioSource>();
        randomizeSpells();
    }
    }
    
	public GameObject playerPrefab;
	static public Transform spawnPoint;
	public int spawnDelay = 2;
	public Transform SpawnParticalPrefab;
	public Transform enemySpawn;
    private static PlayerMovement playerMove;
    public IEnumerator RespawnPlayer()
    {
        Debug.Log("SpawnSound");
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position + new Vector3(0, 8, 0), spawnPoint.rotation);
        GameObject SpawnParticalClone = Instantiate(SpawnParticalPrefab, spawnPoint.position + new Vector3(0, 8, 0), spawnPoint.rotation) as GameObject;
        Destroy(SpawnParticalClone, 3f);
        playerDead = false;
        playedDeath = false;
       
	}
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu" || SceneManager.GetActiveScene().name != "LevelSelector")
        {
            //Debug.Log("WE ARE IN MAIN MENU OR LEVEL SELECTOR");
            pregame = false;
        }
        if (pregame == true)
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                pregame = false;
                //return;
                playerDead = true;
            }
            if (playerDead)
            {
                pregame = true;
                if (playedDeath == false)
                {
                    audioSource.clip = playerSound[5];
                    audioSource.volume = 1.0f;
                    audioSource.Play();
                    playedDeath = true;
                }

            }

            if (GameObject.FindGameObjectWithTag("Player") == null) return;
            playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }
    
    }


    public static void KillPlayer()
    {
        if (lastPlatformHit == null) return;
        spawnPoint = lastPlatformHit.transform;
        Destroy(GameObject.FindGameObjectWithTag("Player").gameObject);


        playerMove = null;

        GM.StartCoroutine(GM.RespawnPlayer());
	}


    public static void randomizeSpells()
    {
        GameMaster.spellNames = new string[spellNumber];
        Random.seed = (int)System.DateTime.Now.Ticks;
        var newSpells = new System.Collections.Generic.Dictionary<string, System.Action>();
        string[] charOptions = { "U", "D", "L", "R" };
        for (int i = 0; i < spellNumber; i++)
        {
            string spellName = "";
            do
            {
                spellName = "";
                while (spellName.Length < 5)
                {
                    spellName += charOptions[(int)(Random.value * 4)];
                }

            } while (newSpells.ContainsKey(spellName));
            GameMaster.spellNames[i] = spellName;

            Debug.Log("New Spell Added : " + spellName);
        }
    }

}
