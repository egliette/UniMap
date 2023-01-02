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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("UpdateState: Input.GetKey(KeyCode.LeftShift)");
            m_player.SwitchState(Enums.PlayerState.RUNNING);
        }
    }

}
