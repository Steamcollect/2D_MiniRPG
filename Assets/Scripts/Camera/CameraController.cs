using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;

    float moveTime;
    Vector3 posOffset = new Vector3(0, 0, -10);
    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + posOffset, ref velocity, moveTime);
    }

    public static void ShakeCam(float intensity, float time)
    {
        //Shake
    }
}
