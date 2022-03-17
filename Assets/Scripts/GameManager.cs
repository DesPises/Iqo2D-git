using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // Pause menu
    public bool isPaused;

    [SerializeField] private GameObject pauseElements;
    [SerializeField] private GameObject engElements;
    [SerializeField] private GameObject rusElements;

    void Start()
    {
        Instance = this;

        isPaused = false;
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

        // Pause
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            StartCoroutine(IsPaused());
            if (Language.eng)
            {
                engElements.SetActive(true);
                rusElements.SetActive(false);
            }
            if (!Language.eng)
            {
                engElements.SetActive(false);
                rusElements.SetActive(true);
            }

            pauseElements.SetActive(true);
            Time.timeScale = 0;

            if (ostGO != null)
                ostGO.GetComponent<OST>().WhenPaused();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Continue();
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


    // Pause
    public void Continue()
    {
        isPaused = false;
        pauseElements.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Player.riflerIsDead = false;
        Player.sniperIsDead = false;
        Player.sicklerIsDead = false;
        CharacterChangeCode.canChange = true;
        StartCoroutine(RestartC());
    }

    public void MainMenu()
    {
        Player.riflerIsDead = false;
        Player.sniperIsDead = false;
        Player.sicklerIsDead = false;
        StartCoroutine(MainMenuC());
    }

    public IEnumerator RestartC()
    {
        Time.timeScale = 1;
        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator MainMenuC()
    {
        Time.timeScale = 1;
        yield return null;
        SceneManager.LoadScene(0);
    }

    IEnumerator IsPaused()
    {
        yield return null;
        isPaused = true;
    }
    IEnumerator TimeStop()
    {
        isPaused = true;
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
