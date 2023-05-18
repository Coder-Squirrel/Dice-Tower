using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public PlayerMove playerMove;
    public GameObject interactButton, Quest, objectName;
    public bool questGiver, questObject;
    private bool questWasGiven = false;


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
    }
    private void OnTriggerEnter(Collider other)
    {
        playerMove.OnInteract += QuestGiver;
        interactButton.SetActive(true);
        objectName.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
        playerMove.OnInteract -= QuestGiver;
        interactButton.SetActive(false);
        objectName.SetActive(false);
    }


    private void QuestGiver(object sender, EventArgs args)
    {
        if(questGiver) Quest.SetActive(true);
        if(questObject && questWasGiven) OnObjectInteract?.Invoke(this, EventArgs.Empty);
    }
}
