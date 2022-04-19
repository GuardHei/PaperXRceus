using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUpdater : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
    }

    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();
    }
}
