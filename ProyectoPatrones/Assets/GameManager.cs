using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectPool objectPool;  // Referencia al ObjectPool
    public int enemiesToSpawn = 10;  // Número de enemigos a aparecer al inicio
    public float spawnDelay = 1f;  // Tiempo de espera entre enemigos
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = objectPool.GetEnemy();  // Obtener un enemigo de la pool

            // Establece una posición aleatoria dentro del área definida
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0
            );

            enemy.transform.position = spawnPos;  // Coloca el enemigo en la escena
            enemy.SetActive(true);  // Activa el enemigo

            yield return new WaitForSeconds(spawnDelay);  // Espera antes de crear el siguiente enemigo
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja un rectángulo para visualizar el área de spawn
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((spawnAreaMin + spawnAreaMax) / 2, new Vector3(spawnAreaMax.x - spawnAreaMin.x, spawnAreaMax.y - spawnAreaMin.y, 0));
    }
}
