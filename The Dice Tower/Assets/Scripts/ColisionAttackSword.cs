using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionAttackSword : MonoBehaviour
{
    public PlayerStats player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<EnemyDamage>(out EnemyDamage enemydamage))
        {
            Debug.Log(enemydamage);
            //player.DealDamage(0.5f);
            other.gameObject.SetActive(false);
            player.KillEnemy();

        }
    }
    
}
