using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLanguage : MonoBehaviour
{
    public GameObject plRu, plEng, setRu, setEng, quitRu, quitEng;

    void Update()
    {
        if (Language.eng)
        {
            plRu.SetActive(false);
            setRu.SetActive(false);
            quitRu.SetActive(false);
            plEng.SetActive(true);
            setEng.SetActive(true);
            quitEng.SetActive(true);
        }

        if (!Language.eng)
        {
            plRu.SetActive(true);
            setRu.SetActive(true);
            quitRu.SetActive(true);
            plEng.SetActive(false);
            setEng.SetActive(false);
            quitEng.SetActive(false);
        }
    }
}
