using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICharacterState
{
    void StateHandle(PlayerController pc);
}
