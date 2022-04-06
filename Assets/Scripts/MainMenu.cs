using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("MenuElements")]
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject transition;
    [SerializeField] private GameObject bindings;
    [SerializeField] private GameObject bindingsRus;
    [SerializeField] private Slider volumeSlider;

    [Header("ButtonTexts")]
    [SerializeField] private Text fwKeyText;
    [SerializeField] private Text bwKeyText;
    [SerializeField] private Text jumpKeyText;
    [SerializeField] private Text attackKeyText;
    [SerializeField] private Text crouchKeyText;
    [SerializeField] private Text reloadKeyText;
    [SerializeField] private Text torKeyText;
    [SerializeField] private Text tosKeyText;
    [SerializeField] private Text tosiKeyText;

    // Audio volume
    private float oldVolume;

    void Start()
    {
        SettingsOKButton();
        oldVolume = 0;
        // Set volume slider value to saved volume value (or set to default if no data saved)
        SetVolumeSliderValue();
    }

    void Update()
    {
        // Check if player changes slider value and then set new volume to that value
        if (oldVolume != volumeSlider.value)
        {
            SetNewVolume();
        }

        // Show description near bindings in chosen language
        bindings.SetActive(Language.eng);
        bindingsRus.SetActive(!Language.eng);

        // Set buttons text to string variables from InputManager script
        SetButtonsText();
    }


    // Public methods for buttons

    public void Play()
    {
        StartCoroutine(TransitionToPlay());
    }

    // Deactivate menu buttons and activate settings menu
    public void Settings()
    {
        menuButtons.SetActive(false);
        settings.SetActive(true);

    }

    public void Exit()
    {
        Application.Quit();
    }

    // Activate menu buttons and deactivate settings menu
    public void SettingsOKButton()
    {
        menuButtons.SetActive(true);
        settings.SetActive(false);
    }
    
    // When pressed "Play", play animation and load next scene in 0.55 sec
    private IEnumerator TransitionToPlay()
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(0.55f);
        SceneManager.LoadScene(1);
    }

    // Volume methods

    private void SetVolumeSliderValue()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            volumeSlider.value = 0.5f;
        }
    }

    private void SetNewVolume()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.Save();
        oldVolume = volumeSlider.value;
    }

    // Set buttons text
    private void SetButtonsText()
    {
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
}
