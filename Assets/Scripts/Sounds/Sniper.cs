using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public GameObject soundContrGO;

    public void runSound()
    {
        soundContrGO.GetComponent<SoundController>().RunS();
    }

    public void jumpSound()
    {
        soundContrGO.GetComponent<SoundController>().JumpS();
    }

    public void reloadSound()
    {
        soundContrGO.GetComponent<SoundController>().svdReloadS();
    }

    
}
