using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemyPrefab;  // Prefab del enemigo

    public GameObject CreateEnemy()
    {
        Vector3 offscreenPos = new Vector3(-100, -100, 0);
        GameObject enemy = Instantiate(enemyPrefab, offscreenPos, Quaternion.identity);
        enemy.SetActive(false);
        return enemy;
    }
}
