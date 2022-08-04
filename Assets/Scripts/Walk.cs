using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PhysicMovement, ICharacterState
{
    new void Awake()
    {
        base.Awake();
        base._groundSpeed = 7;
        base._maxGroundSpeed = 7;
        base._forceMode = ForceMode.Acceleration;
    }
    public void StateHandle(PlayerController pc)
    {
        Vector3 dir = new Vector3(pc.inputMoveDirection.x, 0, pc.inputMoveDirection.y);
        base.Move(dir);
    }
}
