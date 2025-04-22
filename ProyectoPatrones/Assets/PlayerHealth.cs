using UnityEngine;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour, IHealthSubject
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    private List<IHealthObserver> observers = new List<IHealthObserver>();

    void Start()
    {
        currentHealth = maxHealth;
        NotifyObservers();
    }

    public void RegisterObserver(IHealthObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void RemoveObserver(IHealthObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnHealthChanged(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth = Mathf.Max(0, currentHealth - damageAmount);
        Debug.Log($"Player took {damageAmount} damage. Current health: {currentHealth}/{maxHealth}");
        NotifyObservers();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
        Debug.Log($"Player healed for {healAmount}. Current health: {currentHealth}/{maxHealth}");
        NotifyObservers();
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Add your death logic here
    }
}