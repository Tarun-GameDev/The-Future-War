using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform plainTransform;

    void Update()
    {

        float yPos = Mathf.Clamp(plainTransform.position.y, 10f, 500f);

        gameObject.transform.position = new Vector3(plainTransform.position.x, yPos, transform.position.z);
    }
}
