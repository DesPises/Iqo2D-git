using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogueElementsEng;
    [SerializeField] private GameObject[] dialogueElementsRus;

    [SerializeField] private Animator riflerAnim;
    [SerializeField] private Animator sniperAnim;
    [SerializeField] private Animator sicklerAnim;
    [SerializeField] private GameObject cameraObject;

    private int index = 0;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            index++;
        }

        if (Language.eng)
        {
            switch (index)
            {
                case 0:
                    { dialogueElementsEng[0].SetActive(true); dialogueElementsEng[1].SetActive(true); }
                    break;
                case 1:
                    { dialogueElementsEng[1].SetActive(false); dialogueElementsEng[2].SetActive(true); StartCoroutine(CameraMoveToSickler()); }
                    break;
                case 2:
                    { dialogueElementsEng[2].SetActive(false); dialogueElementsEng[3].SetActive(true); sicklerAnim.SetBool("talk", true); }
                    break;
                case 3:
                    { dialogueElementsEng[3].SetActive(false); dialogueElementsEng[4].SetActive(true); sicklerAnim.SetBool("talk", false); StartCoroutine(CameraMoveToRifler()); riflerAnim.SetBool("talk", true); }
                    break;
                case 4:
                    { dialogueElementsEng[4].SetActive(false); dialogueElementsEng[5].SetActive(true); StartCoroutine(CameraMoveToSicklerAgain()); sicklerAnim.SetBool("talk", true); riflerAnim.SetBool("talk", false); }
                    break;
                case 5:
                    { dialogueElementsEng[5].SetActive(false); dialogueElementsEng[6].SetActive(true); StartCoroutine(CameraMoveToRifler()); riflerAnim.SetBool("talk", true); }
                    break;
                case 6:
                    { dialogueElementsEng[6].SetActive(false); dialogueElementsEng[7].SetActive(true); riflerAnim.SetBool("talk", false); }
                    break;
                case 7:
                    { dialogueElementsEng[7].SetActive(false); dialogueElementsEng[8].SetActive(true); sniperAnim.SetBool("talk", true); }
                    break;
                case 8:
                    SceneManager.LoadScene(2);
                    break;

            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    { dialogueElementsRus[0].SetActive(true); dialogueElementsRus[1].SetActive(true); }
                    break;
                case 1:
                    { dialogueElementsRus[1].SetActive(false); dialogueElementsRus[2].SetActive(true); StartCoroutine(CameraMoveToSickler()); }
                    break;
                case 2:
                    { dialogueElementsRus[2].SetActive(false); dialogueElementsRus[3].SetActive(true); sicklerAnim.SetBool("talk", true); }
                    break;
                case 3:
                    { dialogueElementsRus[3].SetActive(false); dialogueElementsRus[4].SetActive(true); sicklerAnim.SetBool("talk", false); StartCoroutine(CameraMoveToRifler()); riflerAnim.SetBool("talk", true); }
                    break;
                case 4:
                    { dialogueElementsRus[4].SetActive(false); dialogueElementsRus[5].SetActive(true); StartCoroutine(CameraMoveToSicklerAgain()); sicklerAnim.SetBool("talk", true); riflerAnim.SetBool("talk", false); }
                    break;
                case 5:
                    { dialogueElementsRus[5].SetActive(false); dialogueElementsRus[6].SetActive(true); StartCoroutine(CameraMoveToRifler()); riflerAnim.SetBool("talk", true); }
                    break;
                case 6:
                    { dialogueElementsRus[6].SetActive(false); dialogueElementsRus[7].SetActive(true); riflerAnim.SetBool("talk", false); }
                    break;
                case 7:
                    { dialogueElementsRus[7].SetActive(false); dialogueElementsRus[8].SetActive(true); sniperAnim.SetBool("talk", true); }
                    break;
                case 8:
                    SceneManager.LoadScene(2);
                    break;

            }
        }
    }

    private IEnumerator CameraMoveToSickler()
    {
        while (cameraObject.transform.position.x < 8)
        {
            cameraObject.transform.position += new Vector3(0.1f, 0, 0) * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CameraMoveToRifler()
    {
        while (cameraObject.transform.position.x > -2)
        {
            cameraObject.transform.position -= new Vector3(2, 0, 0) * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CameraMoveToSicklerAgain()
    {
        while (cameraObject.transform.position.x < 10)
        {
            cameraObject.transform.position += new Vector3(2, 0, 0) * Time.deltaTime;
            yield return null;
        }
    }
}
