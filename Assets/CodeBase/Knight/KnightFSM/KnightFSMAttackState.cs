using CodeBase.Character.CharacterFSM;
using CodeBase.StaticData;

namespace CodeBase.Knight.KnightFSM
{
    public class KnightFSMAttackState : CharacterFSMAttackState
    {
        public KnightFSMAttackState(KnightStateMachine stateMachine, KnightAttacker attacker,
                                    KnightAnimationsController animator, KnightStaticData data)
            : base(stateMachine, attacker, animator, data)
        {
        }

        internal override void CheckTarget()
        {
            if (_stateMachine.Target.gameObject.activeInHierarchy == false)
                _stateMachine.SetState<KnightFSMIdleState>();
        }
    }
}