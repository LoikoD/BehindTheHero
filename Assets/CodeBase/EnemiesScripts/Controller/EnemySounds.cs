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

        private const string AttackKey = "attack";
        private const string TakeWeaponDamageKey = "takeWeaponDamage";
        private const string TakeFistsDamageKey = "takeFistsDamage";

        private AudioSource _audioSource;
        private SoundQueuer _soundQueuer;

        public void Construct()
        {
            _audioSource = GetComponent<AudioSource>();

            _soundQueuer = new();
            _soundQueuer.RegisterSoundList(AttackKey, _attackSounds);
            _soundQueuer.RegisterSoundList(TakeWeaponDamageKey, _takeWeaponDamageSounds);
            _soundQueuer.RegisterSoundList(TakeFistsDamageKey, _takeFistsDamageSounds);
        }

        public void PlayAttackClip(float delay = 0)
        {
            StartCoroutine(PlayDelayedClip(_soundQueuer.GetNextSound(AttackKey), delay));

        }
        public void PlayTakeDamageFromWeaponClip(float delay = 0)
        {
            StartCoroutine(PlayDelayedClip(_soundQueuer.GetNextSound(TakeWeaponDamageKey), delay));
        }
        public void PlayTakeDamageFromFistsClip(float delay = 0)
        {
            StartCoroutine(PlayDelayedClip(_soundQueuer.GetNextSound(TakeFistsDamageKey), delay));
        }
        IEnumerator PlayDelayedClip(AudioClip clip, float delay)
        {
            yield return new WaitForSeconds(delay);
            _audioSource.PlayOneShot(clip);
        }

    }
}