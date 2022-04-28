using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapermonAI : MonoBehaviour
{
    [Range(0, 30f)]
    public float detectionRange = 10f;
    public string enemyTag;
    public LayerMask enemyLayers;

    public int weakHealthThreshold;
    [Range(0f, 1f)]
    public float defaultCaptureRate = .1f;
    public PapermonAIState state = PapermonAIState.IDLE;
    public GameObject enemy;
    public AttackBase attackMove;

    public Collider[] detectionResults = new Collider[10];

    private void Update()
    {
        switch (state)
        {
            case PapermonAIState.IDLE: IdleMove(); break;
            case PapermonAIState.COMBATING: CombatingMove(); break;
        }
    }

    public void TriggerCombat(GameObject target)
    {
        state = PapermonAIState.COMBATING;
        enemy = target;
    }

    private void IdleMove()
    {
        if (enemy)
        {
            state = PapermonAIState.COMBATING;
            return;
        }

        var count = Physics.OverlapSphereNonAlloc(transform.position, detectionRange, detectionResults, enemyLayers);
        for (int i = 0; i < count; i++)
        {
            var r = detectionResults[i];
            if (string.IsNullOrEmpty(enemyTag) || r.CompareTag(enemyTag))
            {
                if (r.TryGetComponent<PapermonAI>(out var ai))
                {
                    enemy = r.gameObject;
                    break;
                }
            }
        }
    }


    private void CombatingMove()
    {
        if (!enemy)
        {
            state = PapermonAIState.IDLE;
            return;
        }
        if (!attackMove) attackMove = GetComponent<AttackBase>();
        attackMove?.Attack(enemy);

    }
}

public enum PapermonAIState
{
    IDLE,
    COMBATING
}