using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "ObjectMoverData", menuName = "StaticData/ObjectMover")]
    public class ObjectMoverStaticData : ScriptableObject
    {
        public float Speed;
        public AnimationCurve Curve;
    }
}
