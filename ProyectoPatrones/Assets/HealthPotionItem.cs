using UnityEngine;

public class HealthPotionItem : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void UsePotion()
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(25);
            Debug.Log("Used Health Potion! +25 HP");
        }
        else
        {
            Debug.LogWarning("PlayerHealth not found.");
        }
    }
}
