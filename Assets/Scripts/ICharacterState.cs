using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICharacterState
{
    public bool StateHandle(PlayerController pc);
}
