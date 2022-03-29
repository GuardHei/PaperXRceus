using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotator : MonoBehaviour
{
    public float speed;
    public Vector3 offset;
    public Vector3 axis;

    private void Update()
    {
        transform.RotateAround(transform.position + offset, axis, speed * Time.deltaTime);
    }
}
