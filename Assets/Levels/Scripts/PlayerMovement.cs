using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    public float jumpAmount = 10;
    public float gravityScale = 10;
    public float fallingGravityScale = 40;
   

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }   

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.1f)
            transform.localScale = new Vector3(-1, 1, 1);

        if(Input.GetButtonDown("Jump") && Mathf.Abs(body.velocity.y) < 0.001f)
        {
            body.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
        }

        anim.SetBool("run", horizontalInput != 0);
    }
}