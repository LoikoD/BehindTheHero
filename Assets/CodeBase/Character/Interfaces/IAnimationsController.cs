namespace CodeBase.Character.Interfaces
{
    public interface IAnimationsController
    {
        void Run();
        void Idle();
        void TakeDamage();
        float Die();
        float Attack();
        void Turn();
    }
}
