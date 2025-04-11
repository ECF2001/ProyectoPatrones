using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    public float lifetime = 30f; // Tiempo de vida de la bala    
    public float damage = 10f; // Daño de la bala

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si la colisión es con un enemigo
        if (other.CompareTag("Enemy"))
        {
            // Obtiene el componente Enemy del objeto con el que colisionó
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Llama al método TakeDamage del enemigo
            }
            Destroy(gameObject); // Destruye la bala después de hacer daño
        }
    }
}
