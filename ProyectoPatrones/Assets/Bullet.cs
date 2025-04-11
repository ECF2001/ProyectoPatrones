using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    public float lifetime = 30f; // Tiempo de vida de la bala    
    public float damage = 10f; // Da�o de la bala

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si la colisi�n es con un enemigo
        if (other.CompareTag("Enemy"))
        {
            // Obtiene el componente Enemy del objeto con el que colision�
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Llama al m�todo TakeDamage del enemigo
            }
            Destroy(gameObject); // Destruye la bala despu�s de hacer da�o
        }
    }
}
