using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController m_Controller;
    private Vector3 m_PlayerVelocity;
    private bool m_IsGrounded;
    
    public float m_Speed = 5f;
    public float m_Gravity = -9.8f;
    public float m_JumpHeight = 1.5f;
    
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        m_IsGrounded = m_Controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        m_Controller.Move(transform.TransformDirection(moveDirection) * m_Speed * Time.deltaTime);
        m_PlayerVelocity.y += m_Gravity * Time.deltaTime;
        if (m_IsGrounded && m_PlayerVelocity.y < 0)
            m_PlayerVelocity.y = -2f;
        m_Controller.Move(m_PlayerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (m_IsGrounded)
        {
            m_PlayerVelocity.y = Mathf.Sqrt(m_JumpHeight * -3.0f * m_Gravity);
        }
    }
}
