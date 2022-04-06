using System.Collections;
using UnityEngine;

public class OST : MonoBehaviour
{
    public static OST Instance { get; private set; }

    [SerializeField] private AudioSource ost;
    [SerializeField] private bool doesContinueFromPreviousScene;
    [SerializeField] private bool notPlayableScene;
    [SerializeField] private bool bossScene;

    void Start()
    {
        Instance = this;

        if (PlayerPrefs.HasKey("volume"))
        {
            ost.volume = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            ost.volume = 0.1f;
        }

        if (doesContinueFromPreviousScene)
        {
            ost.time = PlayerPrefs.GetFloat("timeAudioPrevious");
            ost.Play();
        }
    }

    void Update()
    {
        PlayerPrefs.SetFloat("timeAudioPrevious", ost.time);
        if (ost.volume != PlayerPrefs.GetFloat("volume") * 0.5f && !bossScene && GameManager.Instance)
        {
            if (!GameManager.Instance.isPaused)
            {
                StartCoroutine(WhenUnpaused());
            }
        }
        if (notPlayableScene)
        {
            StartCoroutine(WhenUnpaused());
        }

        if (bossScene && !GameManager.Instance.isPaused)
        {
            ost.volume = PlayerPrefs.GetFloat("volume");
        }
    }

    public void WhenPaused()
    {
        ost.volume = PlayerPrefs.GetFloat("volume") * 0.1f;
    }

    private IEnumerator WhenUnpaused()
    {
        yield return null;
        ost.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
    }
}
