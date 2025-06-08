using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 에너미가 추적 중인 상태
/// </summary>
public class EnemyChasingState : EnemyBaseState
{
    // 생성자
    public EnemyChasingState(EnemyStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!IsInChaseRange()) // 쫓아가는 범위 내에 없으면 Idle
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        else if (IsInAttackRange()) //공격 범위 내에 있으면 공격으로 전환
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }
}
