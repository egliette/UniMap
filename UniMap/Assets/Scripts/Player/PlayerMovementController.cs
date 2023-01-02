using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;


    #region moving

    [SerializeField] private CharacterController m_characterController;
    [SerializeField] private float m_speed;

    #endregion


    #region jumping
    [SerializeField] private float m_jumpHeight = 5f;
    private bool m_isJumping = false;
    #endregion


    #region gravity

    [SerializeField] private Vector3 m_velocity;
    [SerializeField] private float m_gravity = -9.81f;


    #endregion


    #region ground check

    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private float m_groundOffset = 0.4f;
    [SerializeField] private LayerMask m_groundMask;
    private bool m_isGrounded;

    #endregion




    void Update()
    {
        // get inputs
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");


        CheckGrounded();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        ApplyGravity();
    }

    private void Move()
    {
        Vector3 direction = transform.right * m_horizontalInput + transform.forward * m_verticalInput;
        m_characterController.Move(direction * m_speed * Time.deltaTime);

    }


    private void ApplyGravity()
    {
        m_velocity.y += m_gravity * Time.deltaTime;
        m_characterController.Move(m_velocity * Time.deltaTime);
    }

    private void CheckGrounded()
    {
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundOffset, m_groundMask);

        // if grounded -> reset velocity
        if (m_isGrounded)
        {
            m_velocity.y = -2f;
        }

    }

    private void Jump()
    {
        if (m_isJumping)
        {
            m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
            m_isJumping = false;
        }
      
    }

   


    #region debug
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_groundCheck.position, m_groundOffset);
    }
    #endregion



    #region getter setter
    public void SetSpeed(float value)
    {
        m_speed = value * PublicVariables.PLAYER_BASE_SPEED;
    }

    public float GetSpeed()
    {
        return m_speed;
    }

    public float GetHorizontalInput()
    {
        return m_horizontalInput;
    }

    public float GetVerticalInput()
    {
        return m_verticalInput;
    }

    public void SetJumping(bool jump)
    {
        m_isJumping = jump;
    }

    public bool IsGrounded()
    {
        return m_isGrounded;
    }


    #endregion

}
