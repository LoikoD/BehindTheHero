using CodeBase.Character.Interfaces;
using UnityEngine;

namespace CodeBase.Character
{
    public abstract class CharacterMover : MonoBehaviour, IMover
    {
        private protected float _moveSpeed;

        public void Construct(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public virtual void Move(Transform target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
        }

        public float DistanceToTarget(Transform target)
        {
            return Vector3.Distance(transform.position, target.position);
        }
    }
}
