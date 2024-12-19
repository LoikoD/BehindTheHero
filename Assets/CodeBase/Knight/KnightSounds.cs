using CodeBase.Logic.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSounds : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _stepSounds;
    [SerializeField] private List<AudioClip> _meleeAttackSounds;
    [SerializeField] private List<AudioClip> _swordAttackSounds;
    [SerializeField] private List<AudioClip> _poleaxeAttackSounds;
    [SerializeField] private List<AudioClip> _dieSounds;
    [SerializeField] private List<AudioClip> _takeDamageSounds;

    private SoundQueuer _soundQueuer;
    private AudioSource _audioSource;
    private Coroutine _stepCoroutine;

    public void Construct()
    {
        _audioSource = GetComponent<AudioSource>();

        _soundQueuer = new();
        _soundQueuer.RegisterSoundList(SoundKeys.Step, _stepSounds);
        _soundQueuer.RegisterSoundList(SoundKeys.MeleeAttack, _meleeAttackSounds);
        _soundQueuer.RegisterSoundList(SoundKeys.SwordAttack, _swordAttackSounds);
        _soundQueuer.RegisterSoundList(SoundKeys.PoleaxeAttack, _poleaxeAttackSounds);
        _soundQueuer.RegisterSoundList(SoundKeys.Die, _dieSounds);
        _soundQueuer.RegisterSoundList(SoundKeys.TakeDamage, _takeDamageSounds);
    }

    public void PlayMeleeAttackClip(int playTimes, float interval)
    {
        StartCoroutine(PlayXNumberTimes(SoundKeys.MeleeAttack, playTimes, interval));
    }

    public void PlaySwordAttackClip()
    {
        _audioSource.PlayOneShot(_soundQueuer.GetNextSound(SoundKeys.SwordAttack));
    }

    public void PlayPoleaxeAttackClip()
    {
        _audioSource.PlayOneShot(_soundQueuer.GetNextSound(SoundKeys.PoleaxeAttack));
    }

    public void PlayDieClip()
    {
        _audioSource.PlayOneShot(_soundQueuer.GetNextSound(SoundKeys.Die));
    }
    public void PlayTakeDamageClip(float delay = 0)
    {
        StartCoroutine(PlayDelayedClip(_soundQueuer.GetNextSound(SoundKeys.TakeDamage), delay));
    }
    IEnumerator PlayDelayedClip(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        _audioSource.PlayOneShot(clip);
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

    private IEnumerator LoopStepSounds(float interval)
    {
        while (true)
        {
            _audioSource.PlayOneShot(_soundQueuer.GetNextSound(SoundKeys.Step));
            yield return new WaitForSeconds(interval);
        }
    }
    private IEnumerator PlayXNumberTimes(SoundKeys soundKey, int playTimes, float interval)
    {
        for (int i = 0; i < playTimes; ++i)
        {
            _audioSource.PlayOneShot(_soundQueuer.GetNextSound(soundKey));
            yield return new WaitForSeconds(interval);
        }
    }
}
