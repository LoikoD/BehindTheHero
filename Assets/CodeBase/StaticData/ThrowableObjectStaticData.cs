using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "ObjectData", menuName = "StaticData/Object")]
    public class ThrowableObjectStaticData : ScriptableObject
    {
        public float MaxDistance = 5;
        
        [Tooltip("Distance from the target point where object will stop")]
        public float DistanceEpsilon = 0.1f;

        public float TimeToDisappear = 7;
    }
}
