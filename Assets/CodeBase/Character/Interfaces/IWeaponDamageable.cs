namespace CodeBase.Character.Interfaces
{
    public interface IWeaponDamageable
    {
        void TakeDamageFromWeapon(float damage, float delay);
        void TakeDamageFromFists(float damage, float delay, float interval);
    }
}
