using CodeBase.Infrastructure.States;

namespace CodeBase.Character.CharacterFSM
{
    public abstract class CharacterFSMDieState : IFsmState
    {
        private readonly CharacterStateMachine _stateMachine;

        public CharacterFSMDieState(CharacterStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateMachine.SetDieFlag();
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }
    }
}
