
// using UnityEngine;

// public class EnemyMovement : MonoBehaviour
// {
//     public float speed;
//     public float range;
//     public float distance;
//     public Transform target;
//     public GameObject player;
//     public LayerMask playerLayers;
//     public RigidBody2D rb;

//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player");
//         target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
//     }

//     void Update()
//     {
//         Close();
//         if(target.position.x>transform.position.x)
//         {
//             transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
//         }
//         else
//         {
//             transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
//         }
//     }
    
//     void Close()
//     {
//         if(Vector2.Distance(transform.position,target.position)<=distance)
//         {
//             Vector3 Dir=target.position - transform.position;
//             rb.MoveTowards(transform.position + Dir.normalized * speed * Time.deltaTime);
//             //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
//         }
//     }

//     void OnTriggerEnter2D(Collider2D collision)
//         {
//             if (collision.CompareTag("Player"))
//             {
//                 Debug.Log("Hit");
//             }
//         }
// }

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    public float distance;
    public Transform targetToFollow;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    public Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;    

    private void FixedUpdate()
    {
        FollowTarget();
    }

    public void FollowTarget()
    {
        //if there is no target just stay...
        if (targetToFollow == null)
        {
            Move(0);
            return;
        }
        if (Vector2.Distance(transform.position, targetToFollow.position) > distance)
        {
            Move(0);
            return;
        }

        float horizontal = targetToFollow.transform.position.x > transform.position.x ? 1f : -1f;
        Move(horizontal);
    }

    public void Move(float move)    {        

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * speed, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("hit");
        }
    }
}