using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    //작성된 State들
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get;}
    public PlayerComboAttackState ComboAttackState { get;}


    //움직임에 필요한 변수 (이동 입력, 이동속도, 회전민감도, 이동속도 조정, 점프력)
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float JumpForce { get; set; }

    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }

    // 카메라 위치
    public Transform MainCamTransform { get; set; }



    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        //State 지정(IdleState에 새 PlayerIdleState 넣기)
        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        ComboAttackState = new PlayerComboAttackState(this);


        //메인카메라 위치 가져오기
        MainCamTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;
    }
}
