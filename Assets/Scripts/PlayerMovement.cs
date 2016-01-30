using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 7.0f;
    public float jumpY = 7.0f;
    public GameObject lastPlatformHit;

    private Animator anim;
    private bool grounded = true;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Ground", grounded);
        //jumpY -= gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W) && grounded&& anim.GetBool("Ground"))
        {
            rb.velocity = new Vector3(0, jumpY, 0);
            grounded = false;
            anim.SetBool("Ground", false);
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("vSpeed", rb.velocity.y);
        anim.SetFloat("Speed", rb.velocity.x);
        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.velocity = new Vector3(moveHorizontal * speed, rb.velocity.y, 0);
        }
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Ground")
        {
            grounded = true;
            lastPlatformHit = collider.gameObject;
        }
    }

}
