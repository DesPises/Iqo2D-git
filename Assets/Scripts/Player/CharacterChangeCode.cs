using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeCode : MonoBehaviour
{
    [SerializeField] private GameObject riflerGO;
    [SerializeField] private GameObject sniperGO;
    [SerializeField] private GameObject sicklerGO;

    private Vector3 plRCoordinates;
    private Vector3 plSCoordinates;
    private Vector3 plSiCoordinates;

    public static bool canChange;
    public static bool change;

    void Start()
    {
        // Set starting character to rifler
        Player.character = "Rifler";

        change = false;
        canChange = true;
    }

    void Update()
    {
        // Save coordinates
        plRCoordinates = riflerGO.transform.position + new Vector3(0, -2);
        plSCoordinates = sniperGO.transform.position + new Vector3(0, -2);
        plSiCoordinates = sicklerGO.transform.position + new Vector3(0, 2);

        // Characters pick

        // Pick rifler
        if (canChange && Input.GetKeyDown(InputManager.IM.torKey) && !Player.riflerIsDead)
        {
            if (Player.character == "Sniper")
            {
                riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);

            }
            if (Player.character == "Sickler")
            {
                riflerGO.transform.position = plSiCoordinates;

            }
            GameManager.Instance.SwitchToRifler();
            PickRifler();
        }

        // Pick sniper
        if (canChange && Input.GetKeyDown(InputManager.IM.tosKey) && !Player.sniperIsDead)
        {
            if (Player.character == "Rifler")
            {
                sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
            }
            if (Player.character == "Sickler")
            {
                sniperGO.transform.position = plSiCoordinates;
            }
            GameManager.Instance.SwitchToSniper();
            PickSniper();
        }

        // Pick sickler
        if (canChange && Input.GetKeyDown(InputManager.IM.tosiKey) && !Player.sicklerIsDead)
        {
            if (Player.character == "Rifler")
            {
                sicklerGO.transform.position = plRCoordinates;
            }
            if (Player.character == "Sniper")
            {
                sicklerGO.transform.position = plSCoordinates;
            }
            GameManager.Instance.SwitchToSickler();
            PickSickler();
        }

        // Rifler dies

        if (Player.character == "Rifler" && Player.riflerIsDead)
        {
            if (!Player.sicklerIsDead)
            {
                sicklerGO.transform.position = plRCoordinates;
                PickSickler();
            }
            if (Player.sicklerIsDead && !Player.sniperIsDead)
            {
                sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
                PickSniper();
            }
        }

        // Sniper dies

        if (Player.character == "Sniper" && Player.sniperIsDead)
        {
            if (!Player.sicklerIsDead)
            {
                sicklerGO.transform.position = plSCoordinates;
                PickSickler();
            }
            if (Player.sicklerIsDead && !Player.riflerIsDead)
            {
                riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);
                PickRifler();
            }
        }

        // Sickler dies

        if (Player.character == "Sickler" && Player.sicklerIsDead)
        {
            if (!Player.sniperIsDead)
            {
                riflerGO.transform.position = plSiCoordinates;
                PickSniper();
            }
            if (Player.sniperIsDead && !Player.riflerIsDead)
            {
                sniperGO.transform.position = plSiCoordinates;
                PickRifler();
            }
        }
    }

    private void PickRifler()
    {
        riflerGO.gameObject.SetActive(true);
        sniperGO.gameObject.SetActive(false);
        sicklerGO.gameObject.SetActive(false);
        Player.character = "Rifler";
        StartCoroutine(CooldownCoroutine());
    }

    private void PickSniper()
    {
        riflerGO.gameObject.SetActive(false);
        sniperGO.gameObject.SetActive(true);
        sicklerGO.gameObject.SetActive(false);
        Player.character = "Sniper";
        StartCoroutine(CooldownCoroutine());
    }

    private void PickSickler()
    {
        riflerGO.gameObject.SetActive(false);
        sniperGO.gameObject.SetActive(false);
        sicklerGO.gameObject.SetActive(true);
        Player.character = "Sickler";
        StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        yield return null;
        change = true;
        yield return null;
        change = false;
        canChange = false;
        yield return new WaitForSeconds(0.25f);
        if (!Star.isDDOn && !Star.isInfAmmoOn && !Star.isInfHPOn)
        {
            canChange = true;
        }
    }

}
