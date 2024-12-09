using CodeBase.Logic.Utilities;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightMover : MonoBehaviour
    {
        private float _moveSpeed;

        public void Construct(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void Move(Transform target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += _moveSpeed * Time.deltaTime * direction;
        }
    }
}