using UnityEngine;
public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
        }

        else
        {
            animator.SetTrigger("die");
            GetComponent<PlayerMovement>().enabled = false;
            animator.SetBool("run", false);
        }
    }
}

