using CodeBase.Infrastructure;
using CodeBase.Logic;

namespace CodeBase.Character.CharacterFSM
{
    public abstract class CharacterStateMachine : DefaultStateMachine
    {
        public IHealth Target { get; internal set; }
        public bool HasDied { get; internal set; }

        public void Reset()
        {
            HasDied = false;
            SetDefaultState();
        }

        public void SetTarget(IHealth target)
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
