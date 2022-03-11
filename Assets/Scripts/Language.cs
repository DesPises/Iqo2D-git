using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    public static bool eng;

    [SerializeField] private GameObject engActive;
    [SerializeField] private GameObject rusActive;

    private GameObject[] ruUIElements;
    private GameObject[] engUIElements;

    void Awake()
    {
        // Put english and russian elements in arrays
        ruUIElements = GameObject.FindGameObjectsWithTag("Rus");
        engUIElements = GameObject.FindGameObjectsWithTag("Eng");

        // Set default language to english
        if (!PlayerPrefs.HasKey("eng"))
        {
            PlayerPrefs.SetInt("eng", 1);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        StartCoroutine(ButtonsLanguageChange());
    }

    private void Update()
    {
        // Set public static boolean variable to language value
        if (PlayerPrefs.GetInt("eng") == 1)
        {
            eng = true;
        }
        else
        {
            eng = false;
        }

        // Active one of flags in settings
        if (engActive != null)
        {
            engActive.SetActive(eng);
            rusActive.SetActive(!eng);
        }
    }

    // Public methods to change language when button pressed

    public void SetRus()
    {
        PlayerPrefs.SetInt("eng", 0);
        PlayerPrefs.Save();
        StartCoroutine(ButtonsLanguageChange());
    }

    public void SetEng()
    {
        PlayerPrefs.SetInt("eng", 1);
        PlayerPrefs.Save();
        StartCoroutine(ButtonsLanguageChange());
    }

    // Change buttons text language
    private IEnumerator ButtonsLanguageChange()
    {
        yield return null;

        foreach (GameObject go in engUIElements)
        {
            go.SetActive(eng);
        }
        foreach (GameObject go in ruUIElements)
        {
            go.SetActive(!eng);
        }
    }
}
