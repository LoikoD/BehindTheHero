using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero")]
    public class PlayerStaticData : ScriptableObject
    {
        [Header("MOVEMENT")]
        public float MaxSpeed = 10;
        public float Acceleration = 120;
        public float Deceleration = 120;
    }
}
