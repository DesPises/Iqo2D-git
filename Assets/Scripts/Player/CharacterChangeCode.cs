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
        riflerGO.SetActive(true);
        sniperGO.SetActive(false);
        sicklerGO.SetActive(false);

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
        if (Player.character != "Rifler")
        {
            if (canChange && Input.GetKeyDown(InputManager.IM.torKey) && !Player.riflerIsDead)
            {
                if (Player.character == "Sniper")
                {
                    Sniper.Instance.OnCharacterChange();
                    riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);

                }
                if (Player.character == "Sickler")
                {
                    riflerGO.transform.position = plSiCoordinates;

                }
                GameManager.Instance.SwitchToRifler();
                PickRifler();
            }
        }

        // Pick sniper
        if (Player.character != "Sniper")
        {
            if (canChange && Input.GetKeyDown(InputManager.IM.tosKey) && !Player.sniperIsDead)
            {
                if (Player.character == "Rifler")
                {
                    Rifler.Instance.OnCharacterChange();
                    sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
                }
                if (Player.character == "Sickler")
                {
                    sniperGO.transform.position = plSiCoordinates;
                }
                GameManager.Instance.SwitchToSniper();
                PickSniper();
            }
        }

        // Pick sickler
        if (Player.character != "Sickler")
        {
            if (canChange && Input.GetKeyDown(InputManager.IM.tosiKey) && !Player.sicklerIsDead)
            {
                if (Player.character == "Rifler")
                {
                    Rifler.Instance.OnCharacterChange();
                    sicklerGO.transform.position = plRCoordinates;
                }
                if (Player.character == "Sniper")
                {
                    Sniper.Instance.OnCharacterChange();
                    sicklerGO.transform.position = plSCoordinates;
                }
                GameManager.Instance.SwitchToSickler();
                PickSickler();
            }
        }
        
        // Rifler dies

        if (Player.character == "Rifler" && Player.riflerIsDead)
        {
            if (!Player.sicklerIsDead)
            {
                sicklerGO.transform.position = plRCoordinates;
                PickSickler();
                GameManager.Instance.SwitchToSickler();
            }
            if (Player.sicklerIsDead && !Player.sniperIsDead)
            {
                sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
                PickSniper();
                GameManager.Instance.SwitchToSniper();
            }
        }

        // Sniper dies

        if (Player.character == "Sniper" && Player.sniperIsDead)
        {
            if (!Player.sicklerIsDead)
            {
                sicklerGO.transform.position = plSCoordinates;
                PickSickler();
                GameManager.Instance.SwitchToSickler();
            }
            if (Player.sicklerIsDead && !Player.riflerIsDead)
            {
                riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);
                PickRifler();
                GameManager.Instance.SwitchToRifler();
            }
        }

        // Sickler dies

        if (Player.character == "Sickler" && Player.sicklerIsDead)
        {
            if (!Player.sniperIsDead)
            {
                riflerGO.transform.position = plSiCoordinates;
                PickSniper();
                GameManager.Instance.SwitchToSniper();
            }
            if (Player.sniperIsDead && !Player.riflerIsDead)
            {
                sniperGO.transform.position = plSiCoordinates;
                PickRifler();
                GameManager.Instance.SwitchToRifler();
            }
        }
    }

    private void PickRifler()
    {
        Rifler.Instance.reloading = false;
        riflerGO.SetActive(true);
        sniperGO.SetActive(false);
        sicklerGO.SetActive(false);
        Player.character = "Rifler";
        StartCoroutine(CooldownCoroutine());
    }

    private void PickSniper()
    {
        Sniper.Instance.reloading = false;
        riflerGO.SetActive(false);
        sniperGO.SetActive(true);
        sicklerGO.SetActive(false);
        Player.character = "Sniper";
        StartCoroutine(CooldownCoroutine());
    }

    private void PickSickler()
    {
        riflerGO.SetActive(false);
        sniperGO.SetActive(false);
        sicklerGO.SetActive(true);
        Player.character = "Sickler";
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return null;
        change = true;
        yield return null;
        change = false;
        canChange = false;
        yield return new WaitForSeconds(0.25f);
        if (Player.character == "Rifler")
        {
            if (!Rifler.Instance.isBonusActive)
            {
                canChange = true;
            }
        }
        else if (Player.character == "Sniper")
        {
            if (!Sniper.Instance.isBonusActive)
            {
                canChange = true;
            }
        }
        else if (Player.character == "Sickler")
        {
            if (!Sickler.Instance.isBonusActive)
            {
                canChange = true;
            }
        }
    }

}
