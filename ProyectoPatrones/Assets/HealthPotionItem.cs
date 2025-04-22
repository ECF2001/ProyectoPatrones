using UnityEngine;

public class HealthPotionItem : MonoBehaviour
{
    public int healAmount = 25;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void UsePotion()
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(25); // heal amount
            Debug.Log("Used health potion!");

           
            Destroy(gameObject);
        }
    }
}
