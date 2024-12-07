using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "KnightData", menuName = "StaticData/Knight")]
    public class KnightStaticData : ScriptableObject
    { 
        public int Hp;

        public float Damage;
        
        public float MoveSpeed;
        
        public float AggroRange;
        
        public float AttackRange;

        public float PickUpRange;

        public LayerMask Enemy;
    }
}