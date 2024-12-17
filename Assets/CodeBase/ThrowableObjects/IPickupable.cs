using UnityEngine;

namespace CodeBase.ThrowableObjects
{
    public interface IPickable
    {
        bool CanBePickedUp { get; }

        void InitThrow(Vector2 targetPoint, Transform objectsTransform);
        void PickedUp(Transform holdingArea);
        void SwapIn();
        void SwapOut();
    }
}
