using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingRocket : MonoBehaviour
{
    [Header("Missile Things:")]
    [SerializeField] float cameraShakeCheckRadius;
    [SerializeField] float aimingSpped;
    [SerializeField] float speed;
  

    [Header("Bullet Status:")]
    [SerializeField] int Damage;
    [Range(.1f, 5f)]
    [SerializeField] float maxAliveTime = 1.5f;
    [SerializeField] bool DamageEnemy = false, DamagePlayer = true;
    ObjectPoller objectPoller;
    Transform player;

    private void Start()
    {
        objectPoller = ObjectPoller.Instance;
        player = LevelGameManager.instance.player.transform;
    }

    private void OnEnable()
    {
        StartCoroutine("Suicide");
    }

    private void Update()
    {
        if(player != null)
        {
            Quaternion targerRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targerRotation, aimingSpped);
        }

        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {

        if (DamagePlayer && other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerHealth>().TakeDamage(Damage);
            Dead();
        }
        if (DamageEnemy && other.transform.CompareTag("Enemy"))
        {
            other.transform.GetComponent<EnemyHealth>().TakeDamage(Damage);
            Dead();
        }

        Dead();
    }


    IEnumerator Suicide()
    {
        yield return new WaitForSeconds(maxAliveTime);
        camerashake();
        var explosion = objectPoller.SpawnFromPool("Explosion", transform.position, Quaternion.identity);
        Destroy(gameObject, .01f);

        AudioManager.instance.Play("MissileExplosion");
    }

    void Dead()
    {
        camerashake();
        var explosion = objectPoller.SpawnFromPool("Explosion", transform.position, Quaternion.identity);
        Destroy(gameObject, .01f);

        AudioManager.instance.Play("MissileExplosion");
    }

    void camerashake()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, cameraShakeCheckRadius);

        foreach (Collider nearbyObj in collider)
        {
            if (nearbyObj.CompareTag("Player"))
            {
                GameManager.instance.Camerashake();
            }
        }
    }
}
