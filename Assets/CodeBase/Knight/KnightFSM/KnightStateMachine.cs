using CodeBase.Character.CharacterFSM;
using CodeBase.StaticData;

namespace CodeBase.Knight.KnightFSM
{
    public class KnightStateMachine : CharacterStateMachine
    {
        public KnightStateMachine(KnightMover movement, KnightAttacker attacker, KnightStaticData data, KnightAnimationsController animator)
        {
            HasDied = false;

            AddState(new KnightFSMIdleState(this, movement, data, animator));
            AddState(new KnightFSMChaseState(this, movement, animator, data));
            AddState(new KnightFSMAttackState(this, attacker, animator, data));
            AddState(new KnightFSMDieState(this));

            SetState<KnightFSMIdleState>();
        }
    }
}