using CodeBase.ThrowableObjects.StateMachine.States;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Core
{
    public class ThrowableObject : ThrowableObjectBase, IEquippable, IPickable
    {
        public bool CanBeEquipped => _stateMachine?.CurrentState is ObjectFSMMovingState;
        public bool CanBePickedUp => _stateMachine?.CurrentState is ObjectFSMIdleState or ObjectFSMDisappearingState;

        public void InitThrow(Vector2 targetPoint)
        {
            transform.SetParent(_objectsHolder, true);
            Vector3 targetDirection = (Vector3)targetPoint - transform.position;
            TargetPoint = transform.position + targetDirection.normalized * _staticData.MaxDistance;
            
            _stateMachine.SetState<ObjectFSMMovingState>();
        }

        public void AfterEquipped()
        {
            _stateMachine.SetState<ObjectFSMDisabledState>();
        }

        public void PickedUp(Transform holdingArea)
        {
            transform.SetParent(holdingArea, false);
            transform.localPosition = Vector3.zero;
            _stateMachine.SetState<ObjectFSMPickedUpState>();
        }

        public void SwapIn()
        {
            gameObject.SetActive(true);
        }

        public void SwapOut()
        {
            gameObject.SetActive(false);
        }
    }
}
