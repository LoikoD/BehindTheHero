using CodeBase.Logic;

namespace CodeBase.Knight
{
    public interface IDamageable : IHealth
    {
        void TakeDamage(float damage);
    }
}