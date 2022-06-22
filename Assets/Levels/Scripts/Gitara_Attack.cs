using UnityEngine;

public class Gitara_Attack : MonoBehaviour
{
    public Animator animator;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    void Update()
    {
        Attack();
    }
    
    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetInteger("Attack", 1);
        }
        else
        {
            animator.SetInteger("Attack", 0);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().EnemyTakeDamage(attackDamage);
        }
    }
}
