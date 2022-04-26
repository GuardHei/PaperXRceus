using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        thrown = true;
        rb.isKinematic = false;
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
        Destroy(papermon);
        rb.isKinematic = true;
        thrown = false;
        spawned = false;
        gameObject.SetActive(true);
    }
}

