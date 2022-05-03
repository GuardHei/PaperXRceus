using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public bool isAlive;
    public bool destroyOnDeath = true;

    public Image hb;

    public UnityEvent takeDamageEvent;
    public UnityEvent deathEvent;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive)
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (hb)
        {
            hb.fillAmount = (float) currentHealth / (float) maxHealth;
            Debug.Log("fill amount: " + hb.fillAmount);
        }

        takeDamageEvent?.Invoke();
        if (currentHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        isAlive = false;
        deathEvent?.Invoke();
        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
}
