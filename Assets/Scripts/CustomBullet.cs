using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    [Header("Missile Things:")]
    [SerializeField] bool missile = false;
    [SerializeField] float cameraShakeCheckRadius;

    [Header("Bullet Status:")]
    [SerializeField] int Damage;
    [Range(.1f,5f)]
    [SerializeField] float maxAliveTime = 1.5f;
    [SerializeField] bool DamageEnemy = false, DamagePlayer = true;
    ObjectPoller objectPoller;

    private void Start()
    {
        objectPoller = ObjectPoller.Instance;
    }

    private void OnEnable()
    {
        StartCoroutine("Suicide");
    }

    private void OnCollisionEnter(Collision other)
    {

        if (DamagePlayer && other.transform.CompareTag("Player") )
        {
            other.transform.GetComponent<PlayerHealth>().TakeDamage(Damage);
            Dead();
        }
        if (DamageEnemy && other.transform.CompareTag("Enemy"))
        {
            other.transform.GetComponent<EnemyHealth>().TakeDamage(Damage);
            Dead();
        }
        if(DamageEnemy && missile && other.transform.CompareTag("Iland"))
        {
            other.transform.GetComponent<IlandHealth>().TakeDamage(Damage);
        }

        Dead();
    }
    
    
    IEnumerator Suicide()
    {
        yield return new WaitForSeconds(maxAliveTime);
        if (missile)
        {
            camerashake();
            var explosion = objectPoller.SpawnFromPool("Explosion", transform.position, Quaternion.identity);

            AudioManager.instance.Play("MissileExplosion");
        }
        Destroy(gameObject, .01f);
    }

    void Dead()
    {
        if(missile)
        {
            camerashake();
            var explosion = objectPoller.SpawnFromPool("Explosion", transform.position, Quaternion.identity);

            AudioManager.instance.Play("MissileExplosion");
        }
        Destroy(gameObject, .01f);
    }

    void camerashake()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, cameraShakeCheckRadius);

        foreach (Collider nearbyObj in collider)
        {
            if(nearbyObj.CompareTag("Player"))
            {
                GameManager.instance.Camerashake();
            }
        }
    }
}
