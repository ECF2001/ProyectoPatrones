using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Debug.Log("Intentando mover...");
        rb.MovePosition(rb.position + new Vector2(3f, 0));
    }
}
