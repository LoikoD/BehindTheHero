using Spine.Unity;

namespace CodeBase.Character.Data
{
    [System.Serializable]
    public class KnightAnimationNames
    {
        [SpineAnimation] public string Run;
        [SpineAnimation] public string Idle;
        [SpineAnimation] public string MeleeAttack;
        [SpineAnimation] public string SwordAttack;
        [SpineAnimation] public string PoleaxeAttack;
        [SpineAnimation] public string TakeDamage;
        [SpineAnimation] public string Stun;
        [SpineAnimation] public string Death;
    }

    [System.Serializable]
    public class HeroAnimationNames
    {
        [SpineAnimation] public string Run;
        [SpineAnimation] public string Idle;
        [SpineAnimation] public string RunWithItem;
        [SpineAnimation] public string IdleWithItem;
        [SpineAnimation] public string Stun;
        [SpineAnimation] public string Throw;
    }

    [System.Serializable]
    public class EnemyAnimationNames
    {
        [SpineAnimation] public string Run;
        [SpineAnimation] public string Attack;
        [SpineAnimation] public string TakeDamage;
        [SpineAnimation] public string Death;
        [SpineAnimation] public string Idle;
    }
}
