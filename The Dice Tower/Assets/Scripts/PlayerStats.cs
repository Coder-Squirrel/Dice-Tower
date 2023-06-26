using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float life = 100f;
    public float currentLife;

    public float maxExp = 100f;
    public float currentExp;

    public StatButton buttonS, buttonI, buttonD;
    public UIScript ui;
    public GameObject levelUp;
    public EnemyDamage enemyStats;
    public PlayerMove playerMove;
    public NextLevel next;

    public int lvl = 1;
    public int Str = 0;
    public int Dex = 0;
    public int Int = 0;

    public event EventHandler OnEnemyKill;


    void Start()
    {
        currentLife = life;
        ui.SetMaxHealth(life);

        currentExp = 0;
        lvl = 1;

        playerMove.OnEnable();
    }

    void Update()
    {
        ui.UpdateHealthBar(currentLife);
        if(Dex >= 2)
        {
            playerMove.moveSpeed = 20f;
        }
        if(Int >= 2)
        {
            playerMove.animationFinishTime = 0.01f;
        }
    }

    //Detecção de colisão passou pra HitDetection
    /*private void OnCollisionEnter(Collision collision)
    {
        enemyStats = collision.gameObject.GetComponent<EnemyDamage>();
        if(collision.gameObject.tag == "Enemy" && playerMove.isAttacking)
        {
            DealDamage(5f);
        }
    }*/
    public void KillEnemy()
    {
        OnEnemyKill?.Invoke(this, EventArgs.Empty);
    }
    public void DealDamage(float damage)
    {
        if (enemyStats != null)
        {
            if(Str >= 2 || Int >= 2 || Dex >= 2)
            {
                damage += 5;
            }
            enemyStats.enemyHealth -= damage;
            if (enemyStats.enemyHealth <= 0)
            {
                next.enemyCount--;
                GainExp();
                Destroy(enemyStats.gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;

        ui.UpdateHealthBar(currentLife);

        if(currentLife <= 0)
        {
            playerMove.OnDisable();
            ui.gameOver.enabled = true;
            ui.gameOverText.enabled = true;
            ui.restartB.SetActive(true);
            ui.quitB.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            playerMove.OnEnable();
        }
    }

    public void GainExp()
    {
        currentExp += 25f;
        if (currentExp == maxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {

        lvl++;
        levelUp.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        currentExp = 0;
    }
}
