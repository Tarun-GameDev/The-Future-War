using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [InspectorName("Defaults")]
    [SerializeField] float Player_Distance_Spawn_Ground_Part = 300f;

    [SerializeField] Transform levelGround_Start;
    [SerializeField] List<Transform> groundPartLists;
    [SerializeField] Transform player;
    [SerializeField] int startingSpawnGroundParts = 2;

    [InspectorName("Advance")]
    [SerializeField] Vector3 minPosMultiplier;
    [SerializeField] Vector3 maxPosMultiplier;

    Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = levelGround_Start.Find("EndPosition").position;

        for (int i = 0; i < startingSpawnGroundParts; i++)
        {
            spawnGroundPart();
        }
    }

    private void Update()
    {
        if(Vector3.Distance(player.position,lastEndPosition) < Player_Distance_Spawn_Ground_Part)
        {
            spawnGroundPart();
        }
    }

    private void spawnGroundPart()
    {
        Transform chosenGroundPart = groundPartLists[Random.Range(0, groundPartLists.Count)];
        Vector3 randomPosMultiplier = new Vector3(Random.Range(minPosMultiplier.x, maxPosMultiplier.x), Random.Range(minPosMultiplier.y, maxPosMultiplier.y), Random.Range(minPosMultiplier.z, maxPosMultiplier.z));
        Transform lastGroundTransform =  spawnGround(chosenGroundPart,lastEndPosition + randomPosMultiplier);
        lastEndPosition = lastGroundTransform.Find("EndPosition").position;
    }

    Transform spawnGround(Transform groundPart, Vector3 spawnPos)
    {
        Transform levelPartTransform = Instantiate(groundPart, spawnPos, Quaternion.identity);
        return levelPartTransform;
    }
}
