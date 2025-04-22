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
        // Obtener posición del mouse en el mundo
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;

        // Calcular dirección
        Vector2 direction = (target - shootPoint.position).normalized;

        // Instanciar bala
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        // Ajustar escala y dirección
        bullet.transform.localScale = Vector3.one;

        // Configurar física
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed; // <- usa velocity en lugar de linearVelocity
        rb.MoveRotation(Quaternion.FromToRotation(Vector3.up, direction).eulerAngles.z); // Rotar la bala correctamente

        // Configurar comportamiento de la bala
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.isEnemyBullet = false;
        }

        Debug.Log("Bala disparada");
    }


}