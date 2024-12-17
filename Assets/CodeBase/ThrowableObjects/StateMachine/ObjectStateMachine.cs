using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.ThrowableObjects.Components.Disappearing;
using CodeBase.ThrowableObjects.Components.Movement;
using CodeBase.ThrowableObjects.StateMachine.States;

namespace CodeBase.ThrowableObjects.StateMachine
{
    public class ObjectStateMachine : DefaultStateMachine
    {
        public IFsmState CurrentState => _currentState;

        public ObjectStateMachine(IThrowableCore core, IObjectMover objectMover, IDisappearable disappearable)
        {
            AddState(new ObjectFSMIdleState(core));
            AddState(new ObjectFSMDisappearingState(core, disappearable));
            AddState(new ObjectFSMMovingState(core, objectMover));
            AddState(new ObjectFSMPickedUpState(core));
            AddState(new ObjectFSMDisabledState(core));
        }
    }
}
