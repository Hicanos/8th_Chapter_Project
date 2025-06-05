using UnityEngine;

/// <summary>
/// 플레이어의 컨트롤을 제어하는 클래스입니다.
/// </summary>
public class PlayerController : MonoBehaviour
{

    public PlayerInputs PLInputs {  get; private set; }
    public PlayerInputs.PlayerActions PLActions { get; private set; }


    void Start()
    {
        PLInputs = new PlayerInputs();
        PLActions = PLInputs.Player;
    }

    private void OnEnable()
    {
        PLInputs.Enable();
    }

    private void OnDisable()
    {
        PLInputs.Disable();
    }
}
