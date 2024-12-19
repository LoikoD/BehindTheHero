using CodeBase.Logic.Utilities;
using System.Collections;
using UnityEngine;

namespace CodeBase.Character.Base
{
    public abstract class BaseSounds : MonoBehaviour
    {
        private protected AudioSource AudioSource { get; private set; }
        private protected SoundQueuer SoundQueuer { get; private set; }

        private protected virtual void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            SoundQueuer = new();
            RegisterSounds();
        }

        private protected abstract void RegisterSounds();

        private protected void PlaySound(SoundKeys key)
        {
            AudioSource.PlayOneShot(SoundQueuer.GetNextSound(key));
        }

        private protected IEnumerator PlayDelayedSound(SoundKeys key, float delay)
        {
            yield return new WaitForSeconds(delay);
            PlaySound(key);
        }

        private protected IEnumerator PlaySoundMultipleTimes(SoundKeys key, int times, float interval)
        {
            for (int i = 0; i < times; i++)
            {
                PlaySound(key);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
