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
    private AudioSource _audioSource;
    private Coroutine _stepCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMeleeAttackClip()
    {
        int clipIndex = Random.Range(0, _meleeAttackSounds.Count);
        _audioSource.PlayOneShot(_meleeAttackSounds[clipIndex]);

    }

    public void PlaySwordAttackClip()
    {
        int clipIndex = Random.Range(0, _swordAttackSounds.Count);
        _audioSource.PlayOneShot(_swordAttackSounds[clipIndex]);

    }

    public void PlayPoleaxeAttackClip()
    {
        int clipIndex = Random.Range(0, _poleaxeAttackSounds.Count);
        _audioSource.PlayOneShot(_poleaxeAttackSounds[clipIndex]);

    }

    public void PlayDieClip()
    {
        int clipIndex = Random.Range(0, _dieSounds.Count);
        _audioSource.PlayOneShot(_dieSounds[clipIndex]);

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
