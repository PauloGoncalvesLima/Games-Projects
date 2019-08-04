using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairDirection : MonoBehaviour
{
    public bool negative;
    public int direction;

    private void Update()
    {
        if (negative)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }
}
