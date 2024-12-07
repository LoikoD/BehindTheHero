using CodeBase.Logic.Utilities;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightMover : MonoBehaviour
    {
        [SerializeField] private KnightAnimationsController _animator;
        
        private float _moveSpeed;

        public void Construct(float moveSpeed) => 
            _moveSpeed = moveSpeed;

        public void Move(Transform target)
        {
            Vector2 moveDirection = transform.position - target.transform.position;
            
            transform.position = Vector2.Lerp(transform.position, target.position, _moveSpeed * Time.deltaTime);
        }
    }
}