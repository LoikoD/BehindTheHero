using System;

namespace CodeBase.ThrowableObjects.Components.Disappearing
{
    public interface IDisappearable
    {
        event Action Disappeared;

        void StartDisappear();
        void StopDisappear();
    }
}