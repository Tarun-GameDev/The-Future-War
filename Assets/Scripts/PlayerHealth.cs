using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int currentPlayerHealth;
    [SerializeField] int maxPlayerHealth;
    [SerializeField] float currentFuel;
    public float maxFuel = 30f;

    [SerializeField] PlainController playerContr;
    [SerializeField] GameObject plainFracturedObj;
    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] PlayerHealthBar healthBar;
    [SerializeField] PlayerHealthBar fuelBar;

    [SerializeField] PlayerScriptableObj player;
    [SerializeField] LevelGameManager levelGameManager;
    [SerializeField] Timer timer;

    [InspectorName("Menus")]
    [SerializeField] TextMeshProUGUI killsText;
    [SerializeField] TextMeshProUGUI distanceTravelledText;
    [SerializeField] GameObject gamemenu;
    [SerializeField] GameObject deadmenu;

    int i = 0;

    AudioManager audioManager;

    void Start()
    {
        if (audioManager == null)
        {
            audioManager = AudioManager.instance;
        }

        gamemenu.SetActive(true);
        deadmenu.SetActive(false);

        currentFuel = maxFuel;
        fuelBar.MaxHealth(maxFuel);
        healthBar.MaxHealth(player.maxHealth);

        player.currentHealth = player.maxHealth;
        player.playerdead = false;

        HealthUI();

    }


    void Update()
    {
        if(!player.playerdead)
        {
            currentFuel -= Time.deltaTime;

            if(fuelBar != null)
                fuelBar.SetHealth(currentFuel);
        }

        float beforeHealth;

        if (player.currentHealth <= 0)
        {
            player.playerdead = true;

            Dead();
        }

        if(currentFuel <= 0)
        {
            player.playerdead = true;

            Dead();
        }

        beforeHealth = player.currentHealth;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Iland"))
        {
            TakeDamage(Mathf.FloorToInt(player.maxHealth));
        }
    }

    void HealthUI()
    {
        currentPlayerHealth = Mathf.FloorToInt(player.currentHealth);
        maxPlayerHealth = Mathf.FloorToInt(player.maxHealth);

        
        if (healthText != null)
            healthText.text = "HP:" + currentPlayerHealth + "/" + maxPlayerHealth;

        if (healthBar != null)
            healthBar.SetHealth(player.currentHealth);
    }

    public void TakeDamage(int damage)
    {
        player.currentHealth -= damage;

        HealthUI();

    }

    public void HealHealth()
    {
        if (player.currentHealth >= ((player.maxHealth / 4) * 3))
        {
            player.currentHealth = player.maxHealth;
        }
        else
        {
            player.currentHealth += (player.maxHealth / 4);
            if (audioManager != null)
            {
                audioManager.Play("Heal");
            }
        }

        HealthUI();
    }

    public void FillFuel()
    {
        currentFuel = maxFuel;
    }

    /*
    public void DecreaseHealthAndFuel()
    {
        if(maxFuel >= 10f)
        {
            maxFuel -= 2f;
        }
        
        if(player.maxHealth >= 200f)
        {
            player.maxHealth -= 20f;
        }
    }*/

    void Dead()
    {
        if(timer != null)
        {
            timer.Finish();
        }

        if (healthText != null)
            healthText.text = "HP:0/" + maxPlayerHealth;

        gameObject.SetActive(false);
        Instantiate(plainFracturedObj, transform.position, transform.rotation);

        if (audioManager != null)
            audioManager.Play("Dead");

        if (killsText != null)
            killsText.text = " Kills:" + levelGameManager.kills;
        if (distanceTravelledText != null)
            distanceTravelledText.text = "Distance Travelled:" + playerContr.distance_Travelled + "m";
        Invoke("DelayDeadMenu", 2f);

    }

    void DelayDeadMenu()
    {
        //active Dead Menu
        Cursor.lockState = CursorLockMode.Confined;
        gamemenu.SetActive(false);
        deadmenu.SetActive(true);
    }
}
