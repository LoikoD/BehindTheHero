using CodeBase.Infrastructure.States;

namespace CodeBase.ThrowableObjects.StateMachine.Base
{
    public abstract class BaseObjectFSMState : IFsmState
    {
        protected readonly IThrowableCore _context;

        protected BaseObjectFSMState(IThrowableCore context)
        {
            _context = context;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}
