using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region moving

    [SerializeField] private CharacterController m_characterController;
    [SerializeField] private float m_speed = 10f;

    #endregion


    #region jumping
    [SerializeField] private float m_jumpHeight = 5f;

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
        CheckGrounded();
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * x + transform.forward * z;
        m_characterController.Move(direction * m_speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            Jump();
        }

        ApplyGravity();

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
        m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_groundCheck.position, m_groundOffset);
    }
}
