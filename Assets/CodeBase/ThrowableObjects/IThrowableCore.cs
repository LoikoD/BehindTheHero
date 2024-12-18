using CodeBase.StaticData;
using CodeBase.ThrowableObjects.StateMachine;
using UnityEngine;

namespace CodeBase.ThrowableObjects
{
    public interface IThrowableCore
    {
        ThrowableObjectStaticData StaticData { get; }
        ObjectStateMachine StateMachine { get; }
        Vector3 TargetPoint { get; }
        void Disable();
    }
}
