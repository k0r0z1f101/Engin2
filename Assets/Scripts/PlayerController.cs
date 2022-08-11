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
    private ICharacterState walk, run, grounded, jump;
    
    private PlayerInputs pInputs;

    delegate bool PlayerMoveState(PlayerController pc);

    private PlayerMoveState _playerMoveState;
    [System.NonSerialized] public Vector2 inputMoveDirection;

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
        grounded = gameObject.AddComponent<Grounded>();
        jump = gameObject.AddComponent<Jump>();
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
            if (!_activeStates.Contains(State.Running))
            {
                AddMoveState(walk, State.Walking);
            }
        }

        if (value.canceled)
        {
            inputMoveDirection = Vector2.zero;
            if (_activeStates.Contains(State.Walking))
            {
                RemoveMoveState(walk, State.Walking);
            }
        }
    }
    
    public void OnRun(InputAction.CallbackContext value)
    {
            print("run");
            if (value.started)
            {
                AddMoveState(run, State.Running);
                if (_activeStates.Contains(State.Walking))
                {
                    RemoveMoveState(run, State.Walking);
                }
            }

            if (value.canceled)
            {
                RemoveMoveState(run, State.Running);
                if (inputMoveDirection != Vector2.zero)
                {
                    AddMoveState(walk, State.Walking);
                }
            }
    }
    
    public void OnJump(InputAction.CallbackContext value)
    {
        print(OnIsGrounded());
        if (value.started && OnIsGrounded())
        {
            jump.StateHandle(this);
        }
    }

    private bool OnIsGrounded()
    {
        return grounded.StateHandle(this);
    }
}
