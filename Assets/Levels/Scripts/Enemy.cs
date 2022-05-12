using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float distance;
    public Transform targetToFollow;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    public float damage = 1f;
    public Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public float EnemyStartingHealth;
    public float EnemyCurrentHealth { get; private set; }
    public Animator animator;
    public bool PlayerInRange;
    private float AttackTime;
    public float AttackSpeed;
    private Collider2D PlayerCollider;
    private void Awake()
    {
        EnemyCurrentHealth = EnemyStartingHealth;
    }

    void ManageAttack()
    {
        if(PlayerCollider == null)
        {
            return;
        }
        AttackTime += Time.fixedDeltaTime;
        if (AttackTime > AttackSpeed)
        {
            PlayerCollider.GetComponent<Health>().TakeDamage(damage);
            AttackTime = 0;
        }
    }
    private void FixedUpdate()
    {
        if (EnemyCurrentHealth > 0)
        {
            FollowTarget();
            ManageAttack();
        }
        else
        {
            animator.SetTrigger("EDie");
            GetComponent<Enemy>().enabled = false;
        }
    }

    public void FollowTarget()
    {
        //if there is no target just stay...
        if (targetToFollow == null)
        {
            //animator.SetBool("Walk", false);
            Move(0);
            return;
        }
        if (Vector2.Distance(transform.position, targetToFollow.position) > distance)
        {
            //animator.SetBool("Walk", false);
            Move(0);
            return;
        }
        //animator.SetBool("Walk", true);
        float horizontal = targetToFollow.transform.position.x > transform.position.x ? 1f : -1f;
        Move(horizontal);
    }

    public void Move(float move)
    {

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
            PlayerInRange = true;
            PlayerCollider = collision;
            AttackTime = AttackSpeed + 1;
            //collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInRange = false;
            PlayerCollider = null;
            //collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    public void EnemyTakeDamage(float _damage)
    {
        EnemyCurrentHealth = Mathf.Clamp(EnemyCurrentHealth - _damage, 0, EnemyStartingHealth);

    }
}