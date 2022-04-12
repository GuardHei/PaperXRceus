using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour
{
    public int damage;
    public LayerMask canDamage;
    public string damageTag;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        CheckDamage(collision.collider);
    }

    void OnTriggerEnter(Collider other)
    {
        CheckDamage(other);
    }

    void CheckDamage(Collider other)
    {
        int layer = 1 << other.gameObject.layer;

        if ((canDamage.value & layer) != 0)
        {
            if (!string.IsNullOrEmpty(damageTag) && !other.CompareTag(damageTag)) return;
            if (other.TryGetComponent<Health>(out var health))
            {
                Debug.Log(3);
                health.TakeDamage(damage);
            }
        }
    }
}
