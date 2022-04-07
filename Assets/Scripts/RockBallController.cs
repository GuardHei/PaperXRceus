using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RockBallController : MonoBehaviour
{
    public Vector3 target;
    public Rigidbody ball;
    public float speed;
    public float maxDuration = 5.0f;
    public LayerMask canBeBlockedBy;
    // Start is called before the first frame update
    void Start()
    {
        if (!ball) ball = GetComponent<Rigidbody>();
        Destroy(gameObject, maxDuration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(target);
        ball.MovePosition(Vector3.MoveTowards(ball.position, target, speed * Time.fixedDeltaTime));
    }

    void OnCollisionEnter(Collision collision)
    {
        CheckDestroy(collision.collider);
    }

    void CheckDestroy(Collider other)
    {
        int layer = 1 << other.gameObject.layer;
        if ((canBeBlockedBy.value & layer) != 0)
        {
            Destroy(gameObject, .1f);
        }
    }
}
