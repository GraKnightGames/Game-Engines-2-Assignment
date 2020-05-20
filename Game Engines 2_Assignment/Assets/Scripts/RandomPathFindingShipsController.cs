using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPathFindingShipsController : MonoBehaviour
{
    public State[] states;

    public class StillState : State
    {
        public GameObject trigger;
        public Pathfinding_Random pathfindingBehaviour;
        private Vector3 rot;
        public override void Enter()
        {
            trigger = GameObject.FindGameObjectWithTag("Trigger");
            pathfindingBehaviour = owner.gameObject.GetComponent<Pathfinding_Random>();
            pathfindingBehaviour.enabled = false;
            owner.GetComponent<SteeringBehaviour>().force = new Vector3(0, 0, 0);
        }
        public override void Execute()
        {
            if (trigger.activeSelf == true)
            {
                owner.ChangeState(new RandomPathfindingState());
            }
            else
            {

            }
        }
        public override void Exit()
        {
        }
    }
    public class RandomPathfindingState : State
    {
        public GameObject trigger;
        public Pathfinding_Random pathfindingBehaviour;
        public override void Enter()
        {
            trigger = GameObject.FindGameObjectWithTag("Trigger");
            pathfindingBehaviour = owner.GetComponent<Pathfinding_Random>();
            pathfindingBehaviour.enabled = true;
        }
        public override void Execute()
        {
            if (trigger.activeSelf == false)
            {
                pathfindingBehaviour.enabled = false;
                owner.ChangeState(new StillState());
            }
        }
        public override void Exit()
        {
        }
    }

    private void Start()
    {
        GetComponent<StateMachine>().ChangeState(new StillState());
    }
}