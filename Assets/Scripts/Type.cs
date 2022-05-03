using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type : MonoBehaviour
{
    
    public paperType type = paperType.WATER;

    public static float calculateMultiplier(paperType attacker, paperType defender)
    {
        switch (attacker)
        {
            case paperType.FIRE:
                if (defender.Equals(paperType.GRASS)) return 2f;
                else if (defender.Equals(paperType.WATER)) return 0.5f;
                else if (defender.Equals(paperType.FIRE)) return 1f;
                break;
            case paperType.WATER:
                if (defender.Equals(paperType.GRASS)) return 0.5f;
                else if (defender.Equals(paperType.WATER)) return 1f;
                else if (defender.Equals(paperType.FIRE)) return 2f;
                break;
            case paperType.GRASS:
                if (defender.Equals(paperType.GRASS)) return 1f;
                else if (defender.Equals(paperType.WATER)) return 2f;
                else if (defender.Equals(paperType.FIRE)) return 0.5f;
                break;
        }
        return 1f;
    }

    public enum paperType
    {
        FIRE,
        WATER,
        GRASS
    };
}
