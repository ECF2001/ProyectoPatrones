using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 5f;
  

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log($"ShootPoint Pos: {shootPoint.position}, MouseWorld: {Camera.main.ScreenToWorldPoint(Input.mousePosition)}");
        // 1. Obtener direcci�n hacia el mouse
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPosition - shootPoint.position).normalized;

        // 2. Instanciar la bala
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        // 3. Asegurar que la bala est� limpia
        bullet.transform.localScale = Vector3.one;
        bullet.transform.rotation = Quaternion.identity;

        // 4. Aplicar direcci�n
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;  // Usa .velocity si est�s en Unity <2023
                                                // rb.linearVelocity = direction * bulletSpeed; // Usa este si est�s en Unity 2023+
        rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 5. Marcar como bala del jugador
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.isEnemyBullet = false;
        }

        Debug.Log($"Disparo en direcci�n: {direction}, posici�n inicial: {shootPoint.position}");
    }


}