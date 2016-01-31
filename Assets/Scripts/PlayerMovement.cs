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
    public GameMaster soundHandler;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        soundHandler = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            move = !move;
        }
        //jumpY -= gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector3(0, jumpY, 0);
            soundHandler.audioSource.clip = soundHandler.playerSound[0];
            soundHandler.audioSource.volume = 0.4f;
            soundHandler.audioSource.Play();
            
            grounded = false;
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
            if (Mathf.Abs(moveHorizontal) > 0 && GetComponent<Animator>().GetBool("IsIdle"))
                GetComponent<Animator>().SetBool("IsIdle", false);
            if (Input.GetAxis("Horizontal") != 0)
                rb.velocity = new Vector3(moveHorizontal * speed, rb.velocity.y, 0);


        }

    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Obstacles")
        {
            grounded = true;
            lastPlatformHit = collider.gameObject;
            GameMaster.lastPlatformHit = collider.gameObject;
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
