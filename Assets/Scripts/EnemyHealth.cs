using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] GameObject FracturedPlain;

    [SerializeField] bool fillFuel = false;
    [SerializeField] bool enemyDead;

    GameObject player;

    private void Start()
    {
        Invoke("DetectPlayer", .1f);

        currentHealth = maxHealth;
        enemyDead = false;
    }

    void DetectPlayer()
    {
        player = LevelGameManager.instance.player;
    }

    public void TakeDamage(int damage)
    {
        
        currentHealth -= damage;

        //if enemy dead
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        LevelGameManager.instance.EnemieKilled();

        if (player != null && !fillFuel)
            player.GetComponent<PlayerHealth>().HealHealth();

        if (player != null && fillFuel)
            player.GetComponent<PlayerHealth>().FillFuel();

        enemyDead = true;
        gameObject.SetActive(false);
        var fracturedObj = Instantiate(FracturedPlain, transform.position, transform.rotation);
        Destroy(fracturedObj, 5f);
        Destroy(gameObject, 5f);
    }
}
