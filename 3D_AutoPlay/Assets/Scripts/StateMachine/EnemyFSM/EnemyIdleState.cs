using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 에너미가 서있는 상태
/// </summary>
public class EnemyIdleState : EnemyBaseState
{
    // 생성자
    public EnemyIdleState(EnemyStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;

        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }
}
