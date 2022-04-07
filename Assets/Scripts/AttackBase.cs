using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    public float minAttackInterval;
    public float lastAttackTime = -1;

    public bool CanAttack => Time.time - lastAttackTime >= minAttackInterval;

    public virtual void Attack(GameObject target)
    {
        if (!CanAttack) return;
    }
}
