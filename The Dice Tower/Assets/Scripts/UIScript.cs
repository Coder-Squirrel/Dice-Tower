using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public Slider healthbar;

    public Image gameOver, victory;
    public TMP_Text gameOverText, nextLevelText, winText;

    public PlayerStats stats;

    public NextLevel next;
    

    public GameObject buttonS, buttonI, buttonD, buttonY, buttonN,restartB,quitB;

    void Start()
    {
        gameOver.enabled = false;
        gameOverText.enabled = false;
        buttonS.SetActive(false);
        buttonI.SetActive(false);
        buttonD.SetActive(false);
        nextLevelText.enabled = false;
        buttonY.SetActive(false);
        buttonN.SetActive(false);
        winText.enabled = false;
        victory.enabled = false;
        restartB.SetActive(false);
        quitB.SetActive(false);

    }

    void Update()
    {
        StartCoroutine(textForNext());
        if (next.win)
        {
            buttonY.SetActive(true);
            buttonN.SetActive(true);
            winText.enabled = true;
            victory.enabled = true;
        }
        if (next.numSala == 4)
        {
            nextLevelText.enabled = false;
        }
        if(stats.Str >= 2)
        {
            healthbar.maxValue = 150; 
        }
    }
    
    public IEnumerator textForNext()
    {
        yield return new WaitForSeconds(1f);
        if(next.enemyCount <= 0)
        {
            nextLevelText.enabled = true;
        }
    }


    public void SetMaxHealth(float health)
    {
        healthbar.maxValue = health;
        healthbar.value = health;
    }

    public void UpdateHealthBar(float health)
    {
        healthbar.value = health;
        PlayerPrefs.SetFloat("Life", health);
    }
}
