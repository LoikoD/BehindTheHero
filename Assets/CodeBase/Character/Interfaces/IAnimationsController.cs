namespace CodeBase.Character.Interfaces
{
    public interface IAnimationsController
    {
        void Run();
        void Idle();
        float Die();
        AttackAnimationInfo Attack();
        void Turn();
    }
}
