using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DCon : MonoBehaviour
{
    public GameObject player;
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
    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
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
        Debug.Log(jumpRequested);
        horizontalInput = left.Get(movementAxis).x;
        // Debug.Log(horizontalInput);
    }

    void GroundCheck()
    {
        var hit = Physics2D.Raycast(((Vector2) transform.position) - groundCheckOffset, Vector2.down, groundCheckLength, canLandOn);

        isOnGround = hit.transform;
    }

    void SwitchState()
    {
        if (!isOnGround) return;
    }

    public static void StateSwitch()
    {

    }
}
