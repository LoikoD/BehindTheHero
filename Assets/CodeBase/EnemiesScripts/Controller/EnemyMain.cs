using System;
using CodeBase.Character;

public class EnemyMain : CharacterMain
{
    public event Action<EnemyMain> HasDied;

    internal override void Dead()
    {
        HasDied?.Invoke(this);
        gameObject.SetActive(false);
    }
}