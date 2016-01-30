using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10.0f;
    public float jumpY = 7.0f;
    public GameObject lastPlatformHit;
    public bool move = false;
    private Animator anim;
    public bool grounded = true;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            move = !move;
        }
        //jumpY -= gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector3(0, jumpY, 0);
            grounded = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponent<Animator>().SetTrigger("Die");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        if (move)
        {
            GetComponent<Animator>().SetBool("IsIdle", false);
            float moveHorizontal = speed;
            if (moveHorizontal != 0)
            {
                rb.velocity = new Vector3(moveHorizontal, rb.velocity.y, 0);
            }
        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            if(Mathf.Abs(moveHorizontal) > 0)
                GetComponent<Animator>().SetBool("IsIdle", false);
            if (Input.GetAxis("Horizontal") != 0)
                rb.velocity = new Vector3(moveHorizontal * speed, rb.velocity.y, 0);


        }

    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            grounded = true;
            lastPlatformHit = collider.gameObject;
        }
    }
    void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

}
