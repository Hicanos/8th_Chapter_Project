using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: Header("Reference")]

    [field: SerializeField] public EnemySO Data { get; private set; }
    [field: SerializeField] public EnemyConSO ConData { get; private set; }

    [field: Header("Animations")]

    // 플레이어랑 동일한 애니메이션 사용
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    [field: Header("Condition")]
    public Health Health { get; private set; }
    private UICondition uiCondition { get; set; }

    private EnemyStateMachine stateMachine;

    [field: SerializeField] public Weapon Weapon { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        Health = GetComponent<Health>();

        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);

        Health.OnDie += OnDie;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicUpdate();
    }
    void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;

        Player player = FindObjectOfType<Player>();
        if(player != null)
        {
            player.GainExp(ConData.RewardExp);
            uiCondition.UpdateUI(player.ConditionData);
        }

        StartCoroutine("DeathTime");
    }

    //코루틴

    IEnumerator DeathTime()
    {
        
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject); //캐릭터 사망 후 파괴시킴
    }
}
