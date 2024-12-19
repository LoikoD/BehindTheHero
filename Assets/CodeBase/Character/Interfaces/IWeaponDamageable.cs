namespace CodeBase.Character.Interfaces
{
    public interface IWeaponDamageable
    {
        void TakeDamageFromWeapon(float damage);
        void TakeDamageFromFists(float damage, float interval);
    }
}
