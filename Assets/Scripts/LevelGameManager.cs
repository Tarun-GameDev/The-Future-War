using UnityEngine;
using TMPro;

public class LevelGameManager : MonoBehaviour
{
    public static LevelGameManager instance;
    public GameObject player;
    public PlainController playerController;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] ReverseControllers reverseConrtollers;
    public int distance_Player_Travelled;
    [SerializeField]TextMeshProUGUI enemieKillIncicatorText;
    [SerializeField] Animator textAnimator;

    public int kills = 0;
    public float timer;
    public int distanceTravelled;

    public int multiplier = 1;

    private void Awake()
    {
        kills = 0;

        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
        if(playerController == null)
            playerController = player.GetComponent<PlainController>();
        if (playerHealth == null)
            playerHealth = player.GetComponent<PlayerHealth>();
        if (reverseConrtollers == null)
            reverseConrtollers = player.GetComponent<ReverseControllers>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        distance_Player_Travelled = playerController.distance_Travelled;

        if(distance_Player_Travelled == 500 * multiplier)
        {
            multiplier++;
            if (player != null)
            {
                //playerHealth.DecreaseHealthAndFuel();
                playerController.speed += 2;
                //reverseConrtollers.ReverseTheControllers();
            }
        }
    }

    public void EnemieKilled()
    {
        kills++;
        textAnimator.SetTrigger("Killed");
        enemieKillIncicatorText.text = kills + " Kills";
    }
}
