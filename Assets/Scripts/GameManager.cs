using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Different characters elements
    [SerializeField] private GameObject[] riflerElements;
    [SerializeField] private GameObject[] sniperElements;
    [SerializeField] private GameObject[] sicklerElements;

    // Pause menu
    public bool isPaused;

    [SerializeField] private GameObject pauseElements;
    [SerializeField] private GameObject[] engElements;
    [SerializeField] private GameObject[] rusElements;

    // Death menu
    [SerializeField] private GameObject deathMenu;

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

        isPaused = false;
        Time.timeScale = 1;
        deathMenu.SetActive(false);
        pauseElements.SetActive(false);

        // Language
        foreach (GameObject go in engElements)
        {
            go.SetActive(Language.eng);
        }
        foreach (GameObject go in rusElements)
        {
            go.SetActive(!Language.eng);
        }
    }

    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                StartCoroutine(IsPaused());
                pauseElements.SetActive(true);
                Time.timeScale = 0;
                OST.Instance.WhenPaused();
            }
            else
            {
                Continue();
            }
        }

        // Death
        if (Player.riflerIsDead && Player.sniperIsDead && Player.sicklerIsDead)
        {
            deathMenu.SetActive(true);
            StartCoroutine(TimeStop());
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


    // Pause and death menu

    public void Continue()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseElements.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Player.riflerIsDead = false;
        Player.sniperIsDead = false;
        Player.sicklerIsDead = false;
        CharacterChangeCode.canChange = true;
        StartCoroutine(LoadActiveScene());
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        Player.riflerIsDead = false;
        Player.sniperIsDead = false;
        Player.sicklerIsDead = false;
        StartCoroutine(LoadMainMenuScene());
    }

    public IEnumerator LoadActiveScene()
    {
        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator LoadMainMenuScene()
    {
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
        Time.timeScale = 0;
        isPaused = true;
        OST.Instance.WhenPaused();
        yield return null;
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
