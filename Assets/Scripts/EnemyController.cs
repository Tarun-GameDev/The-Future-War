using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] float speed;
    [SerializeField] float rotaionControl;
    [SerializeField] float attackRange;
    [SerializeField] float backAttackRange;
    [SerializeField] float frontAttackRange;


    [Header("No Need To Assign")]
    [SerializeField] GameObject player;
    [SerializeField] bool preChase = false;
    [SerializeField] bool canFrontAttack, canBackAttack, canChase;

    [SerializeField] EnemyGun[] guns;
    [SerializeField] float distanceToPlayer;

    bool preFrontAttackCheck;

    private void Start()
    {

        float randomSpeed = Random.Range(4f, -4f);
        speed = LevelGameManager.instance.playerController.speed + randomSpeed;

        Invoke("DetectPlayer",.1f);
        guns = GetComponentsInChildren<EnemyGun>();
        foreach (var enemyGun in guns)
        {
            enemyGun.enabled = false;
        }
    }

    void DetectPlayer()
    {
        player = LevelGameManager.instance.player;
    }

    void Update()
    {
        if(player != null)
        {
            distanceToPlayer = player.transform.position.x - transform.position.x;
            if (distanceToPlayer > backAttackRange)
            {
                canBackAttack = false;
                canChase = true;
                if (distanceToPlayer < 100f)
                {
                    canFrontAttack = true;
                }
                else
                {
                    canFrontAttack = false;
                }
            }
            else if (distanceToPlayer > frontAttackRange && distanceToPlayer < -10f)
            {
                canBackAttack = true;
                canFrontAttack = false;
                canChase = true;
            }
            else if (distanceToPlayer < -30f)
            {
                canFrontAttack = false;
                canBackAttack = false;
                canChase = true;
            }
            else
            {
                canFrontAttack = false;
                canBackAttack = false;
                canChase = false;
            }

            if (preFrontAttackCheck != canFrontAttack)
            {
                foreach (var enemyGun in guns)
                {
                    enemyGun.enabled = canFrontAttack;
                }
            }

            preFrontAttackCheck = canFrontAttack;

        }

    }

    private void FixedUpdate()
    {
        if(player != null)
        {
            if (canChase)
            {
                Quaternion targerRotation = Quaternion.LookRotation(player.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targerRotation, 1 * Time.fixedDeltaTime);
            }
            else
            {

                Vector3 awayPos = new Vector3(player.transform.position.x * 600f, player.transform.position.y, player.transform.position.z);
                Quaternion targerRotation = Quaternion.LookRotation(awayPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targerRotation, 1 * Time.fixedDeltaTime);
            }
        }

        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
}
