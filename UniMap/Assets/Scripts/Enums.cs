using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public enum PlayerState
    {
        NORMAL = 0, // Normal state, normal walking speed
        RUNNING = 1, // When player holding shift, run faster, worst recoil
        AIMING = 2, // When player holding right mouse, normal walking speed, better recoil
        ONAIR = 3, // When player in the air, disable firing
    }
}
