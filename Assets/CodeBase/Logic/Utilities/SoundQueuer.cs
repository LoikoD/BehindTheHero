using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Utilities
{
    public class SoundQueuer
    {
        private readonly Dictionary<SoundKeys, Queue<AudioClip>> _soundQueues = new();
        private readonly Dictionary<SoundKeys, List<AudioClip>> _soundLists = new();

        public void RegisterSoundList(SoundKeys key, List<AudioClip> sounds)
        {
            _soundLists[key] = sounds;
            _soundQueues[key] = CreateShuffledQueue(sounds);
        }

        public AudioClip GetNextSound(SoundKeys key)
        {
            if (!_soundQueues.ContainsKey(key)) return null;

            Queue<AudioClip> queue = _soundQueues[key];
            if (queue.Count == 0)
            {
                queue = CreateShuffledQueue(_soundLists[key]);
                _soundQueues[key] = queue;
            }

            return queue.Dequeue();
        }

        private Queue<AudioClip> CreateShuffledQueue(List<AudioClip> sounds)
        {
            List<AudioClip> shuffled = new(sounds);
            ShuffleList(shuffled);
            return new Queue<AudioClip>(shuffled);
        }

        private void ShuffleList(List<AudioClip> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
            }
        }
    }
}
