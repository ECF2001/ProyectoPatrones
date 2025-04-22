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

        if (player == null)
        {
            Debug.LogError("No se puede generar el objeto porque la referencia al jugador es nula.");
            return;
        }

        Vector2 playerPos = new Vector2(player.position.x, player.position.y + 1.5f);
        Instantiate(item, playerPos, Quaternion.identity);
        Debug.Log($"Objeto {item.name} generado en la posición {playerPos}.");
    }
}
