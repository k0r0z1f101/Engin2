using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : PhysicMovement, ICharacterState
{
    new void Awake()
    {
        base.Awake();
        base._groundSpeed = 15;
        base._maxGroundSpeed = 15;
        base._forceMode = ForceMode.Acceleration;
    }
    public void StateHandle(PlayerController pc)
    {
        Vector3 dir = new Vector3(pc.inputMoveDirection.x, 0, pc.inputMoveDirection.y);
        base.Move(dir);
    }
}
