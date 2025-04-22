using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour, IHealthObserver
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private PlayerHealth playerHealth;

    void Start()
    {
        // Find the player and register this UI as an observer
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.RegisterObserver(this);
        }
        else
        {
            Debug.LogError("Could not find PlayerHealth component on Player!");
        }
    }

    void OnDestroy()
    {
        // Important: Unregister when this object is destroyed to prevent memory leaks
        if (playerHealth != null)
        {
            playerHealth.RemoveObserver(this);
        }
    }

    public void OnHealthChanged(int currentHealth, int maxHealth)
    {
        // Update the UI elements
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{currentHealth} / {maxHealth}";
        }
    }
}
