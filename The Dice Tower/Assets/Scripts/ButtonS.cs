using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonS : MonoBehaviour
{
    public Button buttonS;

    void Start()
    {
        buttonS = GetComponent<Button>();
    }

    public void GameStart()
    {
        Debug.Log("You have Started the game!");
        PlayerPrefs.SetInt("Lvl", 1);
        PlayerPrefs.SetInt("Str", 0);
        PlayerPrefs.SetInt("Int", 0);
        PlayerPrefs.SetInt("Dex", 0);
        SceneManager.LoadScene("Andar_1");
    }

}
