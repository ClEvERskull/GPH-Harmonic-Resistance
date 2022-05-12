using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField]private float HealthValue;
    Health playerHealth;
    private void Awake()
    {
        playerHealth = FindObjectOfType<Health>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerHealth.currentHealth < playerHealth.startingHealth)
        {


            if (collision.tag == "Player")
            {
                collision.GetComponent<Health>().AddHealth(HealthValue);
                gameObject.SetActive(false);
            }
        }
    }

}
