using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public int enemyCount;
    public Renderer door;
    public Material closed;
    public Material open;

    public int numSala = 1;

    public UIScript ui;
    public PlayerMove move;
    public StatButton sStr, sInt, sDex;
    public PlayerStats stats;

    public bool inDoorRange = false;
    public bool win = false;


    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<UIScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        stats.lvl = PlayerPrefs.GetInt("Lvl");
        stats.Str = PlayerPrefs.GetInt("Str");
        stats.Int = PlayerPrefs.GetInt("Int");
        stats.Dex = PlayerPrefs.GetInt("Dex");
        stats.currentLife = PlayerPrefs.GetFloat("Life");
        KillAll();
        if(enemyCount > 0)
        {
            door.material = closed;
        }
        else if (enemyCount <= 0)
        {
            door.material = open;
        }
        if (scene.name == "Andar_4")
        {
            win = true;
            move.OnDisable();
        }

        if (inDoorRange && enemyCount <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                numSala++;
                inDoorRange = false;
                SceneManager.LoadScene("Andar_" + numSala);
            }
        }
    }

    public void KillAll()
    {
        if(enemyCount == 0)
        {
            foreach (GameObject remainingEnemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemyCount += 1;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        inDoorRange = true;
    }

}
