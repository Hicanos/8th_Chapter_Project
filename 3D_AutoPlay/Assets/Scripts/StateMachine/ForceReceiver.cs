using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f;

    private float verticalVelocity;
    public Vector3 Movement => Vector3.up*verticalVelocity;

    private Vector3 dampingVelocity;
    private Vector3 impact;

    private void Start()
    {
      controller = GetComponent<CharacterController>();  
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            //컨트롤러(캐릭터)가 땅에 있으면 =>중력을 그대로 받고 있음.
            verticalVelocity = Physics.gravity.y*Time.deltaTime;
        }
        else
        {
            //땅에 없으면(공중이면) 중력값을 누적시킴 => 위치에너지에 의한 가속도
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void Reset()
    {
        verticalVelocity = 0;
        impact = Vector3.zero;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }

}
