using UnityEngine;

public class SAmmo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SoundController.Instance.ammoS();
            StartCoroutine(GameManager.Instance.AmmoBonus(5, 0));
            Destroy(gameObject);
        }
    }
}
