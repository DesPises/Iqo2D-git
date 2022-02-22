using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterCity : MonoBehaviour
{
    public GameObject s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, space, sr1, sr2, sr3, sr4, sr5, sr6, sr7, sr8, sr9, sr10, sr11, sr12, sr13, sr14, sr15, sr16, spacer, cameraGO, sicklerGO, sniperGO;
    public int i = 0;
    public Animator rAnim, sAnim, siAnim;
    public Camera cam;
    private bool cooldown;

    void Start()
    {
        cooldown = true;
        StartCoroutine(CooldownStart());
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !cooldown)
        {
            i++;
        }
        if (Language.eng)
        {
            switch (i)
            {
                case 0:
                    { s1.SetActive(true); space.SetActive(true); rAnim.SetBool("talk", true); }
                    break;
                case 1:
                    { s1.SetActive(false); s2.SetActive(true); rAnim.SetBool("talk", false); siAnim.SetBool("talk", true); }
                    break;
                case 2:
                    { s2.SetActive(false); s3.SetActive(true); }
                    break;
                case 3:
                    { s3.SetActive(false); s4.SetActive(true); siAnim.SetBool("talk", false); sAnim.SetBool("talk", true); }
                    break;
                case 4:
                    { s4.SetActive(false); s5.SetActive(true); sAnim.SetBool("talk", false); rAnim.SetBool("talk", true); }
                    break;
                case 5:
                    { s5.SetActive(false); s6.SetActive(true); rAnim.SetBool("talk", false); siAnim.SetBool("talk", true); }
                    break;
                case 6:
                    { s6.SetActive(false); s7.SetActive(true); siAnim.SetBool("talk", false); rAnim.SetBool("talk", true); }
                    break;
                case 7:
                    { s7.SetActive(false); s8.SetActive(true); rAnim.SetBool("talk", false); siAnim.SetBool("talk", true); rAnim.SetBool("takevodka", true); }
                    break;
                case 8:
                    { s8.SetActive(false); s9.SetActive(true); siAnim.SetBool("talk", false); rAnim.SetBool("talk", true); rAnim.SetBool("takevodka", false); StartCoroutine(DrinkVodka()); }
                    break;
                case 9:
                    { s9.SetActive(false); s10.SetActive(true); }
                    break;
                case 10:
                    { s10.SetActive(false); s11.SetActive(true); rAnim.SetBool("talk", false); sAnim.SetBool("talk", true); }
                    break;
                case 11:
                    { s11.SetActive(false); s12.SetActive(true); sAnim.SetBool("talk", false); rAnim.SetBool("talk", true); }
                    break;
                case 12:
                    { s12.SetActive(false); s13.SetActive(true); rAnim.SetBool("talk", false); sAnim.SetBool("talk", true); }
                    break;
                case 13:
                    { s13.SetActive(false); s14.SetActive(true); sAnim.SetBool("talk", false); siAnim.SetBool("talk", true); sicklerGO.gameObject.transform.eulerAngles = new Vector3(0, 0, 0); }
                    break;
                case 14:
                    { s14.SetActive(false); s15.SetActive(true); siAnim.SetBool("talk", false); rAnim.SetBool("talk", true); }
                    break;
                case 15:
                    { s15.SetActive(false); s16.SetActive(true); rAnim.SetBool("talk", false); sAnim.SetBool("talk", true); sniperGO.gameObject.transform.eulerAngles = new Vector3(0, 180, 0); }
                    break;
                case 16:
                    { SceneManager.LoadScene(6); }
                    break;
            }
        }

        if (!Language.eng)
        {
            switch (i)
            {
                case 0:
                    { sr1.SetActive(true); spacer.SetActive(true); rAnim.SetBool("talk", true); }
                    break;
                case 1:
                    { sr1.SetActive(false); sr2.SetActive(true); rAnim.SetBool("talk", false); siAnim.SetBool("talk", true); }
                    break;
                case 2:
                    { sr2.SetActive(false); sr3.SetActive(true); }
                    break;
                case 3:
                    { sr3.SetActive(false); sr4.SetActive(true); siAnim.SetBool("talk", false); sAnim.SetBool("talk", true); }
                    break;
                case 4:
                    { sr4.SetActive(false); sr5.SetActive(true); sAnim.SetBool("talk", false); rAnim.SetBool("talk", true); }
                    break;
                case 5:
                    { sr5.SetActive(false); sr6.SetActive(true); rAnim.SetBool("talk", false); siAnim.SetBool("talk", true); }
                    break;
                case 6:
                    { sr6.SetActive(false); sr7.SetActive(true); siAnim.SetBool("talk", false); rAnim.SetBool("talk", true); }
                    break;
                case 7:
                    { sr7.SetActive(false); sr8.SetActive(true); rAnim.SetBool("talk", false); siAnim.SetBool("talk", true); rAnim.SetBool("takevodka", true); }
                    break;
                case 8:
                    { sr8.SetActive(false); sr9.SetActive(true); siAnim.SetBool("talk", false); rAnim.SetBool("talk", true); rAnim.SetBool("takevodka", false); StartCoroutine(DrinkVodka()); }
                    break;
                case 9:
                    { sr9.SetActive(false); sr10.SetActive(true); }
                    break;
                case 10:
                    { sr10.SetActive(false); sr11.SetActive(true); rAnim.SetBool("talk", false); sAnim.SetBool("talk", true); }
                    break;
                case 11:
                    { sr11.SetActive(false); sr12.SetActive(true); sAnim.SetBool("talk", false); rAnim.SetBool("talk", true); }
                    break;
                case 12:
                    { sr12.SetActive(false); sr13.SetActive(true); rAnim.SetBool("talk", false); sAnim.SetBool("talk", true); }
                    break;
                case 13:
                    { sr13.SetActive(false); sr14.SetActive(true); sAnim.SetBool("talk", false); siAnim.SetBool("talk", true); sicklerGO.gameObject.transform.eulerAngles = new Vector3(0, 0, 0); }
                    break;
                case 14:
                    { sr14.SetActive(false); sr15.SetActive(true); siAnim.SetBool("talk", false); rAnim.SetBool("talk", true); }
                    break;
                case 15:
                    { sr15.SetActive(false); sr16.SetActive(true); rAnim.SetBool("talk", false); sAnim.SetBool("talk", true); sniperGO.gameObject.transform.eulerAngles = new Vector3(0, 180, 0); }
                    break;
                case 16:
                    { SceneManager.LoadScene(6); }
                    break;
            }
        }
    }

    IEnumerator DrinkVodka()
    {
        rAnim.SetBool("drink", true);
        yield return null;
        rAnim.SetBool("drink", false);
    }

    IEnumerator CooldownStart()
    {
        yield return new WaitForSeconds(3);
        cooldown = false;
    }

}
