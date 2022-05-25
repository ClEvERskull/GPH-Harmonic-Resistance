using UnityEngine;
public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    public Transform StartPos;
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
            transform.position = StartPos.position;
            currentHealth = startingHealth;
            animator.SetBool("run", false);
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}

