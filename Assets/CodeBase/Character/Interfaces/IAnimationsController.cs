namespace CodeBase.Character.Interfaces
{
    public interface IAnimationsController
    {
        void Run();
        void Idle();
        float Die();
        float Attack();
        void Turn();
    }
}
