using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[System.Serializable]
	public class EnemyStats {
		public int Health = 100;
	}
		
	public EnemyStats stats = new EnemyStats();

	public void DamageEnemy (int Damage){
		stats.Health -= Damage;
		if (stats.Health <= 0) {
			GameMaster.KillEnemy(this);
		}
	}
	
}