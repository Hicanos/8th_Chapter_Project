using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 에너미가 공격 상태
/// </summary>
public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyApplyForce;
    bool alreadyAppliedDealing;

    //생성자
    public EnemyAttackState(EnemyStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    //콤보 관련 내용은 필요 없음
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);

        alreadyApplyForce = false;
        alreadyAppliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);

    }

    // 콤보 관련 내용 전부 제거
    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Enemy.Data.ForceTransitionTime)
                TryApplyForce();

            if(!alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
            {
                //Weapon 게임오브젝트 켜기
                stateMachine.Enemy.Weapon.SetAttack(stateMachine.Enemy.Data.Damage,
                                                    stateMachine.Enemy.Data.Force);
                stateMachine.Enemy.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }

            if (alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.Data.Dealing_End_TransitionTime)
            {
                //Weapon을 다시 끈다
                stateMachine.Enemy.Weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            if (IsInChaseRange()) // 쫓아가는 범위 내라면 쫓아가고
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else //벗어나버렸으면 추적 종료
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }

    }

    private void TryApplyForce()
    {
        if (alreadyApplyForce) return;
        alreadyApplyForce = true;

        stateMachine.Enemy.ForceReceiver.Reset();

        stateMachine.Enemy.ForceReceiver.AddForce(stateMachine.Enemy.transform.forward * stateMachine.Enemy.Data.Force);

    }

}
