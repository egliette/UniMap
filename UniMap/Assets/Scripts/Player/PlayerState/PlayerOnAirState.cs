using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnAirState : PlayerBaseState
{
    private float m_onAirSpeed = 1f;
    public PlayerOnAirState(PlayerManager player): base(player) { }
    public override void EnterState()
    {
        m_player.GetMovementController().SetSpeed(m_onAirSpeed);
    }

    public override void UpdateState()
    {
        //DISABLE FIRING

        // LEAVE STATE
        if (m_player.GetMovementController().IsGrounded()) {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_player.SwitchState(Enums.PlayerState.RUNNING);
            }
            else
            {
                m_player.SwitchState(Enums.PlayerState.NORMAL);
            }
        }
    }
}
