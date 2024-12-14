using CodeBase.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI
{
    public class SliderSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _sliderSounds;

        private const string SliderSoundKey = "slider";

        private SoundQueuer _soundQueuer;

        public void Awake()
        {
            _soundQueuer = new();
            _soundQueuer.RegisterSoundList(SliderSoundKey, _sliderSounds);
        }

        public void PlaySliderSound()
        {
            _audioSource.PlayOneShot(_soundQueuer.GetNextSound(SliderSoundKey));
        }
    }
}
