using CodeBase.Character.CharacterFSM;
using CodeBase.Character.Interfaces;
using CodeBase.Logic.Utilities;
using UnityEngine;

namespace CodeBase.Character
{
    public class CharacterTurner : MonoBehaviour, ITurner
    {
        private CharacterStateMachine _stateMachine;
        private IAnimationsController _animationsController;
        private HorizontalDirection _horizontalDirection;

        public void Construct(CharacterStateMachine stateMachine, IAnimationsController animationsController)
        {
            _stateMachine = stateMachine;
            _animationsController = animationsController;
            _horizontalDirection = HorizontalDirection.Right;
        }

        private void Update()
        {
            Turn();
        }

        public void Turn()
        {
            if (_stateMachine.Target == null)
                return;

            Vector2 vectorToTarget = _stateMachine.Target.position - transform.position;
            if (vectorToTarget.x > 0 && _horizontalDirection != HorizontalDirection.Right)
            {
                _horizontalDirection = HorizontalDirection.Right;
                _animationsController.Turn();
            }
            else if (vectorToTarget.x < 0 && _horizontalDirection != HorizontalDirection.Left)
            {
                _horizontalDirection = HorizontalDirection.Left;
                _animationsController.Turn();
            }
        }
    }
}
