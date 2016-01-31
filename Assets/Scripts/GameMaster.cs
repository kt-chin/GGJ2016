using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster GM;
    public string[] spellNames;
    public int spellNumber = 4;
    public AudioClip[] playerSound;
    public AudioSource audioSource;
    public bool playerDead = false;
    private bool playedDeath = false;

    void Start(){
		if (GM == null) {
			GM = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
		}
        spellNames = new string[spellNumber];
        audioSource = GetComponent<AudioSource>();
        randomizeSpells();
    }
    
	public GameObject playerPrefab;
	static public Transform spawnPoint;
	public int spawnDelay = 2;
	public Transform SpawnParticalPrefab;
	public Transform enemySpawn;
	public IEnumerator RespawnPlayer (){
		Debug.Log ("SpawnSound");
		yield return new WaitForSeconds (spawnDelay);
		Instantiate (playerPrefab, spawnPoint.position + new Vector3(0, 8, 0), spawnPoint.rotation);
		GameObject SpawnParticalClone = Instantiate (SpawnParticalPrefab, spawnPoint.position + new Vector3(0, 8, 0), spawnPoint.rotation) as GameObject;
		Destroy (SpawnParticalClone, 3f);
        playerDead = false;
        playedDeath = false;
       
	}
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            //return;
            playerDead = true;
            
        }
        if (playerDead)
        {
            if (playedDeath == false)
            {
                audioSource.clip = playerSound[5];
                audioSource.volume = 1.0f;
                audioSource.Play();
                playedDeath = true;
            }
            
        }

        
    }


	public static void KillPlayer(){
        if (GameObject.FindGameObjectWithTag("Player") == null) return;
        
        spawnPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().lastPlatformHit.transform;
		Destroy (GameObject.FindGameObjectWithTag("Player").gameObject);
        
		GM.StartCoroutine (GM.RespawnPlayer ());
	}


    void randomizeSpells()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        var newSpells = new System.Collections.Generic.Dictionary<string, System.Action>();
        string[] charOptions = { "U", "D", "L", "R" };
        for (int i = 0; i < spellNumber; i++)
        {
            string spellName = "";
            do
            {
                while (spellName.Length < 5)
                {
                    spellName += charOptions[(int)(Random.value * 4)];
                }

            } while (newSpells.ContainsKey(spellName));
            spellNames[i] = spellName;

            Debug.Log("New Spell Added : " + spellName);
        }
    }

}
