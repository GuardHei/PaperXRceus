using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Papercube : MonoBehaviour
{
    public LayerMask layerMask;
    public string tagCheck;
    public bool flag;
    public float duration = 10f;

    public void Throw()
    {
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (flag) return;

        int layer = 1 << other.gameObject.layer;

        if ((layerMask.value & layer) != 0)
        {
            if (!string.IsNullOrEmpty(tagCheck) && !other.CompareTag(tagCheck)) return;
            if (other.TryGetComponent<PapermonAI>(out var ai))
            {
                flag = true;
                if (CanCapture(this, ai))
                {
                    CapturePapermon(ai);
                    Destroy(gameObject);
                }
                else
                {
                    var rigid = GetComponent<Rigidbody>();
                    rigid.velocity = -(rigid.velocity * .6f);
                    GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }

    public void CapturePapermon(PapermonAI ai)
    {
        Debug.Log("Congrats");
    }

    public static bool CanCapture(Papercube cube, PapermonAI ai)
    {
        var rate = ai.defaultCaptureRate;
        var health = ai.GetComponent<Health>();
        if (health.currentHealth <= ai.weakHealthThreshold) rate += 0.25f;
        if (ai.state == PapermonAIState.IDLE) rate += 0.1f;

        rate = Mathf.Clamp(rate, .0001f, .999f);

        return Random.value <= rate;
    }
}