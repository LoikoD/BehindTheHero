using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : CharacterStaticData
    {
        public EnemyTypeID Type;
        public float AttackCooldown;
        public GameObject Prefab;
    }
}
