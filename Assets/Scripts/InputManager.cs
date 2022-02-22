using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager IM;

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

    void Start()
    {

    }

    void Update()
    {

    }
}
