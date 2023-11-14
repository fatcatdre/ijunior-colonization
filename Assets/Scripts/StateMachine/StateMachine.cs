using UnityEngine;

namespace FSM
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State _initialState;

        private State _currentState;

        private void Awake()
        {
            ChangeState(_initialState.name);
        }

        private void Update()
        {
            if (_currentState != null)
                _currentState.Process();
        }

        public void ChangeState(string newState)
        {
            Transform stateTransform = transform.Find(newState);

            if (stateTransform == null)
            {
                Debug.LogError($"StateMachine {name} couldn't find GameObject named {newState}.");
                return;
            }

            if (stateTransform.TryGetComponent(out State state))
            {
                if (_currentState == null)
                {
                    _currentState = state;
                    _currentState.Enter();
                }
                else if (_currentState != state)
                {
                    _currentState.Exit();
                    _currentState = state;
                    _currentState.Enter();
                }
            }
            else
            {
                Debug.LogError($"State script not found on GameObject named {newState}.");
            }
        }
    }
}
