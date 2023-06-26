using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager1 : MonoBehaviour
{
    public NPCHandler barril;
    public PlayerStats player;
    public GameObject hud,rewardOne,rewardTwo;
    public GameObject quest2;
    public GameObject questButton;

    private bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        barril.OnObjectInteract += FinishQuest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void FinishQuest(object sender, EventArgs args)
    {
        if(!finished)
        {
            player.LevelUp();
            hud.SetActive(false);
            finished = true;
            quest2.SetActive(true);
            rewardOne.SetActive(true);
            rewardTwo.SetActive(true);
        }
    }
}
