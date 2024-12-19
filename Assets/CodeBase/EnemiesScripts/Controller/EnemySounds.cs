using CodeBase.Character.Base;
using CodeBase.Logic.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.EnemiesScripts.Controller
{
    public class EnemySounds : BaseSounds
    {
        [SerializeField] private List<AudioClip> _attackSounds;
        [SerializeField] private List<AudioClip> _takeWeaponDamageSounds;
        [SerializeField] private List<AudioClip> _takeFistsDamageSounds;

        private protected override void RegisterSounds()
        {
            SoundQueuer.RegisterSoundList(SoundKeys.Attack, _attackSounds);
            SoundQueuer.RegisterSoundList(SoundKeys.TakeWeaponDamage, _takeWeaponDamageSounds);
            SoundQueuer.RegisterSoundList(SoundKeys.TakeFistsDamage, _takeFistsDamageSounds);
        }

        public void PlayAttackClip(float delay = 0) =>
            StartCoroutine(PlayDelayedSound(SoundKeys.Attack, delay));

        public void PlayTakeDamageFromWeaponClip(float delay = 0) =>
            StartCoroutine(PlayDelayedSound(SoundKeys.TakeWeaponDamage, delay));

        public void PlayTakeDamageFromFistsClip(float delay = 0) =>
            StartCoroutine(PlayDelayedSound(SoundKeys.TakeFistsDamage, delay));
    }
}