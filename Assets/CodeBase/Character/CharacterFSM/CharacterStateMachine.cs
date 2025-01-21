using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Character.CharacterFSM
{
    public abstract class CharacterStateMachine : DefaultStateMachine
    {
        public Transform Target { get; internal set; }
        public bool HasDied { get; internal set; }

        public void Reset()
        {
            HasDied = false;
            SetDefaultState();
        }

        public void SetTarget(Transform target)
        {
            Target = target;
        }

        public void SetDieFlag()
        {
            HasDied = true;
        }

        internal abstract void SetDefaultState();
    }
}
