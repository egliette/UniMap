using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalState : PlayerBaseState
{
    private float m_normalSpeed = 1f;
    public PlayerNormalState(PlayerManager player): base(player) { }
    public override void EnterState()
    {
        m_player.GetMovementController().SetSpeed(m_normalSpeed);
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
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_player.SwitchState(Enums.PlayerState.RUNNING);
            }
        }
        else
        {
            m_player.SwitchState(Enums.PlayerState.ONAIR);
        }


    }

}
