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

    private const string StepKey = "step";
    private const string MeleeAttackKey = "meleeAttack";
    private const string SwordAttackKey = "swordAttack";
    private const string PoleaxeAttackKey = "poleaxeAttack";
    private const string DieKey = "die";
    private const string TakeDamageKey = "takeDamage";

    private SoundQueuer _soundQueuer;
    private AudioSource _audioSource;
    private Coroutine _stepCoroutine;

    public void Construct()
    {
        _audioSource = GetComponent<AudioSource>();

        _soundQueuer = new();
        _soundQueuer.RegisterSoundList(StepKey, _stepSounds);
        _soundQueuer.RegisterSoundList(MeleeAttackKey, _meleeAttackSounds);
        _soundQueuer.RegisterSoundList(SwordAttackKey, _swordAttackSounds);
        _soundQueuer.RegisterSoundList(PoleaxeAttackKey, _poleaxeAttackSounds);
        _soundQueuer.RegisterSoundList(DieKey, _dieSounds);
        _soundQueuer.RegisterSoundList(TakeDamageKey, _takeDamageSounds);
    }

    public void PlayMeleeAttackClip(int playTimes, float interval)
    {
        StartCoroutine(PlayXNumberTimes(MeleeAttackKey, playTimes, interval));
    }

    public void PlaySwordAttackClip()
    {
        _audioSource.PlayOneShot(_soundQueuer.GetNextSound(SwordAttackKey));
    }

    public void PlayPoleaxeAttackClip()
    {
        _audioSource.PlayOneShot(_soundQueuer.GetNextSound(PoleaxeAttackKey));
    }

    public void PlayDieClip()
    {
        _audioSource.PlayOneShot(_soundQueuer.GetNextSound(DieKey));
    }
    public void PlayTakeDamageClip(float delay = 0)
    {
        StartCoroutine(PlayDelayedClip(_soundQueuer.GetNextSound(TakeDamageKey), delay));
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
            _audioSource.PlayOneShot(_soundQueuer.GetNextSound(StepKey));
            yield return new WaitForSeconds(interval);
        }
    }
    private IEnumerator PlayXNumberTimes(string soundKey, int playTimes, float interval)
    {
        for (int i = 0; i < playTimes; ++i)
        {
            _audioSource.PlayOneShot(_soundQueuer.GetNextSound(soundKey));
            yield return new WaitForSeconds(interval);
        }
    }
}
