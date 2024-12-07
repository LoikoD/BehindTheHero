using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeID Type;
        public float AttackRange;
        public float AttackCooldown;
        public int MaxHp = 100;
        public int Damage = 35;
        public float DamageRange = 0.9f;
        public float Speed = 0.7f;
        public GameObject Prefab;
        public LayerMask KnightLayer;
    }
}
