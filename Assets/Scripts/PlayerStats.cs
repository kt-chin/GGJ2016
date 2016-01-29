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
			TakeDamage(999999);
		}
	}
	public void TakeDamage (int Damage){
		playerP.Health -= Damage;
		if (playerP.Health <= 0) {
			GameMaster.KillPlayer(this);
		}
	}

}

