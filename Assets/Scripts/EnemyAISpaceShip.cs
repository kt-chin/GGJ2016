using UnityEngine;
using System.Collections;
using Pathfinding;



[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]
public class EnemyAISpaceShip : MonoBehaviour {
		
		//what to chase
		public Transform Target;

		//how many times path is updated
		public float UpdateRate = 2f;

		//caching
		private Seeker seeker;

		//rigidbody
		private Rigidbody2D RigBod;

		//path to follow
		public Path path;

		//AI speed per second
		public float speed = 300;
		public ForceMode2D fMode;

		[HideInInspector]
		public bool pathIsEnd = false;

		//max dist for AI to continue to next waypoint
		public float nextWayPointDistance = 3;

		//the waypoint we are currently moving towards
		private int currentWayPoint = 0;

		private bool searchingForPlayer = false;

		void Start(){
			seeker = GetComponent<Seeker> ();
			RigBod = GetComponent<Rigidbody2D> ();

			if (Target == null) {
				Debug.LogError("NO TARGET FOUND");
			if (!searchingForPlayer){
				searchingForPlayer = true;
				StartCoroutine(SearchingForPlayer());
			}
				return;
			}
			// Starts a parth to targets position return the result
			seeker.StartPath (transform.position, Target.position, OnPathComplete);

			StartCoroutine (UpdatePath());
		}
		
		IEnumerator SearchingForPlayer(){

		GameObject SearchResult = GameObject.FindGameObjectWithTag("Player");
			if (SearchResult == null) {
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (SearchingForPlayer ());
		}
		else {
			Target = SearchResult.transform;
			searchingForPlayer = false;
			StartCoroutine(UpdatePath());
			return false;
		}
		}

		IEnumerator UpdatePath() {
		if (Target == null) {
			Debug.LogError("NO TARGET FOUND");
			if (!searchingForPlayer){
				searchingForPlayer = true;
				StartCoroutine(SearchingForPlayer());
			}
			return false;
		}

			// Starts a parth to targets position return the result
			seeker.StartPath (transform.position, Target.position, OnPathComplete);

			yield return new WaitForSeconds (1f / UpdateRate);
			StartCoroutine (UpdatePath ());

		}

		public void OnPathComplete(Path p){
			Debug.Log ("We got a path did it have an error" + p.error);
			if (!p.error) {
				path = p;
				currentWayPoint = 0;
			}
			
		}
		void FixedUpdate () {
		if (Target == null) {
			Debug.LogError("NO TARGET FOUND");
			if (!searchingForPlayer){
				searchingForPlayer = true;
				StartCoroutine(SearchingForPlayer());
			}
			return;
		}
			//TODO: Always look at player?
			if (path == null)
				return;

			if (currentWayPoint >= path.vectorPath.Count) {
			if (pathIsEnd)
					return;

				Debug.Log("Path End");
				pathIsEnd = true;
				return;
			}
			pathIsEnd = false;
		//Direction to next point
			Vector3 dir = (path.vectorPath [currentWayPoint] - transform.position).normalized;
			dir *= speed * Time.fixedDeltaTime;
		//Move The AI!!!!
			RigBod.AddForce (dir, fMode);
			float dis = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
			if (dis < nextWayPointDistance) {
				currentWayPoint++;
				return;

			}
		}

	}	