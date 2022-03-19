using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private Text timeText;

    void Start()
    {
        StartCoroutine(LevelTimerCoroutine());   
    }

    IEnumerator LevelTimerCoroutine()
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
