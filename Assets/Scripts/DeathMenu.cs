using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void Restart()
    {
        PauseMenu.isPaused = false;
        GameManager.instance.rIsDead = false;
        GameManager.instance.sIsDead = false;
        GameManager.instance.siIsDead = false;
        CharacterChangeCode.canChange = true;
        StartCoroutine(RestartC());
    }

    public void MainMenu()
    {
        PauseMenu.isPaused = false;
        GameManager.instance.rIsDead = false;
        GameManager.instance.sIsDead = false;
        GameManager.instance.siIsDead = false;
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
}
