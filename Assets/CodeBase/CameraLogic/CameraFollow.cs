using System;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _following;

        private void LateUpdate()
        {
            if (_following == null) return;

            transform.position = new Vector3(
                _following.transform.position.x,
                _following.transform.position.y,
                gameObject.transform.position.z);
        }

        public void Follow(GameObject following) => 
            _following = following.transform;
    }
}