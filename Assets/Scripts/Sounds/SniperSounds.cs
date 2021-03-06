using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperSounds : MonoBehaviour
{
    public GameObject soundContrGO;

    public void runSound()
    {
        soundContrGO.GetComponent<SoundController>().Run();
    }

    public void jumpSound()
    {
        soundContrGO.GetComponent<SoundController>().JumpS();
    }

    public void reloadSound()
    {
        soundContrGO.GetComponent<SoundController>().SvdReload();
    }

    
}
