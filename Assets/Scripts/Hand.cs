using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public OVRInput.Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 Get(OVRInput.Axis2D axis)
    {
        return OVRInput.Get(axis, controller);
    }

    public bool Get(OVRInput.Button button)
    {
        return OVRInput.Get(button, controller);
    }

    public bool GetDown(OVRInput.Button button)
    {
        return OVRInput.GetDown(button, controller);
    }

    public bool GetUp(OVRInput.Button button)
    {
        return OVRInput.GetUp(button, controller);
    }
}
