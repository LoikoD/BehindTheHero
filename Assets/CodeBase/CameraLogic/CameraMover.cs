using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraMover : MonoBehaviour
    {
        private readonly Vector3 _targetPosition = new(4.46f, -2.42f, -10f);
        private const float DistanceEpsilon = 0.1f;

        private bool _isMoving = false;

        public event Action Moved;

        public void FixedUpdate()
        {
            if (_isMoving)
            {
                if (Vector3.Distance(transform.position, _targetPosition) > DistanceEpsilon)
                {
                    MoveTo();
                }
                else
                {
                    _isMoving = false;
                    Moved?.Invoke();
                }
            }
        }

        public void StartMoving()
        {
            _isMoving = true;
        }


        private void MoveTo()
        {
            Vector3 newPos = Vector3.Lerp(transform.position, _targetPosition, 1.5f * Time.deltaTime);
            transform.position = newPos;
        }


    }
}
