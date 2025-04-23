using UnityEngine;

public class WeaponFlip : MonoBehaviour
{
    public Transform player;
    public float offsetX = 0.5f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - player.position;

        if (direction.x >= 0)
        {
            // Derecha
            transform.localPosition = new Vector3(offsetX, transform.localPosition.y, transform.localPosition.z);
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            // Izquierda
            transform.localPosition = new Vector3(-offsetX, transform.localPosition.y, transform.localPosition.z);
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }
}
