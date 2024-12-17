using CodeBase.ThrowableObjects.StateMachine.Base;

namespace CodeBase.ThrowableObjects.StateMachine.States
{
    public class ObjectFSMDisabledState : BaseObjectFSMState
    {
        public ObjectFSMDisabledState(IThrowableCore context) : base(context)
        {

        }

        public override void Enter()
        {
            _context.ReturnToPool();
        }
    }
}
