using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    public GameObject s1, s2, s3, s4, s5, s6, s7, s8, space, sr1, sr2, sr3, sr4, sr5, sr6, sr7, sr8, spacer, cameraGO;
    public int i = 0;
    public Animator rAnim, sAnim, siAnim;
    public Camera cam;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            i++;
        }

        if (Language.eng)
        {
            switch (i)
            {
                case 0:
                    { s1.SetActive(true); space.SetActive(true); }
                    break;
                case 1:
                    { s1.SetActive(false); s2.SetActive(true); StartCoroutine(CameraMoveToSickler()); }
                    break;
                case 2:
                    { s2.SetActive(false); s3.SetActive(true); siAnim.SetBool("talk", true); }
                    break;
                case 3:
                    { s3.SetActive(false); s4.SetActive(true); siAnim.SetBool("talk", false); StartCoroutine(CameraMoveToRifler()); rAnim.SetBool("talk", true); }
                    break;
                case 4:
                    { s4.SetActive(false); s5.SetActive(true); StartCoroutine(CameraMoveToSicklerAgain()); siAnim.SetBool("talk", true); rAnim.SetBool("talk", false); }
                    break;
                case 5:
                    { s5.SetActive(false); s6.SetActive(true); StartCoroutine(CameraMoveToRifler()); rAnim.SetBool("talk", true); }
                    break;
                case 6:
                    { s6.SetActive(false); s7.SetActive(true); rAnim.SetBool("talk", false); }
                    break;
                case 7:
                    { s7.SetActive(false); s8.SetActive(true); sAnim.SetBool("talk", true); }
                    break;
                case 8:
                    SceneManager.LoadScene(2);
                    break;

            }
        }
       
        if (!Language.eng)
        {
            switch (i)
            {
                case 0:
                    { sr1.SetActive(true); spacer.SetActive(true); }
                    break;
                case 1:
                    { sr1.SetActive(false); sr2.SetActive(true); StartCoroutine(CameraMoveToSickler()); }
                    break;
                case 2:
                    { sr2.SetActive(false); sr3.SetActive(true); siAnim.SetBool("talk", true); }
                    break;
                case 3:
                    { sr3.SetActive(false); sr4.SetActive(true); siAnim.SetBool("talk", false); StartCoroutine(CameraMoveToRifler()); rAnim.SetBool("talk", true); }
                    break;
                case 4:
                    { sr4.SetActive(false); sr5.SetActive(true); StartCoroutine(CameraMoveToSicklerAgain()); siAnim.SetBool("talk", true); rAnim.SetBool("talk", false); }
                    break;
                case 5:
                    { sr5.SetActive(false); sr6.SetActive(true); StartCoroutine(CameraMoveToRifler()); rAnim.SetBool("talk", true); }
                    break;
                case 6:
                    { sr6.SetActive(false); sr7.SetActive(true); rAnim.SetBool("talk", false); }
                    break;
                case 7:
                    { sr7.SetActive(false); sr8.SetActive(true); sAnim.SetBool("talk", true); }
                    break;
                case 8:
                    SceneManager.LoadScene(2);
                    break;

            }
        }
    }

    IEnumerator CameraMoveToSickler()
    {
        while (cameraGO.transform.position.x < 8)
        {
            cameraGO.transform.position += new Vector3(0.1f, 0, 0) * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CameraMoveToRifler()
    {
        while (cameraGO.transform.position.x > -2)
        {
            cameraGO.transform.position -= new Vector3(2, 0, 0) * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator CameraMoveToSicklerAgain()
    {
        while(cameraGO.transform.position.x < 10)
        {
            cameraGO.transform.position += new Vector3(2, 0, 0) * Time.deltaTime;
            yield return null;
        }
    }
}
