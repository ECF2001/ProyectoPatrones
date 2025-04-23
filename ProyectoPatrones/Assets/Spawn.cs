using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player' en la escena.");
        }
    }

    public void SpawnDroppedItem()
    {
        if (item == null)
        {
            Debug.LogError("El prefab 'item' no está asignado en el script Spawn.");
            return;
        }

        Debug.Log($"[Spawn] Spawning item: {item.name}");

        Vector2 playerPos = new Vector2(player.position.x, player.position.y + 1.5f);
        Instantiate(item, playerPos, Quaternion.identity);
    }

}
