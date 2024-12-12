using UnityEngine;

namespace CodeBase.StaticData
{
    public abstract class CharacterStaticData : ScriptableObject
    {
        public int Hp;
        public float Damage;
        public float MoveSpeed;
        public float AttackRange;
    }
}
