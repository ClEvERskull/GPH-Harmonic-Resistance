using UnityEngine;
public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    public Transform StartPos;
    public Animator Eanim;
    public Animator Vanim;
    public int SpawnDelay;
    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Voda"))
        {
            Respawn();
        }

        if (collision.CompareTag("CheckPoint"))
        {
            Debug.Log("Amogus");
            StartPos.position = collision.transform.position;
        }
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
            Respawn();
        }
    }
    private void Respawn()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<SpriteRenderer>().color = Color.white;
            enemies[i].enabled = true;
            if (enemies[i].respawn != null)
            {
                enemies[i].transform.position = enemies[i].respawn.position;
                enemies[i].EnemyCurrentHealth = enemies[i].EnemyStartingHealth;
                enemies[i].animator.SetTrigger("Idle");
                enemies[i].animator.SetInteger("EAttack", 0);
                enemies[i].PlayerInRange = false;
                enemies[i].PlayerCollider = null;
            }
        }
        Vanim.SetTrigger("VIdle");
        animator.SetTrigger("die");
        animator.SetBool("run", false);
        transform.position = StartPos.position;
        currentHealth = startingHealth;
        HealthCollectible[] hpup = FindObjectsOfType<HealthCollectible>();
        for (int j = 0; j < hpup.Length; j++)
        {
            hpup[j].enabled = true;
            hpup[j].gameObject.SetActive(true);
            Debug.Log("Sugoma");
        }
    }
}

