using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public PlayerMove playerMove;
    public GameObject interactButton, Quest, objectName,rewardOne,rewardTwo,musicOff;
    public bool questGiver, questObject, hasReward;
    private bool questWasGiven = false;
    public GameObject questButton;



    public event EventHandler OnObjectInteract;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcceptQuest()
    {
        questWasGiven = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        questButton.SetActive(true);

    }
    private void OnTriggerEnter(Collider other)
    {
        playerMove.OnInteract += QuestGiver;
        interactButton.SetActive(true);
        if (objectName != null)
        {
            objectName.SetActive(true);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        playerMove.OnInteract -= QuestGiver;
        interactButton.SetActive(false);
        if (objectName != null)
        {
            objectName.SetActive(false);
        }
    }


    private void QuestGiver(object sender, EventArgs args)
    {
        if (questGiver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Quest.SetActive(true);
        }

        if (questObject && questWasGiven)
        {
            musicOff.SetActive(false);
            OnObjectInteract?.Invoke(this, EventArgs.Empty);
        }

    }
}
