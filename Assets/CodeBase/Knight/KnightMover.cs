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
            transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
        }
    }
}