using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject menu, settings, PerehodGO, bindings, bindingsr;
    public Slider slideVolume;
    public float oldVolume;
    public Text buttonText, fwKeyText, bwKeyText, jumpKeyText, attackKeyText, crouchKeyText, reloadKeyText, torKeyText, tosKeyText, tosiKeyText;

    Event keyEvent;
    KeyCode newKey;
    bool waitingForKey;


    void Start()
    {
        menu.SetActive(true);
        settings.SetActive(false);
        Time.timeScale = 1;
        
        oldVolume = slideVolume.value;
        if (!PlayerPrefs.HasKey("volume"))
        {
            slideVolume.value = 0.5f;
        }
        else slideVolume.value = PlayerPrefs.GetFloat("volume");

        fwKeyText.text = InputManager.IM.fwKey.ToString();
        bwKeyText.text = InputManager.IM.bwKey.ToString();
        attackKeyText.text = InputManager.IM.attackKey.ToString();
        jumpKeyText.text = InputManager.IM.jumpKey.ToString();
        crouchKeyText.text = InputManager.IM.crouchKey.ToString();
        reloadKeyText.text = InputManager.IM.reloadKey.ToString();
        torKeyText.text = InputManager.IM.torKey.ToString();
        tosKeyText.text = InputManager.IM.tosKey.ToString();
        tosiKeyText.text = InputManager.IM.tosiKey.ToString();
    }

    void Update()
    {
        if (oldVolume != slideVolume.value)
        {
            PlayerPrefs.SetFloat("volume", slideVolume.value);
            PlayerPrefs.Save();
            oldVolume = slideVolume.value;
        }
        if (Language.eng)
        {
            bindings.SetActive(true);
            bindingsr.SetActive(false);
        }
        if (!Language.eng)
        {
            bindings.SetActive(false);
            bindingsr.SetActive(true);
        }
    }

    public void Play()
    {
        StartCoroutine(Perehod());

    }
    public void Settings()
    {
        menu.SetActive(false);
        settings.SetActive(true);

    }
    public void SettingsOKButton()
    {
        menu.SetActive(true);
        settings.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator Perehod()
    {
        PerehodGO.SetActive(true);
        yield return new WaitForSeconds(0.55f);
        SceneManager.LoadScene(1);
    }

    void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
            case "fwKey":
                InputManager.IM.fwKey = newKey;
                fwKeyText.text = InputManager.IM.fwKey.ToString();
                PlayerPrefs.SetString("fwKey", InputManager.IM.fwKey.ToString());
                break;

            case "bwKey":
                InputManager.IM.bwKey = newKey;
                bwKeyText.text = InputManager.IM.bwKey.ToString();
                PlayerPrefs.SetString("bwKey", InputManager.IM.bwKey.ToString());
                break;

            case "jumpKey":
                InputManager.IM.jumpKey = newKey;
                jumpKeyText.text = InputManager.IM.jumpKey.ToString();
                PlayerPrefs.SetString("jumpKey", InputManager.IM.jumpKey.ToString());
                break;

            case "attackKey":
                InputManager.IM.attackKey = newKey;
                attackKeyText.text = InputManager.IM.attackKey.ToString();
                PlayerPrefs.SetString("attackKey", InputManager.IM.attackKey.ToString());
                break;

            case "crouchKey":
                InputManager.IM.crouchKey = newKey;
                crouchKeyText.text = InputManager.IM.crouchKey.ToString();
                PlayerPrefs.SetString("crouchKey", InputManager.IM.crouchKey.ToString());
                break;

            case "reloadKey":
                InputManager.IM.reloadKey = newKey;
                reloadKeyText.text = InputManager.IM.reloadKey.ToString();
                PlayerPrefs.SetString("reloadKey", InputManager.IM.reloadKey.ToString());
                break;

            case "torKey":
                InputManager.IM.torKey = newKey;
                torKeyText.text = InputManager.IM.torKey.ToString();
                PlayerPrefs.SetString("torKey", InputManager.IM.torKey.ToString());
                break;

            case "tosKey":
                InputManager.IM.tosKey = newKey;
                tosKeyText.text = InputManager.IM.tosKey.ToString();
                PlayerPrefs.SetString("tosKey", InputManager.IM.tosKey.ToString());
                break;

            case "tosiKey":
                InputManager.IM.tosiKey = newKey;
                tosiKeyText.text = InputManager.IM.tosiKey.ToString();
                PlayerPrefs.SetString("tosiKey", InputManager.IM.tosiKey.ToString());
                break;
        }

        yield return null;
    }
}
