using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapermonAI : MonoBehaviour
{
    public int weakHealthThreshold;
    [Range(0f, 1f)]
    public float defaultCaptureRate = .1f;
    public PapermonAIState state = PapermonAIState.IDLE;
    public GameObject enemy;
    public AttackBase attackMove;

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