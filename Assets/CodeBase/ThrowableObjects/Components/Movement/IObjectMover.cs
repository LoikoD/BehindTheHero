using System;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Components.Movement
{
    public interface IObjectMover
    {
        event Action Moved;

        void StartMoving(Vector3 targetPoint);
        void Move();
    }
}