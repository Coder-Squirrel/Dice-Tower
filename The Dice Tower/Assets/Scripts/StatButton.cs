using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StatButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    public UIScript ui;
    public PlayerStats stats;
    public NextLevel next;

    public Scene scene;

    public int sStr,sDex,sInt;

    void Start()
    {

    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        AssingStat();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public void AssingStat()
    {
        if(this.gameObject == ui.buttonS.gameObject)
        {
            stats.Str++;
            PlayerPrefs.SetInt("Str", stats.Str);
            ui.buttonS.SetActive(false);
            ui.buttonI.SetActive(false);
            ui.buttonD.SetActive(false);
        }
        else if(this.gameObject == ui.buttonI.gameObject)
        {
            stats.Int++;
            PlayerPrefs.SetInt("Int", stats.Int);
            ui.buttonS.SetActive(false);
            ui.buttonI.SetActive(false);
            ui.buttonD.SetActive(false);
        }
        else
        {
            stats.Dex++;
            PlayerPrefs.SetInt("Dex", stats.Dex);
            ui.buttonS.SetActive(false);
            ui.buttonI.SetActive(false);
            ui.buttonD.SetActive(false);
        }
    }
}
