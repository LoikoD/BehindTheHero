using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "KnightData", menuName = "StaticData/Knight")]
    public class KnightStaticData : CharacterStaticData
    { 
        public float PickUpRange;
        public LayerMask TargetLayer;
    }
}