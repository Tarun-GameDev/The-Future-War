using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlainSPawner : MonoBehaviour
{
    [SerializeField] Transform enemyPlainPrefab;
    [SerializeField] int enemiesAmount = 1;

    private void Awake()
    {
        for (int i = 0; i < enemiesAmount; i++)
        {
            Instantiate(enemyPlainPrefab, transform.position, Quaternion.identity);
        }

    }
}
