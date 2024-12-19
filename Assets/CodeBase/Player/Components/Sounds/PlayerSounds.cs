using Assets.CodeBase.Character.Base;
using CodeBase.Logic.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player.Components.Sounds
{
    public class PlayerSounds : CharacterSoundsWithSteps, IPlayerSounds
    {
        [SerializeField] private List<AudioClip> _throwSounds;

        private protected override void RegisterSounds()
        {
            base.RegisterSounds();
            SoundQueuer.RegisterSoundList(SoundKeys.Throw, _throwSounds);
        }

        public void PlayThrowClip() =>
            PlaySound(SoundKeys.Throw);
    }
}