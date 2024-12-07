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
        private Coroutine _stepCoroutine;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayThrowClip()
        {
            int clipIndex = Random.Range(0, _throwSounds.Count);
            _audioSource.PlayOneShot(_throwSounds[clipIndex]);

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
            }
        }

        private void PlayStepClip()
        {
            int clipIndex = Random.Range(0, _stepSounds.Count);
            _audioSource.PlayOneShot(_stepSounds[clipIndex]);
        }

        private IEnumerator LoopStepSounds(float interval)
        {
            while (true)
            {
                PlayStepClip();
                yield return new WaitForSeconds(interval);
            }
        }
    }
}