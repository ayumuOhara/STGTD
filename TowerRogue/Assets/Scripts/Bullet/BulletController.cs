using Photon.Pun;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float moveSpeed = 5.0f;
    Vector3 moveVector = Vector3.zero;

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
}
