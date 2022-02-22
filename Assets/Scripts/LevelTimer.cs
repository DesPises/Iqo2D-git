using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelTimer : MonoBehaviour
{
    public Text timeText;

    void Start()
    {
        StartCoroutine(TheLevelTimer());   
    }

    IEnumerator TheLevelTimer()
    {
        for (int i = 100; i >= 0; i--)
        {
            timeText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        NextScene();
        
    }

    void NextScene()
    {
        SceneManager.LoadScene(5);
    }
}
