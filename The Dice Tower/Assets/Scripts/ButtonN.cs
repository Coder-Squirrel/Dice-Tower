using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonN : MonoBehaviour
{
    public Button buttonN;

    void Start()
    {
        buttonN = GetComponent<Button>();
    }

    public void Close()
    {
        Debug.Log("You have Exited the game!");
        PlayerPrefs.SetInt("Lvl", 1);
        PlayerPrefs.SetInt("Str", 0);
        PlayerPrefs.SetInt("Int", 0);
        PlayerPrefs.SetInt("Dex", 0);
        Application.Quit();
    }

}
