using UnityEngine;

public class HealthTester : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Press minus key to take damage
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (playerHealth != null)
                playerHealth.TakeDamage(10);
        }

        // Press plus key to heal
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (playerHealth != null)
                playerHealth.Heal(10);
        }
    }
}
