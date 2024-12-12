using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;

namespace CodeBase.EnemiesScripts.Controller
{
    public interface IWeaponDamageable
    {
        void TakeDamageFromWeapon(float damage, Weapon weapon);
        void TakeDamageFromFists(float damage, Weapon weapon, float interval);
    }
}
