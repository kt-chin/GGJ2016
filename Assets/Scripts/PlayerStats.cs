using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	[System.Serializable]
	public class Player {
		public int Health = 100;
	}

	public int fallBoundary = -20;

	public Player playerP = new Player();

	void Update(){
		if (transform.position.y <= -20) {
            GameMaster.KillPlayer();
        }
	}


}

