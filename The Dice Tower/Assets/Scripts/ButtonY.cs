using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonY : MonoBehaviour
{
    public NextLevel next;
    public PlayerStats stats;
    public Button buttonY;
    public PlayerMove move;

    void Start()
    {
        buttonY = GetComponent<Button>();
        move = GetComponent<PlayerMove>();
    }
    
    public void Restart()
    {
        next.numSala = 1;
        next.win = false;
        PlayerPrefs.SetInt("Lvl", 1);
        PlayerPrefs.SetInt("Str", 0);
        PlayerPrefs.SetInt("Int", 0);
        PlayerPrefs.SetInt("Dex", 0);
        SceneManager.LoadScene("Andar_1");
    }

}
