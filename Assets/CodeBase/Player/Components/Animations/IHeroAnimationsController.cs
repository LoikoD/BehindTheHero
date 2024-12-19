namespace CodeBase.Player.Components.Animations
{
    public interface IHeroAnimationsController
    {
        void Idle();
        void Run();
        void SetHasItem(bool hasItem);
        void Throw();
        void Turn();
    }
}