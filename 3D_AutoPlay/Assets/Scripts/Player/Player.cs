using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField] public PlayerSO Data {  get; private set; }
    [field: SerializeField] public PlayerConSO ConditionData { get; set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerController Input {  get; private set; }
    public CharacterController Controller { get; private set; }

    public ForceReceiver ForceReceiver { get; private set; }

    private PlayerStateMachine stateMachine;

    public Health Health { get; private set; }
    [field: SerializeField] public Weapon Weapon { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();

        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        Health = GetComponent<Health>();

        stateMachine = new PlayerStateMachine(this);

    }


    void Start()
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
    }

    public void GainExp(float amount)
    {
        ConditionData.AddExp(amount);
    }
    public void GainCoin(int amount)
    {
        ConditionData.AddCoin(amount);
    }
}
