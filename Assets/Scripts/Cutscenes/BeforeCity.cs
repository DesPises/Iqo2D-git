using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeCity : MonoBehaviour
{
    [SerializeField] private GameObject[] dialogueElementsEng;
    [SerializeField] private GameObject[] dialogueElementsRus;

    [SerializeField] private Animator riflerAnim;
    [SerializeField] private Animator sniperAnim;
    [SerializeField] private Animator sicklerAnim;

    private bool cooldown;
    private int index = 0;

    void Start()
    {
        StartCoroutine(Cooldown());
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !cooldown)
        {
            index++;
        }

        if (Language.eng)
        {
            switch (index)
            {
                case 0:
                    { dialogueElementsEng[0].SetActive(true); dialogueElementsEng[1].SetActive(true); sicklerAnim.SetBool("talk", true); }
                    break;
                case 1:
                    { dialogueElementsEng[1].SetActive(false); dialogueElementsEng[2].SetActive(true); sicklerAnim.SetBool("talk", false); riflerAnim.SetBool("talk", true); }
                    break;
                case 2:
                    { dialogueElementsEng[2].SetActive(false); dialogueElementsEng[3].SetActive(true); riflerAnim.SetBool("talk", false); sicklerAnim.SetBool("talk", true); }
                    break;
                case 3:
                    { dialogueElementsEng[3].SetActive(false); dialogueElementsEng[4].SetActive(true); dialogueElementsEng[12].SetActive(true); }
                    break;
                case 4:
                    { dialogueElementsEng[4].SetActive(false); dialogueElementsEng[5].SetActive(true); sicklerAnim.SetBool("talk", false); sniperAnim.SetBool("talk", true); dialogueElementsEng[12].SetActive(false); }
                    break;
                case 5:
                    { dialogueElementsEng[5].SetActive(false); dialogueElementsEng[6].SetActive(true); sniperAnim.SetBool("talk", false); sicklerAnim.SetBool("talk", true); }
                    break;
                case 6:
                    { dialogueElementsEng[6].SetActive(false); dialogueElementsEng[7].SetActive(true); sicklerAnim.SetBool("talk", false); sniperAnim.SetBool("talk", true); }
                    break;
                case 7:
                    { dialogueElementsEng[7].SetActive(false); dialogueElementsEng[8].SetActive(true); sniperAnim.SetBool("talk", false); sicklerAnim.SetBool("talk", true); }
                    break;
                case 8:
                    { dialogueElementsEng[8].SetActive(false); dialogueElementsEng[9].SetActive(true); sicklerAnim.SetBool("talk", false); riflerAnim.SetBool("talk", true); }
                    break;
                case 9:
                    { dialogueElementsEng[9].SetActive(false); dialogueElementsEng[10].SetActive(true); riflerAnim.SetBool("talk", false); sicklerAnim.SetBool("talk", true); }
                    break;
                case 10:
                    { dialogueElementsEng[10].SetActive(false); dialogueElementsEng[11].SetActive(true); sicklerAnim.SetBool("talk", false); riflerAnim.SetBool("talk", true); }
                    break;
                case 11:
                    { SceneManager.LoadScene(4); }
                    break;

            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    { dialogueElementsRus[0].SetActive(true); dialogueElementsRus[1].SetActive(true); sicklerAnim.SetBool("talk", true); }
                    break;
                case 1:
                    { dialogueElementsRus[1].SetActive(false); dialogueElementsRus[2].SetActive(true); sicklerAnim.SetBool("talk", false); riflerAnim.SetBool("talk", true); }
                    break;
                case 2:
                    { dialogueElementsRus[2].SetActive(false); dialogueElementsRus[3].SetActive(true); riflerAnim.SetBool("talk", false); sicklerAnim.SetBool("talk", true); }
                    break;
                case 3:
                    { dialogueElementsRus[3].SetActive(false); dialogueElementsRus[4].SetActive(true); dialogueElementsRus[12].SetActive(true); }
                    break;
                case 4:
                    { dialogueElementsRus[4].SetActive(false); dialogueElementsRus[5].SetActive(true); sicklerAnim.SetBool("talk", false); sniperAnim.SetBool("talk", true); dialogueElementsRus[12].SetActive(false); }
                    break;
                case 5:
                    { dialogueElementsRus[5].SetActive(false); dialogueElementsRus[6].SetActive(true); sniperAnim.SetBool("talk", false); sicklerAnim.SetBool("talk", true); }
                    break;
                case 6:
                    { dialogueElementsRus[6].SetActive(false); dialogueElementsRus[7].SetActive(true); sicklerAnim.SetBool("talk", false); sniperAnim.SetBool("talk", true); }
                    break;
                case 7:
                    { dialogueElementsRus[7].SetActive(false); dialogueElementsRus[8].SetActive(true); sniperAnim.SetBool("talk", false); sicklerAnim.SetBool("talk", true); }
                    break;
                case 8:
                    { dialogueElementsRus[8].SetActive(false); dialogueElementsRus[9].SetActive(true); sicklerAnim.SetBool("talk", false); riflerAnim.SetBool("talk", true); }
                    break;
                case 9:
                    { dialogueElementsRus[9].SetActive(false); dialogueElementsRus[10].SetActive(true); riflerAnim.SetBool("talk", false); sicklerAnim.SetBool("talk", true); }
                    break;
                case 10:
                    { dialogueElementsRus[10].SetActive(false); dialogueElementsRus[11].SetActive(true); sicklerAnim.SetBool("talk", false); riflerAnim.SetBool("talk", true); }
                    break;
                case 11:
                    { SceneManager.LoadScene(4); }
                    break;

            }
        }
    }

    IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(2.5f);
        cooldown = false;
    }
}
