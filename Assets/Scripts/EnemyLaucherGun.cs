using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaucherGun : MonoBehaviour
{
    [SerializeField] float distanceFromPlayer;
    [SerializeField] float attackRange;
    [SerializeField] float aimingSpeed;
    [Range(0.1f, 1f)]
    [SerializeField] float posMulitiplier = 1f;
    [SerializeField] EnemyGun enemyGun;
    [SerializeField] Transform launcherTransform;
   

    [SerializeField] bool canAttack = false;

    Transform player;


    private void Start()
    {
        enemyGun.enabled = false;
        Invoke("Delay", .1f);
    }

    void Delay()
    {
        player = LevelGameManager.instance.player.transform;
    }

    private void Update()
    {
        if (player != null)
        {
            distanceFromPlayer = Vector3.Distance(player.transform.position, launcherTransform.transform.position);


            if (distanceFromPlayer < attackRange)
            {
                Quaternion targerRotation = Quaternion.LookRotation(player.transform.position * posMulitiplier - launcherTransform.transform.position);
                launcherTransform.transform.rotation = Quaternion.Slerp(launcherTransform.transform.rotation, targerRotation, aimingSpeed);
                enemyGun.enabled = true;
            }
            else
            {
                enemyGun.enabled = false;
            }
        }
    }
}
