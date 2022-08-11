using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PhysicMovement, ICharacterState
{
    [SerializeField] private sbyte jumpSpeed = 7;

    private new void Awake()
    {
        base.Awake();
        base._forceMode = ForceMode.Impulse;
        base._groundSpeed = 1;
    }

    public bool StateHandle(PlayerController pc)
    {
        base.Move(new Vector3(0, jumpSpeed, 0));
        return true;
    }
}
