using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBallAttack : AttackBase
{
    public GameObject rockBall;
    public Transform firePoint;

    void Start()
    {
    }

    public override void Attack(GameObject target)
    {
        if (!CanAttack) return;
        if (!rockBall) return;
        lastAttackTime = Time.time;
        var attackBall = Instantiate(rockBall);
        Physics.IgnoreCollision(attackBall.GetComponent<Collider>(), GetComponent<Collider>());
        var controller = rockBall.GetComponent<RockBallController>();
        controller.target = target.transform.position;
        if (!firePoint) firePoint = transform;
        attackBall.transform.position = firePoint.position;
        attackBall.transform.LookAt(target.transform);
    }
}
