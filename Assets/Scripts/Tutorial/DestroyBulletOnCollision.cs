using UnityEngine;

public class DestroyBulletOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("SniperBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
