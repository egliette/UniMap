using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovementController m_movementController;
    [SerializeField] private PlayerFiringController m_firingController;

    // states
    private PlayerBaseState m_currentState;
    private PlayerNormalState m_normalState;
    private PlayerRunState m_runState;
    private PlayerOnAirState m_onAirState;


    // Animation
    [SerializeField] private Animator m_animator;

    private void Start()
    {
        m_normalState = new PlayerNormalState(this);
        m_runState = new PlayerRunState(this);
        m_onAirState = new PlayerOnAirState(this);


        m_currentState = m_normalState;
        m_currentState.EnterState();
    }

    // Update is called once per frame
    private void Update()
    {
        m_currentState.UpdateState();
        SetAnimation();
    }

    #region Public
    public void SwitchState(Enums.PlayerState state)
    {
        switch (state)
        {
            case Enums.PlayerState.NORMAL:
                {
                    m_currentState = m_normalState;
                    break;
                }
            case Enums.PlayerState.RUNNING:
                {
                    m_currentState = m_runState;
                    break;
                }
            case Enums.PlayerState.AIMING:
                {
                    break;
                }
            case Enums.PlayerState.ONAIR:
                {
                    m_currentState = m_onAirState;
                    break;
                }
        }
        m_currentState.EnterState();
    }
    #endregion


    #region getter setter
    public PlayerMovementController GetMovementController()
    {
        return m_movementController;
    }

    public PlayerFiringController GetFiringController()
    {
        return m_firingController;
    }

    #endregion


    #region Animation Methods
    private void SetAnimation()
    {
        m_animator.SetFloat("hzInput", GetMovementController().GetHorizontalInput());
        m_animator.SetFloat("vInput", GetMovementController().GetVerticalInput());

        m_animator.SetFloat("speed", GetMovementController().GetSpeed() / PublicVariables.PLAYER_BASE_SPEED);
        m_animator.SetBool("grounded", GetMovementController().IsGrounded());
    }
    #endregion


}
