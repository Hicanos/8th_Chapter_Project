﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Exit() 
    {
        base.Exit(); 
        StopAnimation(stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        //내려와서 땅에 닿으면 Idle로
        if (stateMachine.Player.Controller.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
