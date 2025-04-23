using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f; // Vida del enemigo
    public float damagePerHit = 10f; // Da�o recibido por cada impacto
    //public Faction faction = Faction.Enemy; 
    private EnemyShoot shooter;

    private void Awake()
    {
        shooter = GetComponent<EnemyShoot>();
    }
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
        Debug.Log("Enemy died!");

        if (shooter != null)
            shooter.enabled = false; // Detiene el disparo

        gameObject.SetActive(false); // Para usar con pooling
    }

    private void OnEnable()
    {
        health = 50f;

        if (shooter != null)
        {
            shooter.enabled = true;
        }

        // MUY IMPORTANTE si el enemigo tiene f�sica:
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}
