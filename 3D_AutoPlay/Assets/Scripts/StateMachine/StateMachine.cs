using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 상태변환 인터페이스
public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}


/// <summary>
/// 각 상태를 동작 시켜 줄 StateMachine
/// </summary>
public abstract class StateMachine
{
    protected IState currentState;
    
    public void ChangeState(IState state)
    {
        currentState?.Exit(); // 현재 상태가 있다면? =>Exit(); 호출
        currentState = state; //그리고 현재 상태를 변경하고
        currentState?.Enter(); //변경된 상태에 Enter()를 호출한다.
    }

    public void HandleInput()
    {
        // 현재 상태의 HandleInput 출력
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicUpdate()
    {
        currentState?.PhysicsUpdate();
    }

}
