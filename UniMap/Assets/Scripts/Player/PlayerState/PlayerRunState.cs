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
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("UpdateState: NOT Input.GetKey(KeyCode.LeftShift)");
            m_player.SwitchState(Enums.PlayerState.NORMAL);
        }
    }
}
