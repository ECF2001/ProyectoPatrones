using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 30f;       // Tiempo de vida de la bala    
    public float damage = 10f;         // Daño de la bala
    public bool isEnemyBullet = false; // Marca si la bala fue disparada por un enemigo

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destruye la bala después de cierto tiempo
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnemyBullet && other.CompareTag("Enemy"))
        {
            // Si es bala del jugador y golpea a un enemigo
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
           
        }
        else if (isEnemyBullet && other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            
        }
        Destroy(gameObject); // Destruye la bala al colisionar
    }
}
