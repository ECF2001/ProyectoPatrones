using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemyPrefab;  // Prefab del enemigo

    public GameObject CreateEnemy()
    {
        // Instancia un enemigo fuera de la vista del jugador (fuera de la cámara)
        Vector3 offscreenPos = new Vector3(-100, -100, 0);  // Puedes cambiar esto según tu necesidad
        GameObject enemy = Instantiate(enemyPrefab, offscreenPos, Quaternion.identity);
        enemy.SetActive(false);  // Desactiva el enemigo para que no sea visible
        return enemy;
    }
}
