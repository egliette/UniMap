using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private float m_runSpeed = 2f;

    public PlayerRunState(PlayerManager player): base(player) { }
    public override void EnterState()
    {
        m_player.GetMovementController().SetSpeed(m_runSpeed);
    }

    public override void UpdateState()
    {
        // CHANGE RECOIL

        // LEAVE STATE
        if (m_player.GetMovementController().IsGrounded())
        {
            // if pressed Jump 
            if (Input.GetButtonDown("Jump"))
            {
                m_player.GetMovementController().SetJumping(true);
            }
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                m_player.SwitchState(Enums.PlayerState.NORMAL);
            }
        }
        else
        {
            m_player.SwitchState(Enums.PlayerState.ONAIR);
        }

    }
}
