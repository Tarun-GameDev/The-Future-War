using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trails : MonoBehaviour
{
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] float duration;
    [SerializeField] float timeStamp;

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }


    void Update()
    {
        if(Time.time > timeStamp + duration)
        {
            duration = Random.Range(0.05f, 0.1f);
            timeStamp = Time.time;
            trailRenderer.emitting = !trailRenderer.emitting;
        }
    }
}
