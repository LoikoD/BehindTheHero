using UnityEngine;

namespace CodeBase.ThrowableObjects
{
    public interface IPickable
    {
        bool CanBePickedUp { get; }

        void InitThrow(Vector2 targetPoint);
        void PickedUp(Transform holdingArea);
        void SwapIn();
        void SwapOut();
    }
}
