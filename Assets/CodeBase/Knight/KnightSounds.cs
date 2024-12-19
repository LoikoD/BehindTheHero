using Assets.CodeBase.Character.Base;
using CodeBase.Logic.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class KnightSounds : CharacterSoundsWithSteps
{
    [SerializeField] private List<AudioClip> _meleeAttackSounds;
    [SerializeField] private List<AudioClip> _swordAttackSounds;
    [SerializeField] private List<AudioClip> _poleaxeAttackSounds;
    [SerializeField] private List<AudioClip> _dieSounds;
    [SerializeField] private List<AudioClip> _takeDamageSounds;

    private protected override void RegisterSounds()
    {
        base.RegisterSounds();
        SoundQueuer.RegisterSoundList(SoundKeys.MeleeAttack, _meleeAttackSounds);
        SoundQueuer.RegisterSoundList(SoundKeys.SwordAttack, _swordAttackSounds);
        SoundQueuer.RegisterSoundList(SoundKeys.PoleaxeAttack, _poleaxeAttackSounds);
        SoundQueuer.RegisterSoundList(SoundKeys.Die, _dieSounds);
        SoundQueuer.RegisterSoundList(SoundKeys.TakeDamage, _takeDamageSounds);
    }

    public void PlayMeleeAttackClip(int playTimes, float interval) =>
        StartCoroutine(PlaySoundMultipleTimes(SoundKeys.MeleeAttack, playTimes, interval));

    public void PlaySwordAttackClip() =>
        PlaySound(SoundKeys.SwordAttack);

    public void PlayPoleaxeAttackClip() =>
        PlaySound(SoundKeys.PoleaxeAttack);

    public void PlayDieClip() =>
        PlaySound(SoundKeys.Die);

    public void PlayTakeDamageClip(float delay = 0) =>
        StartCoroutine(PlayDelayedSound(SoundKeys.TakeDamage, delay));
}
