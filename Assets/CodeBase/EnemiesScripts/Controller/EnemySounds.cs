using CodeBase.Logic.Utilities;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
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

        public void PlayAttackClip()
        {
            _audioSource.PlayOneShot(_soundQueuer.GetNextSound(AttackKey));

        }
        public void PlayTakeDamageClip(Weapon weapon)
        {
            if (weapon is Fists)
            {
                _audioSource.PlayOneShot(_soundQueuer.GetNextSound(TakeFistsDamageKey));
            }
            else
            {
                _audioSource.PlayOneShot(_soundQueuer.GetNextSound(TakeWeaponDamageKey));
            }
        }

    }
}