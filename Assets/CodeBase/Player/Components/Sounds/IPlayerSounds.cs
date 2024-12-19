namespace CodeBase.Player.Components.Sounds
{
    public interface IPlayerSounds
    {
        void PlayThrowClip();
        void StartStepSounds(float interval);
        void StopStepSounds();
    }
}