using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public int numAlive = 3;
    public bool hasWon = false;
    public TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (hasWon)
        {
            text.text = "You Win!";
        }
    }

    public void updateNumAlive()
    {
        numAlive -= 1;
        if (numAlive == 0) hasWon = true;
    }
}
