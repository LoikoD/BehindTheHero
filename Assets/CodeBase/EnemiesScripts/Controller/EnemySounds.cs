using CodeBase.Logic.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.EnemiesScripts.Controller
{
    public class EnemySounds : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _attackSounds;
        [SerializeField] private List<AudioClip> _takeDamageSounds;
        [SerializeField] private List<AudioClip> _takeWeaponDamageSounds;
        [SerializeField] private List<AudioClip> _takeFistsDamageSounds;

        private AudioSource _audioSource;
        private SoundQueuer _soundQueuer;

        public void Construct()
        {
            _audioSource = GetComponent<AudioSource>();

            _soundQueuer = new();
            _soundQueuer.RegisterSoundList(SoundKeys.Attack, _attackSounds);
            _soundQueuer.RegisterSoundList(SoundKeys.TakeWeaponDamage, _takeWeaponDamageSounds);
            _soundQueuer.RegisterSoundList(SoundKeys.TakeFistsDamage, _takeFistsDamageSounds);
        }

        public void PlayAttackClip(float delay = 0)
        {
            StartCoroutine(PlayDelayedClip(_soundQueuer.GetNextSound(SoundKeys.Attack), delay));

        }
        public void PlayTakeDamageFromWeaponClip(float delay = 0)
        {
            StartCoroutine(PlayDelayedClip(_soundQueuer.GetNextSound(SoundKeys.TakeWeaponDamage), delay));
        }
        public void PlayTakeDamageFromFistsClip(float delay = 0)
        {
            StartCoroutine(PlayDelayedClip(_soundQueuer.GetNextSound(SoundKeys.TakeFistsDamage), delay));
        }
        IEnumerator PlayDelayedClip(AudioClip clip, float delay)
        {
            yield return new WaitForSeconds(delay);
            _audioSource.PlayOneShot(clip);
        }

    }
}