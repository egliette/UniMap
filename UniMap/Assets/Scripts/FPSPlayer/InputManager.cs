using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputAction m_PlayerInput;
    private PlayerInputAction.OnFootActions m_OnFoot;

    private PlayerMotor m_Motor;
    private PlayerLook m_Look;

    private void Awake()
    {
        m_PlayerInput = new PlayerInputAction();
        m_OnFoot = m_PlayerInput.OnFoot;
        m_Motor = GetComponent<PlayerMotor>();
        m_Look = GetComponent<PlayerLook>();

        m_OnFoot.Jump.performed += ctx => m_Motor.Jump();
    }

    private void FixedUpdate()
    {
        m_Motor.ProcessMove(m_OnFoot.Movement.ReadValue<Vector2>());        
    }

    private void LateUpdate()
    {
        m_Look.ProcessLook(m_OnFoot.Look.ReadValue<Vector2>());        
    }

    private void OnEnable() 
    {
        m_OnFoot.Enable();    
    }

    private void OnDisable() {
        m_OnFoot.Disable();    
    }
}
