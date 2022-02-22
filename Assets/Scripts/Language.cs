using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    public static bool eng;
    public GameObject engActive, rusActive;

    void Start()
    {
        if (!PlayerPrefs.HasKey("eng"))
        {
            PlayerPrefs.SetInt("eng", 1);
            PlayerPrefs.Save();
            eng = true;
            if (engActive != null)
            {
                rusActive.SetActive(false);
                engActive.SetActive(true);
            }
        }

        if (PlayerPrefs.GetInt("eng") == 0)
        {
            eng = false;
            if (engActive != null)
            {
                rusActive.SetActive(true);
                engActive.SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("eng") == 1)
        {
            eng = true;
            if (engActive != null)
            {
                rusActive.SetActive(false);
                engActive.SetActive(true);
            }
        }
    }

    public void SetRus()
    {
        PlayerPrefs.SetInt("eng", 0);
        PlayerPrefs.Save();
        eng = false;
        rusActive.SetActive(true);
        engActive.SetActive(false);
    }

    public void SetEng()
    {
        PlayerPrefs.SetInt("eng", 1);
        PlayerPrefs.Save();
        eng = true;
        rusActive.SetActive(false);
        engActive.SetActive(true);
    }
}
