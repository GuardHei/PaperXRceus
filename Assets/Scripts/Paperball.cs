using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Paperball : MonoBehaviour
{
    public bool thrown;
    public bool spawned;
    public float spawnOffset;
    public GameObject prototype;
    public GameObject papermon;
    public Rigidbody rb;

    public void Throw(Vector3 force)
    {
        if (thrown) return;

        GetComponent<ParentConstraint>().constraintActive = false;

        thrown = true;
        rb.isKinematic = false;
        GetComponent<Collider>().enabled = true;
        rb.AddForce(force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Spawn(collision.contacts[0].point);
        // gameObject.SetActive(false);
    }

    public void Spawn(Vector3 pos)
    {
        if (!prototype || spawned) return;

        spawned = true;

        pos.y += spawnOffset;

        papermon = Instantiate(prototype, pos, Quaternion.identity);
    }

    public void Reset()
    {
        if (papermon) Destroy(papermon);
        rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        thrown = false;
        spawned = false;
        GetComponent<ParentConstraint>().constraintActive = true;
        gameObject.SetActive(true);
    }
}

