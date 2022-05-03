using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DCon : MonoBehaviour
{

    public static bool isInVR;

    public GameObject paperball;
    public float throwForce = 5f;

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

    public OVRInput.Axis1D throwButton;

    public bool isOnGround;

    public Rigidbody2D rb;

    public bool jumped;
    public bool jumpRequested;
    public bool switched;
    public bool switchRequested;
    public bool throwed;
    public bool throwRequested;
    public float horizontalInput;

    public int lastSwitchFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (paperball)
        {
            paperball.GetComponent<Rigidbody>().isKinematic = true;
        }
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
        //The || value is used for keyboard controls
        var jumpPressed = right.Get(jumpButton) || Input.GetButton("Jump");
        jumpRequested = jumpPressed && !jumped;
        jumped = jumpPressed;
        // Debug.Log(jumpRequested);

        var switchPressed = right.Get(switchButton) || Input.GetButton("Fire2");
        switchRequested = switchPressed && !switched;
        switched = switchPressed;

        var throwPressed = right.Get(throwButton) || Input.GetButton("Fire1");
        throwRequested = throwPressed && !throwed;
        throwed = throwPressed;

        horizontalInput = left.Get(movementAxis).x;
        var keyboardHorizontalInput = Input.GetAxis("Horizontal");
        horizontalInput = (Mathf.Abs(horizontalInput) > Mathf.Abs(keyboardHorizontalInput)) ? horizontalInput : keyboardHorizontalInput;
        // Debug.Log(horizontalInput);

        CheckSwitch();
        CheckThrow();
    }

    void CheckSwitch()
    {
        if (!switchRequested) return;

        var cur = Time.frameCount;

        if (cur - lastSwitchFrame < 5) return;

        lastSwitchFrame = cur;

        SwitchState();
    }

    void CheckThrow()
    {
        if (!throwRequested || !isInVR || !paperball) return;

        // paperball.transform.parent = null;

        var t = right.transform;

        var pb = paperball.GetComponent<Paperball>();

        if (pb.thrown) pb.Reset();
        else pb.Throw(t.forward * throwForce);
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

            paperball.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            cameraRig.transform.position = sofaPos.position;
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = true;
            }

            paperball.GetComponent<Renderer>().enabled = false;
        }

        StateSwitch(isInVR);
    }

    public static void StateSwitch(bool isInVR)
    {
        
    }
}
