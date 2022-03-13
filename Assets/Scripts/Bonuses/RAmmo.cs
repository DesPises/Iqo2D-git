using UnityEngine;

public class RAmmo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SoundController.Instance.ammoS();
            Rifler.Instance.ammoInStock += 30;
            Destroy(gameObject);
        }
    }
}
