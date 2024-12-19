using CodeBase.Logic.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI
{
    public class SliderSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _sliderSounds;

        private SoundQueuer _soundQueuer;

        public void Awake()
        {
            _soundQueuer = new();
            _soundQueuer.RegisterSoundList(SoundKeys.Slider, _sliderSounds);
        }

        public void PlaySliderSound()
        {
            _audioSource.PlayOneShot(_soundQueuer.GetNextSound(SoundKeys.Slider));
        }
    }
}
