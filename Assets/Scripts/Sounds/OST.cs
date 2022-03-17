using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OST : MonoBehaviour
{
    public static OST Instance { get; private set; }

    public AudioSource ost;
    public bool doesContinueFromPreviousScene;
    public bool notPlayableScene;
    public bool bossScene;

    void Start()
    {
        Instance = this;

        if (PlayerPrefs.HasKey("volume"))
            ost.volume = PlayerPrefs.GetFloat("volume");
        else
            ost.volume = 0.1f;

        if (doesContinueFromPreviousScene)
        {
            ost.time = PlayerPrefs.GetFloat("timeAudioPrevious");
            ost.Play();
        }
    }

    void Update()
    {
        PlayerPrefs.SetFloat("timeAudioPrevious", ost.time);
        if (ost.volume != PlayerPrefs.GetFloat("volume") * 0.5f && !bossScene && !GameManager.Instance.isPaused)
            StartCoroutine(WhenUnpaused());

        if (bossScene && !GameManager.Instance.isPaused)
            ost.volume = PlayerPrefs.GetFloat("volume");
    }

    public void WhenPaused()
    {
        ost.volume = PlayerPrefs.GetFloat("volume") * 0.1f;
    }

    IEnumerator WhenUnpaused()
    {
        yield return null;
        if (!GameManager.Instance.isPaused || notPlayableScene)
            ost.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
    }
}
