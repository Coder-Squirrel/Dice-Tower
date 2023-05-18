using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager2 : MonoBehaviour
{
    public PlayerStats player;
    public GameObject hud;
    public GameObject winScreen;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        player.OnEnemyKill += FinishQuest;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FinishQuest(object sender, EventArgs args)
    {
        counter += 1;
        if (counter >= 4)
        {
            player.LevelUp();
            hud.SetActive(false);
            winScreen.SetActive(true);
        }
    }
}
