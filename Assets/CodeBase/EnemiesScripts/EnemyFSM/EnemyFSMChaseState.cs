using CodeBase.StaticData;
using CodeBase.Character.CharacterFSM;
using CodeBase.EnemiesScripts.Controller;

namespace CodeBase.EnemiesScripts.EnemyFSM
{
    public class EnemyFSMChaseState : CharacterFSMChaseState
    {
        public EnemyFSMChaseState(EnemyStateMachine stateMachine, EnemyMover mover, EnemyAnimationsController animator, EnemyStaticData data) : base(stateMachine, mover, animator, data)
        {
        }
    }
}
