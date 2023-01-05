using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerManager m_player;

    public PlayerBaseState(PlayerManager player)
    {
        m_player = player;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public void LeaveState(Enums.PlayerState state)
    {
        m_player.SwitchState(state);
    }
}
