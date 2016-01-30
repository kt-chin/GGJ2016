using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster GM;

	void Start(){
		if (GM == null) {
			GM = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
		}
	}
    
	public Transform playerPrefab;
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
	}


	public static void KillPlayer(PlayerStats player){
        spawnPoint = player.gameObject.GetComponent<PlayerMovement>().lastPlatformHit.transform;
		Destroy (player.gameObject);
		GM.StartCoroutine (GM.RespawnPlayer ());
	}



}
