using UnityEngine;

public class WeaponFlip : MonoBehaviour
{
    public Transform player;
    public float offsetX = 0.5f;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - player.position;

        // Determinar si el mouse está a la izquierda o derecha del jugador
        bool isLookingRight = direction.x >= 0f;

        // Posicionar el arma a un lado del jugador
        float xOffset = isLookingRight ? offsetX : -offsetX;
        transform.localPosition = new Vector3(xOffset, transform.localPosition.y, transform.localPosition.z);

        // Rotar horizontalmente el arma sin escalar
        transform.rotation = isLookingRight
            ? Quaternion.identity
            : Quaternion.Euler(0f, 180f, 0f);  // giro horizontal por Y
    }
}
