using UnityEngine;

public class SAmmo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SoundController.Instance.AmmoSniper();
            StartCoroutine(GameManager.Instance.AmmoBonus(5, 0));
            Destroy(gameObject);
        }
    }
}
