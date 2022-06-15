using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    public Vector2 paralaxMultiplier;
    private Transform cameraTransform;
    private Vector3 lastcameraPosition;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastcameraPosition = cameraTransform.position;
    }
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastcameraPosition;
        transform.position += new Vector3(deltaMovement.x * paralaxMultiplier.x, deltaMovement.y * paralaxMultiplier.y);
        lastcameraPosition = cameraTransform.position;
    }
}
