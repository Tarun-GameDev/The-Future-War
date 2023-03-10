using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlandHealth : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] GameObject FracturedPlain;

    private void Start()
    {
        currentHealth = maxHealth;
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
        gameObject.SetActive(false);
        var fracturedObj = Instantiate(FracturedPlain, transform.position, transform.rotation);
        Destroy(fracturedObj, 5f);
        Destroy(gameObject, 5f);
    }
}
