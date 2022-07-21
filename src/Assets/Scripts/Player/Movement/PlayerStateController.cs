using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerBaseState CurrentState {get{return currentState;}}
    public PlayerBaseState EnterState;
    PlayerBaseState newState;
    public PlayerIndexController faces;
    public Transform movePoint;
    public float moveSpeed = 5f;

    void Awake(){
        currentState = EnterState;
    }

    void Update(){
        if(Vector3.Distance(transform.position, movePoint.position) >= .001f){
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        }
            newState = currentState.UpdateState();
            Debug.Log(newState.GetState());
            if(newState != currentState){
                newState.EnterState();
                currentState.ExitState();
                currentState = newState;
            }

    }
}
