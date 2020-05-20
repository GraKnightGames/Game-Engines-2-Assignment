using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public StateMachine owner;
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Execute() { }
}
public class StateMachine : MonoBehaviour
{
    public State currentState;
    public State prevState;
    public float updatesPerSecond = 0.2f; // Ensures that the coroutine does not update every second, which would cause a crash
    private void OnEnable()
    {
        StartCoroutine(Execute()); //Calls execute on the current state
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ChangeState(State newState)
    {
        prevState = currentState; 
        if (currentState != null)
        {
            currentState.Exit(); //Calls exit on the current state in order to execute any code in this function before changing
        }
        currentState = newState; //Changes state
        currentState.owner = this;
        currentState.Enter(); //Calls Enter on the new state, fully changing the state
    }
    IEnumerator Execute()
    {
        yield return new WaitForSeconds(updatesPerSecond);
        while (true)
        {
            if (currentState != null)
            {
                currentState.Execute();
            }
            yield return new WaitForSeconds(updatesPerSecond);
        }
    }
}