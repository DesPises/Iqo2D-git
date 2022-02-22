using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseGO, cGO, crGO, rGO, rrGO, mGO, mrGO, ostGO;
    public static bool isPaused;

    void Start()
    {
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            StartCoroutine(IsPaused());
            if (Language.eng)
            {
                cGO.SetActive(true);
                rGO.SetActive(true);
                mGO.SetActive(true);
                crGO.SetActive(false);
                rrGO.SetActive(false);
                mrGO.SetActive(false);
            }
            if (!Language.eng)
            {
                crGO.SetActive(true);
                rrGO.SetActive(true);
                mrGO.SetActive(true);
                cGO.SetActive(false);
                rGO.SetActive(false);
                mGO.SetActive(false);
            }

            pauseGO.SetActive(true);
            Time.timeScale = 0;

            if (ostGO != null)
                ostGO.GetComponent<OST>().WhenPaused();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Continue();
        }
    }

    public void Continue()
    {
        isPaused = false;
        pauseGO.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        GameManager.rIsDead = false;
        GameManager.sIsDead = false;
        GameManager.siIsDead = false;
        CharacterChangeCode.CanChange = true;
        StartCoroutine(RestartC());
    }

    public void MainMenu()
    {
        GameManager.rIsDead = false;
        GameManager.sIsDead = false;
        GameManager.siIsDead = false;
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
}
