using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInputs.IPlayerActionsActions
{
    enum State : sbyte
    {
        Walking,
        Running
    };

    [SerializeField] private List<State> _activeStates = new List<State>();
    private ICharacterState walk, run;
    
    private PlayerInputs pInputs;

    delegate void PlayerMoveState(PlayerController pc);

    private PlayerMoveState _playerMoveState;
    [System.NonSerialized] public Vector3 inputMoveDirection;

    private void Awake()
    {
        AddStatesComponents();
        pInputs = new PlayerInputs();
        pInputs.PlayerActions.SetCallbacks(this);
    }

    private void AddStatesComponents()
    {
        walk = gameObject.AddComponent<Walk>();
        run = gameObject.AddComponent<Run>();
    }

    private void AddMoveState(ICharacterState state, State newState)
    {
        _playerMoveState += state.StateHandle;
        if(!_activeStates.Contains(newState)) _activeStates.Add(newState);
    }
    
    private void RemoveMoveState(ICharacterState state, State endedState)
    {
        _playerMoveState -= state.StateHandle;
        if(_activeStates.Contains(endedState)) _activeStates.Remove(endedState);
    }

    private void FixedUpdate()
    {
        if (_playerMoveState != null) _playerMoveState(this);
    }

    private void OnEnable()
    {
        pInputs.Enable();
    }

    private void OnDisable()
    {
        pInputs.Disable();
    }

    public void OnWalk(InputAction.CallbackContext value)
    {
        print("walk");
        inputMoveDirection = value.ReadValue<Vector2>();
        if (value.started)
        {
            AddMoveState(walk, State.Walking);
        }

        if (value.canceled)
        {
            RemoveMoveState(walk, State.Walking);
        }
    }
    
    public void OnRun(InputAction.CallbackContext value)
    {
        if (_activeStates.Contains(State.Walking))
        {
            print("run");
            if (value.started)
            {
                AddMoveState(run, State.Running);
            }

            if (value.canceled)
            {
                RemoveMoveState(run, State.Running);
            }
        }
    }
    
    public void OnJump(InputAction.CallbackContext value)
    {
        print("jump");
    }
}
