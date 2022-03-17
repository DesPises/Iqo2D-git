using UnityEngine;

public class Distance : MonoBehaviour
{
    // Destroy bullet on enter trigger zone
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
        }
    }
}
