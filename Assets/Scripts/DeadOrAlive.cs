using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadOrAlive : MonoBehaviour
{
    public GameObject winText;

    void OnDestroy()
    {
        var wt = winText.GetComponent<WinCondition>();
        wt.updateNumAlive();
    }
}
