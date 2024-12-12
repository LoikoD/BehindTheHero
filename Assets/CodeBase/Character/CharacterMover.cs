using CodeBase.Character.Interfaces;
using UnityEngine;

namespace CodeBase.Character
{
    public abstract class CharacterMover : MonoBehaviour, IMover
    {
        private float _moveSpeed;

        public void Construct(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void Move(Transform target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
        }
    }
}
