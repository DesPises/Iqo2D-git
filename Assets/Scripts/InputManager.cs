using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager IM { get; private set; }

    // Variables to bind keys
    private bool waitingForKey;
    private Event keyEvent;
    private KeyCode newKey;

    public KeyCode jumpKey { get; set; }
    public KeyCode attackKey { get; set; }
    public KeyCode fwKey { get; set; }
    public KeyCode bwKey { get; set; }
    public KeyCode crouchKey { get; set; }
    public KeyCode torKey { get; set; }
    public KeyCode tosKey { get; set; }
    public KeyCode tosiKey { get; set; }
    public KeyCode reloadKey { get; set; }

    void Awake()
    {
        IM = this;

        // Set player control keys to saved values (or to default if no data saved)
        jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        attackKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attackKey", "L"));
        fwKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fwKey", "D"));
        bwKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("bwKey", "A"));
        crouchKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crouchKey", "S"));
        reloadKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("reloadKey", "R"));
        torKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("torKey", "Alpha2"));
        tosKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("tosKey", "Alpha1"));
        tosiKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("tosiKey", "Alpha3"));
    }

    // Read key to bind it to player control

    private void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    // Public method to start assigning when player pressed key
    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    private IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    private IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
            case "fwKey":
                fwKey = newKey;
                PlayerPrefs.SetString("fwKey", fwKey.ToString());
                break;

            case "bwKey":
                bwKey = newKey;
                PlayerPrefs.SetString("bwKey", bwKey.ToString());
                break;

            case "jumpKey":
                jumpKey = newKey;
                PlayerPrefs.SetString("jumpKey", jumpKey.ToString());
                break;

            case "attackKey":
                attackKey = newKey;
                PlayerPrefs.SetString("attackKey", attackKey.ToString());
                break;

            case "crouchKey":
                crouchKey = newKey;
                PlayerPrefs.SetString("crouchKey", crouchKey.ToString());
                break;

            case "reloadKey":
                reloadKey = newKey;
                PlayerPrefs.SetString("reloadKey", reloadKey.ToString());
                break;

            case "torKey":
                torKey = newKey;
                PlayerPrefs.SetString("torKey", torKey.ToString());
                break;

            case "tosKey":
                tosKey = newKey;
                PlayerPrefs.SetString("tosKey", tosKey.ToString());
                break;

            case "tosiKey":
                tosiKey = newKey;
                PlayerPrefs.SetString("tosiKey", tosiKey.ToString());
                break;
        }

        yield return null;
    }
}
