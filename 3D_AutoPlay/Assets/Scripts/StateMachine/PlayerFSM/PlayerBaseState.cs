using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine; //stateMachine에 playerStateMachine을 할당
        groundData = stateMachine.Player.Data.GroundData; // 땅에 붙어있는지 여부를 판독하는 데이터
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }
    public virtual void Exit() 
    {
        RemoveInputActionsCallbacks();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        input.PLActions.Move.canceled += OnMovementCanceled;
        input.PLActions.Run.started += OnRunStarted;

        stateMachine.Player.Input.PLActions.Jump.started += OnJumpStarted;

        stateMachine.Player.Input.PLActions.Attack.performed += OnAttackPerformed;
        stateMachine.Player.Input.PLActions.Attack.canceled += OnAttackCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        input.PLActions.Move.canceled -= OnMovementCanceled;
        input.PLActions.Run.started -= OnRunStarted;

        stateMachine.Player.Input.PLActions.Jump.started -= OnJumpStarted;

        stateMachine.Player.Input.PLActions.Attack.performed -= OnAttackPerformed;
        stateMachine.Player.Input.PLActions.Attack.canceled -= OnAttackCanceled;
    }



    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    //움직임 취소 되었을 때 콜백

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }


    //달리기 시작했을 때 입력 콜백
    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = false;
    }


    //애니메이션 작동 on/off
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);

    }


    //움직임을 감지하는 로직
    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PLActions.Move.ReadValue<Vector2>();

    }

    private void Move()
    {
        Vector3 movementDirection = GetMovemetDirection();
        Move(movementDirection); // Direction 방향으로 이동하고

        Rotate(movementDirection); //Direction 방향으로 회전한다.
    }


    //움직이는 방향을 설정하는 함수
    private Vector3 GetMovemetDirection()
    {
        //앞과 오른쪽. 사실상 x, z축
        Vector3 forward = stateMachine.MainCamTransform.forward;
        Vector3 right = stateMachine.MainCamTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Move(Vector3 direction)
    {
        //movemetSpeed에 이동 속도(SpeedModifier로 조정한 값)를 구해서 할당
        float movementSpeed = GetMovementSpeed();

        stateMachine.Player.Controller.Move(
            ((direction * movementSpeed)+stateMachine.Player.ForceReceiver.Movement) * Time.deltaTime
            );
    }

    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return moveSpeed;
    }

    private void Rotate(Vector3 direction)
    {
        if(direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, 
                                                        targetRotation, 
                                                        stateMachine.RotationDamping * Time.deltaTime);
            //Slerp=> A부터 B까지 C의 형태로 보간
        }

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

}
