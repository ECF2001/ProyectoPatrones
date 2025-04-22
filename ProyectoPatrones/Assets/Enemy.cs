using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f; // Vida del enemigo
    public float damagePerHit = 10f; // Daño recibido por cada impacto
    //public Faction faction = Faction.Enemy; 

    public void TakeDamage(float damage)
    {
        health -= damage; // Reduce la vida
        if (health <= 0)
        {
            Die(); // Si la vida llega a cero, el enemigo muere
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!"); // Puedes agregar una animación aquí
        gameObject.SetActive(false); // Desactiva el enemigo
    }
}
