namespace CodeBase.Character
{
    public class AttackAnimationInfo
    {
        public float Duration { get; }
        public float HitDelay { get; }
        public float HitInterval { get; }
        public AttackAnimationInfo(float duration, float hitDelay, float hitInterval)
        {
            Duration = duration;
            HitDelay = hitDelay;
            HitInterval = hitInterval;
        }
    }
}
