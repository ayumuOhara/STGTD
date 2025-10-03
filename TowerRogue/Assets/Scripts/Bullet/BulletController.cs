using Photon.Pun;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    string shooterTag = string.Empty;

    float moveSpeed = 10.0f;
    Vector3 moveVector = Vector3.zero;

    public void SetShooter(string shooter)
    {
        shooterTag = shooter;
    }

    void OnEnable()
    {
        moveVector = transform.right.normalized;
    }

    void Update()
    {
        Move();
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    void Move()
    {
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shooterTag == "Player" && !collision.gameObject.CompareTag("Player"))
        {
            EnemyController e = collision.gameObject.GetComponent<EnemyController>();
            e.enemy.TakeDamage(1);
        }
        else if (shooterTag == "Enemy" && !collision.gameObject.CompareTag("Enemy"))
        {
            PlayerController p = collision.gameObject.GetComponent<PlayerController>();
            p.player.TakeDamage(1);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
