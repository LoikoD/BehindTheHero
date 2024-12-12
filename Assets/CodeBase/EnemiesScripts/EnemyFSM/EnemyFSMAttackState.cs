using CodeBase.StaticData;
using CodeBase.EnemiesScripts.Controller;
using CodeBase.Character.CharacterFSM;

namespace CodeBase.EnemiesScripts.EnemyFSM
{
    public class EnemyFSMAttackState : CharacterFSMAttackState
    {
        public EnemyFSMAttackState(EnemyStateMachine stateMachine, EnemyAttacker attacker, EnemyAnimationsController animator, EnemyStaticData data) : base(stateMachine, attacker, animator, data)
        {
        }
    }
}
