using System;
using UnityEngine;


/// <summary>
/// 플레이어의 애니메이션 데이터 정보를 담는 스크립트
/// </summary>

[Serializable]
public class PlayerAnimationData
{
    //땅(Ground)에서의 애니메이션 파라미터 이름 - 그대로 가져오기
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";

    //하늘(Air)에서의 애니메이션 파라미터 이름
    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";

    //공격 중(Attack)일 때 애니메이션 파라미터 이름
    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string comboAttackParameterName = "ComboAttack";


    //땅에서의 파라미터 Hash - Hash값으로 변환하여 int로 넣는 쪽이 더 효율이 좋음.
    //변환값을 저장할 변수
    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    //하늘에서의 파라미터 해쉬
    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }

    //공격할 때의 파라미터 해쉬
    public int AttackParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }


    //Player의 Awake에서 사용
    public void Initialize()
    {
        //Ground
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);

        //Air
        AirParameterHash = Animator.StringToHash(airParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        FallParameterHash = Animator.StringToHash(fallParameterName);

        //Attack
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        ComboAttackParameterHash = Animator.StringToHash(comboAttackParameterName);
    }


}
