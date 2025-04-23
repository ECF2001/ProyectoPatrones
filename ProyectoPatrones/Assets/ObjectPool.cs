using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public EnemyFactory factory;  // Referencia a EnemyFactory
    public int initialSize = 20;  // Número de enemigos iniciales en la pool

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        // Inicializa la pool de enemigos
        for (int i = 0; i < initialSize; i++)
        {
            GameObject enemy = factory.CreateEnemy();
            pool.Enqueue(enemy);
        }
    }

    // Método para obtener un enemigo del pool
    public GameObject GetEnemy()
    {
        if (pool.Count == 0)
        {
            return factory.CreateEnemy();  // Si no hay enemigos en la pool, crea uno nuevo
        }

        return pool.Dequeue();  // Toma el siguiente enemigo de la pool
    }

    // Método para devolver un enemigo al pool
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);  // Desactiva el enemigo
        pool.Enqueue(enemy);  // Lo devuelve a la pool
    }
}
