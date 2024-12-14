using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI
{
    public class UISoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _buttonClickSound;

        public void PlayButtonClickSound()
        {
            _audioSource.PlayOneShot(_buttonClickSound);
        }
    }
}
