using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text[] hintsEng;
    [SerializeField] private Text[] hintsRus;
    [SerializeField] private GameObject hintsEngObject;
    [SerializeField] private GameObject hintsRusObject;

    [SerializeField] private GameObject smallTarget;
    [SerializeField] private GameObject medTarget;
    [SerializeField] private GameObject bigTarget;

    private int hintIndex;
    private bool playerInTrigger;

    void Start()
    {
        hintIndex = 0;
        Player.character = "Rifler";
        smallTarget.SetActive(false);
        medTarget.SetActive(false);
        bigTarget.SetActive(false);

        // Set text for all hints
        hintsEng[0].text = InputManager.IM.bwKey.ToString() + " / " + InputManager.IM.fwKey.ToString() + " - move";
        hintsRus[0].text = InputManager.IM.bwKey.ToString() + " / " + InputManager.IM.fwKey.ToString() + " - бег";
        hintsEng[1].text = InputManager.IM.crouchKey.ToString() + " - crouch, " + InputManager.IM.attackKey.ToString() + " - attack";
        hintsRus[1].text = InputManager.IM.crouchKey.ToString() + " - присесть, " + InputManager.IM.attackKey.ToString() + " - атака";
        hintsEng[2].text = InputManager.IM.tosiKey.ToString() + " - change character";
        hintsRus[2].text = InputManager.IM.tosiKey.ToString() + " - смена персонажа";

        hintsEng[4].text = InputManager.IM.tosKey.ToString() + " - change character";
        hintsRus[4].text = InputManager.IM.tosKey.ToString() + " - смена персонажа";
        hintsEng[5].text = InputManager.IM.jumpKey.ToString() + " x2 - double jump \n Headshots deal more damage";
        hintsRus[5].text = InputManager.IM.jumpKey.ToString() + " x2 - двойной прыжок \n Хедшоты наносят больше урона";
        hintsEng[6].text = InputManager.IM.torKey.ToString() + " - change character";
        hintsRus[6].text = InputManager.IM.torKey.ToString() + " - смена персонажа";
        hintsEng[7].text = InputManager.IM.reloadKey.ToString() + " - reload";
        hintsRus[7].text = InputManager.IM.reloadKey.ToString() + " - перезарядка";

        // Activate/deactivate eng/rus elements
        if (Language.eng)
        {
            hintsEngObject.SetActive(true);
            hintsRusObject.SetActive(false);
        }
        else
        {
            hintsEngObject.SetActive(false);
            hintsRusObject.SetActive(true);
        }
    }

    void Update()
    {
        // Give player infinite ammo
        StartCoroutine(Rifler.Instance.InfiniteAmmo(9999999));
        StartCoroutine(Sniper.Instance.InfiniteAmmo(9999999));

        // Hints activation
        if (hintIndex == 0)
        {
            hintsEng[hintIndex].gameObject.SetActive(true);
            hintsRus[hintIndex].gameObject.SetActive(true);
        }
        else
        {
            hintsEng[hintIndex - 1].gameObject.SetActive(false);
            hintsRus[hintIndex - 1].gameObject.SetActive(false);
            hintsEng[hintIndex].gameObject.SetActive(true);
            hintsRus[hintIndex].gameObject.SetActive(true);
        }

        // Switching hints
        switch (hintIndex)
        {
            case 0:
                {
                    if (Input.GetKey(InputManager.IM.fwKey))
                    {
                        hintIndex = 1;
                    }
                    Player.sniperIsDead = true;
                    Player.sicklerIsDead = true;
                }
                break;
            case 1:
                {
                    smallTarget.SetActive(true);
                    GetComponent<BoxCollider2D>().offset = new Vector2(5.3f, -2.8f);
                    GetComponent<BoxCollider2D>().size = new Vector2(1.2f, 0.8f);
                }
                break;
            case 2:
                {
                    smallTarget.SetActive(false);
                    Player.sicklerIsDead = false;
                    if (Input.GetKey(InputManager.IM.tosiKey))
                    {
                        hintIndex = 3;
                        Player.riflerIsDead = true;
                    }
                }
                break;
            case 3:
                {
                    medTarget.SetActive(true);
                    if (playerInTrigger)
                    {
                        if (Input.GetKeyDown(InputManager.IM.attackKey) && GameManager.sicklerCanAttack)
                        {
                            hintIndex = 4;
                        }
                    }
                }
                break;
            case 4:
                {
                    medTarget.SetActive(false);
                    Player.sniperIsDead = false;
                    if (Input.GetKey(InputManager.IM.tosKey))
                    {
                        hintIndex = 5;
                        Player.sicklerIsDead = true;
                    }
                }
                break;
            case 5:
                {
                    bigTarget.SetActive(true);
                    GetComponent<BoxCollider2D>().offset = new Vector2(8.3f, 1.6f);
                    GetComponent<BoxCollider2D>().size = new Vector2(1.6f, 1.3f);
                }
                break;
            case 6:
                {
                    bigTarget.SetActive(false);
                    Player.riflerIsDead = false;
                    CharacterChangeCode.canChange = true;
                    if (Input.GetKey(InputManager.IM.torKey))
                    {
                        hintIndex = 7;
                        Player.sniperIsDead = true;
                    }
                }
                break;
            case 7:
                {
                    if (Input.GetKeyDown(InputManager.IM.reloadKey))
                    {
                        StartCoroutine(EndTutorial());
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet") && hintIndex == 1)
        {
            Destroy(other.gameObject);
            hintIndex = 2;
        }
        else if (other.gameObject.CompareTag("SniperBullet") && hintIndex == 5)
        {
            Destroy(other.gameObject);
            hintIndex = 6;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private IEnumerator EndTutorial()
    {
        Player.sniperIsDead = false;
        Player.riflerIsDead = false;
        Player.sicklerIsDead = false;
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(3);
    }
}
