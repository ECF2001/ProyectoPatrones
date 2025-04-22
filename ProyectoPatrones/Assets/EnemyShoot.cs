using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 1f;
    public float bulletSpeed = 5f;
    private float fireTimer = 0f;
  

    private Transform targetPlayer;

    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform; // Aseg�rate que el jugador tenga la tag "Player"
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            fireTimer = 0f;
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        GameObject bulletGO = Instantiate(bulletPrefab,transform.position, transform.rotation);
        bulletGO.GetComponent<Bullet>().isEnemyBullet = true;

        if (targetPlayer == null) return;

        Vector2 direction = (targetPlayer.position - shootPoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }
}
