using UnityEngine;

public class SecondEnemyFactory : MonoBehaviour
{
    public GameObject secondenemyPrefab;  // Prefab del enemigo

    public GameObject CreateEnemy()
    {
        Vector3 offscreenPos = new Vector3(-100, -100, 0);
        GameObject enemy = Instantiate(secondenemyPrefab, offscreenPos, Quaternion.identity);
        enemy.SetActive(false);
        return enemy;
    }
}
