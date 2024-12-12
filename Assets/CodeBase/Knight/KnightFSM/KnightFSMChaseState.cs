using CodeBase.Character.CharacterFSM;
using CodeBase.StaticData;

namespace CodeBase.Knight.KnightFSM
{
    public class KnightFSMChaseState : CharacterFSMChaseState
    {
        public KnightFSMChaseState(KnightStateMachine stateMachine, KnightMover mover,
                                  KnightAnimationsController animator, KnightStaticData data)
            : base(stateMachine, mover, animator, data)
        {
        }
    }
}