using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 30f; 

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
    
        Destroy(gameObject);
    }
    */
}
