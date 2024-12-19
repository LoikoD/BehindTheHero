using CodeBase.Logic.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _throwSounds;
        [SerializeField] private List<AudioClip> _stepSounds;

        private AudioSource _audioSource;
        private SoundQueuer _soundQueuer;
        private Coroutine _stepCoroutine;

        public void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            _soundQueuer = new();
            _soundQueuer.RegisterSoundList(SoundKeys.Throw, _throwSounds);
            _soundQueuer.RegisterSoundList(SoundKeys.Step, _stepSounds);
        }

        public void PlayThrowClip()
        {
            _audioSource.PlayOneShot(_soundQueuer.GetNextSound(SoundKeys.Throw));
        }

        public void StartStepSounds(float interval)
        {
            _stepCoroutine = StartCoroutine(LoopStepSounds(interval));
        }

        public void StopStepSounds()
        {
            if (_stepCoroutine != null)
            {
                StopCoroutine(_stepCoroutine);
                _stepCoroutine = null;
            }
        }

        private IEnumerator LoopStepSounds(float interval)
        {
            while (true)
            {
                _audioSource.PlayOneShot(_soundQueuer.GetNextSound(SoundKeys.Step));
                yield return new WaitForSeconds(interval);
            }
        }
    }
}