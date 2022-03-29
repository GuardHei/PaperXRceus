using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapermonAI : MonoBehaviour
{

    public PapermonAIState state = PapermonAIState.IDLE;
    public GameObject enemy;

    private void Update()
    {
        switch (state)
        {
            case PapermonAIState.IDLE: IdleMove(); break;
            case PapermonAIState.PREPARING: PreparingMove(); break;
            case PapermonAIState.COMBATING: CombatingMove(); break;
        }
    }

    private void IdleMove()
    {

    }

    private void PreparingMove()
    {

    }

    private void CombatingMove()
    {

    }
}

public enum PapermonAIState
{
    IDLE,
    PREPARING,
    COMBATING
}