using UnityEngine;

public class HPBonus : MonoBehaviour
{
    [SerializeField] private GameObject vodka;
    [SerializeField] private GameObject shawa;
    [SerializeField] private GameObject kebab;

    void Update()
    {
        if (Player.character == "Rifler")
        {
            vodka.SetActive(true);
            shawa.SetActive(false);
            kebab.SetActive(false);
        }
        if (Player.character == "Sniper")
        {
            vodka.SetActive(false);
            shawa.SetActive(true);
            kebab.SetActive(false);
        }
        if (Player.character == "Sickler")
        {
            vodka.SetActive(false);
            shawa.SetActive(false);
            kebab.SetActive(true);
        }
    }
}
