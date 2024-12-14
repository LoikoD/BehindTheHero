using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Dialogue
{
    public class TypingSoundsController : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _typingSounds;

        private AudioSource _audioSource;
        private Queue<AudioClip> _typingSoundsQueue;

        public void Construct()
        {
            _audioSource = GetComponent<AudioSource>();
            _typingSoundsQueue = new Queue<AudioClip>(_typingSounds);
        }

        public void StartTypingSounds()
        {
            AudioClip clip = _typingSoundsQueue.Dequeue();
            _typingSoundsQueue.Enqueue(clip);
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void StopTypingSounds()
        {
            _audioSource.Stop();
        }

        public void StopForSeconds(float seconds)
        {
            _audioSource.Stop();
            _audioSource.PlayDelayed(seconds);
        }
    }
}
