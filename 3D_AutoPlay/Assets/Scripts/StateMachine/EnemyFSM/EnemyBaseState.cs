using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 에너미의 상태(행동) 베이스
/// </summary>
public class EnemyBaseState : IState
{
    // 에너미 스테이트 머신 들고오기
    protected EnemyStateMachine stateMachine;
    // 땅 판정은 같으므로 Player의 GroundData를 읽기로 들고옴

    // Player의 내용을 그대로 들고옴
    protected readonly PlayerGroundData groundData;

    public EnemyBaseState(EnemyStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Enemy.Data.GroundData;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
        Move();
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 dir = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).normalized;
        return dir;
    }

    void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Enemy.Controller.Move(((movementDirection * movementSpeed) + stateMachine.Enemy.ForceReceiver.Movement) * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Enemy.transform.rotation = Quaternion.Lerp(stateMachine.Enemy.transform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected void ForceMove()
    {
        stateMachine.Enemy.Controller.Move(stateMachine.Enemy.ForceReceiver.Movement * Time.deltaTime);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    //주변에 플레이어가 있는지 확인하는 메서드
    protected bool IsInChaseRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        // sqrMagnitude = 공격 범위 내에 있는지 없는지 비교하기 위함. 제곱으로 나눈 것.(제곱근 사용 X)
        // 제곱근 X의 이유 => 제곱근은 부하가 큼. 크기비교만이 목적이라면 제곱으로 비교해도 상관없음.

        return playerDistanceSqr <= stateMachine.Enemy.Data.PlayerChasingRange * stateMachine.Enemy.Data.PlayerChasingRange;
    }
}
