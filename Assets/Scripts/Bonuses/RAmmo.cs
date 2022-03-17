using UnityEngine;

public class RAmmo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SoundController.Instance.ammoS();
            StartCoroutine(GameManager.Instance.AmmoBonus(30, 1));
            Destroy(gameObject);
        }
    }
}
