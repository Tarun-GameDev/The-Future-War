using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Enemies_Spawner : MonoBehaviour
{
    [InspectorName("Defaults")]
    [SerializeField] float Player_Distance_Spawn_Ground_Part = 300f;

    [SerializeField] Transform levelGround_Start;
    [SerializeField] List<Transform> enemiesTypes;
    [SerializeField] Transform player;
    [SerializeField] int startingSpawnGroundParts = 2;

    [InspectorName("Advance")]
    [SerializeField] Vector3 minPosMultiplier;
    [SerializeField] Vector3 maxPosMultiplier;
    [SerializeField] Vector3 distanceDecreasePos;
    [SerializeField] Vector3 afterdistanceMultiplied;
    [SerializeField] Vector3 leastDecreasePos;

    [SerializeField] LevelGameManager levelGameManager;
    [SerializeField] int multiplier;

    Vector3 lastEndPosition;
    Transform chosenGroundPart;

    private void Awake()
    {
        if (levelGameManager == null)
            levelGameManager = LevelGameManager.instance;

        lastEndPosition = new Vector3(levelGround_Start.Find("EndPosition").position.x, transform.position.y, transform.position.z);

        for (int i = 0; i < startingSpawnGroundParts; i++)
        {
            spawnGroundPart();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, lastEndPosition) < Player_Distance_Spawn_Ground_Part)
        {
            spawnGroundPart();
        }
    }

    private void spawnGroundPart()
    {


        if (afterdistanceMultiplied.x < leastDecreasePos.x)
        {
            multiplier = levelGameManager.multiplier;
            //for flying Obj decrease main spawner y pos to 
            transform.position = new Vector3(transform.position.x, transform.position.y - (distanceDecreasePos.y * multiplier), transform.position.z);
        }
        afterdistanceMultiplied = distanceDecreasePos * multiplier;

        if (multiplier == 1)
        { 
            chosenGroundPart = enemiesTypes[Random.Range(0, enemiesTypes.Count - 1)];
        }
        else if (multiplier == 2)
        {
            chosenGroundPart = enemiesTypes[Random.Range(0, enemiesTypes.Count - 1)];
        }
        else if(multiplier > 2)
        {
            chosenGroundPart = enemiesTypes[Random.Range(0, enemiesTypes.Count)];
        }

        Vector3 randomPosMultiplier = new Vector3(Random.Range(minPosMultiplier.x - afterdistanceMultiplied.x, maxPosMultiplier.x - afterdistanceMultiplied.x), Random.Range(minPosMultiplier.y - afterdistanceMultiplied.y, maxPosMultiplier.y - afterdistanceMultiplied.y), Random.Range(minPosMultiplier.z - afterdistanceMultiplied.z, maxPosMultiplier.z - afterdistanceMultiplied.z));
        Transform lastGroundTransform = spawnGround(chosenGroundPart, lastEndPosition + randomPosMultiplier);
        lastEndPosition = new Vector3(lastGroundTransform.Find("EndPosition").position.x, transform.position.y, transform.position.z);
    }

    Transform spawnGround(Transform groundPart, Vector3 spawnPos)
    {
        Transform levelPartTransform = Instantiate(groundPart, spawnPos, Quaternion.identity);
        return levelPartTransform;
    }
}
