using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float cameraOffset;
    Vector3 cameraDefaultPosition;

    private void Awake()
    {
        cameraDefaultPosition = transform.position;
    }

    private void Update()
    {
        Vector3 newPos = cameraDefaultPosition;
        newPos.z = player.position.z + cameraOffset;

        transform.position = newPos;
    }
}
