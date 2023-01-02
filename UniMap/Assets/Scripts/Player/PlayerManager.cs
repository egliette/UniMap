using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovementController m_movementController;


    // states
    private PlayerBaseState m_currentState;
    private PlayerNormalState m_normalState;
    private PlayerRunState m_runState;


    // Animation
    [SerializeField] private Animator m_animator;

    private void Start()
    {
        m_normalState = new PlayerNormalState(this);
        m_runState = new PlayerRunState(this);


        m_currentState = m_normalState;
        m_currentState.EnterState();
    }

    // Update is called once per frame
    private void Update()
    {
        SetAnimation();
        m_currentState.UpdateState();
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

    #endregion


    #region Animation Methods
    private void SetAnimation()
    {
        m_animator.SetFloat("hzInput", GetMovementController().GetHorizontalInput());
        m_animator.SetFloat("vInput", GetMovementController().GetVerticalInput());

        m_animator.SetFloat("speed", GetMovementController().GetSpeed() / PublicVariables.PLAYER_BASE_SPEED);
    }
    #endregion


}
