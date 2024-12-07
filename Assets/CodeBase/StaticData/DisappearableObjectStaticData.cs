using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu]
    public class DisappearableObjectStaticData : ScriptableObject
    {
        public float DisappearTime = 2;

        [Tooltip("Time between flashes while disappearing")]
        public float TimeBetweenFlashes = 0.3f;
    }
}