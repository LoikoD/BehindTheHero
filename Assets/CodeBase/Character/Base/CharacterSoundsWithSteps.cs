using CodeBase.Character.Base;
using CodeBase.Logic.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Character.Base
{
    public abstract class CharacterSoundsWithSteps : BaseSounds
    {
        [SerializeField] private protected List<AudioClip> _stepSounds;

        private protected Coroutine StepSoundsCoroutine;

        private protected override void RegisterSounds()
        {
            SoundQueuer.RegisterSoundList(SoundKeys.Step, _stepSounds);
        }

        public virtual void StartStepSounds(float interval)
        {
            StopStepSounds();
            StepSoundsCoroutine = StartCoroutine(PlayStepSoundsLoop(interval));
        }

        public virtual void StopStepSounds()
        {
            if (StepSoundsCoroutine != null)
            {
                StopCoroutine(StepSoundsCoroutine);
                StepSoundsCoroutine = null;
            }
        }

        private IEnumerator PlayStepSoundsLoop(float interval)
        {
            while (true)
            {
                PlaySound(SoundKeys.Step);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
