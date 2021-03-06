using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RockBallController : MonoBehaviour
{
    public Vector3 dir;
    public Rigidbody ball;
    public float speed;
    public float maxDuration = 5.0f;
    public LayerMask canBeBlockedBy;
    public bool destroyFlag;
    // Start is called before the first frame update
    void Start()
    {
        if (!ball) ball = GetComponent<Rigidbody>();
        Destroy(gameObject, maxDuration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.LookAt(transform.position + dir);
        ball.MovePosition(ball.position + speed * Time.fixedDeltaTime * dir);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        CheckDestroy(other);
    }

    void OnCollisionEnter(Collision collision)
    {
        CheckDestroy(collision.collider);
    }

    void CheckDestroy(Collider other)
    {
        if (destroyFlag) return;
        int layer = 1 << other.gameObject.layer;
        if ((canBeBlockedBy.value & layer) != 0)
        {
            destroyFlag = true;
            Destroy(gameObject, .1f);
        }
    }
}
