using UnityEngine;

namespace CodeBase.Character.Interfaces
{
    public interface IMover
    {
        void Move(Transform target);
        float DistanceToTarget(Transform target);
    }
}