﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DCon : MonoBehaviour
{

    public static bool isInVR;

    public GameObject cameraRig;
    public Transform sofaPos;
    public float speed;
    public float jumpPower;
    public float gravity;

    public Vector2 groundCheckOffset;
    public float groundCheckLength;
    public LayerMask canLandOn;
    public float deadZone = .005f;

    public Hand left;
    public Hand right;

    public OVRInput.Axis2D movementAxis;

    public OVRInput.Button jumpButton;
    public OVRInput.Button switchButton;

    public OVRInput.Controller lHandCon;
    public OVRInput.Controller rHandCon;

    public bool isOnGround;

    public Rigidbody2D rb;

    public bool jumped;
    public bool jumpRequested;
    public bool switched;
    public bool switchRequested;
    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {

        if (isInVR) return;

        GroundCheck();

        var finalVel = new Vector2(.0f, .0f);
        if (Mathf.Abs(horizontalInput) <= deadZone) finalVel.x = .0f;
        else finalVel.x = speed * Mathf.Sign(horizontalInput);

        if (isOnGround)
        {
            if (jumpRequested) {
                finalVel.y = jumpPower;
                jumpRequested = false;
            } else finalVel.y = .0f;
        } else {
            finalVel.y -= gravity;
        }
        
        rb.velocity = finalVel;
    }

    // Update is called once per frame
    void Update()
    {
        var jumpPressed = right.Get(jumpButton);
        jumpRequested = jumpPressed && !jumped;
        jumped = jumpPressed;
        // Debug.Log(jumpRequested);

        var switchPressed = right.Get(switchButton);
        switchRequested = switchPressed && !switched;
        switched = switchPressed;

        horizontalInput = left.Get(movementAxis).x;
        // Debug.Log(horizontalInput);

        CheckSwitch();
    }

    void CheckSwitch()
    {
        if (switchRequested) SwitchState();
    }

    void GroundCheck()
    {
        var hit = Physics2D.Raycast(((Vector2) transform.position) - groundCheckOffset, Vector2.down, groundCheckLength, canLandOn);

        isOnGround = hit.transform;
    }

    void SwitchState()
    {
        if (!isOnGround) return;

        rb.velocity = Vector3.zero;

        isInVR = !isInVR;
        if (isInVR)
        {
            cameraRig.transform.position = transform.position;
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
        else
        {
            cameraRig.transform.position = sofaPos.position;
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = true;
            }
        }

        StateSwitch();
    }

    public static void StateSwitch()
    {
        
    }
}
