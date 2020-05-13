using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_Trigger : MonoBehaviour
{
    public bool pathfindingEnabled;
    private void Start()
    {
        pathfindingEnabled = false;
    }

    public void TurnOn()
    {
        pathfindingEnabled = true;
    }

    public void turnOff()
    {
        pathfindingEnabled = false;
    }
}
