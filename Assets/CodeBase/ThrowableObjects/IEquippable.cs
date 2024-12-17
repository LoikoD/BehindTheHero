namespace CodeBase.ThrowableObjects
{
    public interface IEquippable
    {
        bool CanBeEquipped { get; }

        void AfterEquipped();
    }
}
