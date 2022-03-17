using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Objects
    [SerializeField] private GameObject[] riflerElements;
    [SerializeField] private GameObject[] sniperElements;
    [SerializeField] private GameObject[] sicklerElements;

    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject soundController;

    // HP
    [SerializeField] private Image HPBarImage;

    // Ammo UI
    [SerializeField] private Text ammoInRiflerMagText;
    [SerializeField] private Text ammoInRiflerStockText;
    [SerializeField] private Text ammoInSniperMagText;
    [SerializeField] private Text ammoInSniperStockText;

    void Start()
    {
        Instance = this;

        Time.timeScale = 1;
        deathMenu.SetActive(false);
    }

    void Update()
    {
        if (Player.riflerIsDead && Player.sniperIsDead && Player.sicklerIsDead)
        {
            deathMenu.SetActive(true);
            StartCoroutine(TimeStop());
        }

        // Rifler ammo UI
        if (Player.character == "Rifler")
        {
            ammoInRiflerMagText.text = Rifler.Instance.ammoInMag.ToString();
            ammoInRiflerStockText.text = "/" + Rifler.Instance.ammoInStock.ToString();
        }

        // Sniper ammo UI
        if (Player.character == "Sniper")
        {
            ammoInSniperMagText.text = Sniper.Instance.ammoInMag.ToString();
            ammoInSniperStockText.text = "/" + Sniper.Instance.ammoInStock.ToString();
         
            
        }
    }

    public IEnumerator AmmoBonus(int ammo, int bulletType)
    {
        if (bulletType == 0)
        {
            if (Sniper.Instance.isBonusActive)
            {
                while (Sniper.Instance.isBonusActive)
                {
                    yield return null;
                }
                Sniper.Instance.ammoInStock += ammo;
            }
            else
            {
                Sniper.Instance.ammoInStock += ammo;
            }
        }
        else
        {
            Rifler.Instance.ammoInStock += ammo;
        }
    }


    // Display HP ammount
    public void HPBarFill(int HP, float multiplier)
    {
        HPBarImage.fillAmount = HP * multiplier;
    }

   
    // Pause soundtrack and stop time
    IEnumerator TimeStop()
    {
        PauseMenu.isPaused = true;
        OST.Instance.WhenPaused();
        yield return null;
        Time.timeScale = 0;
    }

    // Characters elements control

    public void SwitchToRifler()
    {
        foreach (GameObject go in riflerElements)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in sniperElements)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in sicklerElements)
        {
            go.SetActive(false);
        }
    }

    public void SwitchToSniper()
    {
        foreach (GameObject go in riflerElements)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in sniperElements)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in sicklerElements)
        {
            go.SetActive(false);
        }
    }

    public void SwitchToSickler()
    {
        foreach (GameObject go in riflerElements)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in sniperElements)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in sicklerElements)
        {
            go.SetActive(true);
        }
    }
}
