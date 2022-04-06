using UnityEngine;

public class Star : MonoBehaviour
{
    // Pick up bonus
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            BonusController.Instance.StarPickUp();
            Destroy(gameObject);
        }
    }
}
