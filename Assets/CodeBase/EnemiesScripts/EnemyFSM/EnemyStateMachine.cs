using CodeBase.StaticData;
using CodeBase.EnemiesScripts.Controller;
using CodeBase.Character.CharacterFSM;
using UnityEngine;

namespace CodeBase.EnemiesScripts.EnemyFSM
{
    public class EnemyStateMachine : CharacterStateMachine
    {
        public EnemyStateMachine(EnemyMover mover, EnemyAttacker attacker, EnemyStaticData data, EnemyAnimationsController animator, Transform target)
        {
            HasDied = false;
            Target = target;

            AddState(new EnemyFSMChaseState(this, mover, animator, data));
            AddState(new EnemyFSMAttackState(this, attacker, animator, data));
            AddState(new EnemyFSMDieState(this));

            SetDefaultState();
        }

        internal override void SetDefaultState()
        {
            SetState<EnemyFSMChaseState>();
        }
    }
}
