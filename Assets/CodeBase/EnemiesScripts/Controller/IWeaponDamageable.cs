namespace CodeBase.EnemiesScripts.Controller
{
    public interface IWeaponDamageable
    {
        void TakeDamageFromWeapon(float damage, float delay);
        void TakeDamageFromFists(float damage, float delay, float interval);
    }
}
