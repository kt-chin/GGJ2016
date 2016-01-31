using UnityEngine;

namespace UnitySampleAssets._2D
{

    public class Camera2DFollow : MonoBehaviour
    {

        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
		public float boundary = -1;
        private float offsetZ;
        private Vector3 lastTargetPosition;
        private Vector3 currentVelocity;
        private Vector3 lookAheadPos;
        public bool earthQuake = false;

        private float nextTimeToSearch = 0;
        // Use this for initialization
        private void Start()
        {
            lastTargetPosition = target.position;
            offsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }

        // Update is called once per frame
        private void Update()
        {
			if (target == null) {
				FindPlayer();
				return;
			}
				
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - lastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                lookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            if(earthQuake)
                lookAheadPos = new Vector3(lookAheadPos.x + Random.value * 2.0f - 1.0f, lookAheadPos.y + Random.value * 2.0f - 1.0f, lookAheadPos.z + Random.value * 2.0f - 1.0f);

            Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward*offsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

			newPos = new Vector3 (newPos.x, Mathf.Clamp(newPos.y,boundary,Mathf.Infinity), newPos.z);

            transform.position = newPos; 

            lastTargetPosition = target.position;
        }
		void FindPlayer (){
			if (nextTimeToSearch <= Time.time) {
				GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
				if (searchResult != null)
					target = searchResult.transform;
				nextTimeToSearch = Time.time + 0.5f;
			}
		}
    }
}