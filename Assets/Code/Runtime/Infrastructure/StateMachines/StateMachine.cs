﻿using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.States;

namespace Code.Runtime.Infrastructure.StateMachines
{
    public class StateMachine : IStateMachine
    {
        private Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
        private IExitableState _activeState;
        
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void RegisterState<TState>(TState state) where TState : IExitableState => 
            _states.Add(typeof(TState), state);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            TState state = GetState<TState>();

            if (state == _activeState) 
                return state;
            
            _activeState?.Exit();
            TryRemoveState();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;

        private void TryRemoveState()
        {
            if (_activeState is IDeletebleState)
                _states.Remove(_activeState.GetType());
        }
    }
}