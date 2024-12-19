using CodeBase.Character.Base;
using CodeBase.Character.Data;
using CodeBase.Player.Components.Sounds;
using System.Collections;
using UnityEngine;

namespace CodeBase.Player.Components.Animations
{
    public class HeroAnimationsController : BaseAnimationsController, IHeroAnimationsController
    {
        [SerializeField] private HeroAnimationNames _animations;

        private IPlayerSounds _sounds;

        private bool _isRunning = false;
        private bool _hasItem = false;
        private Coroutine _throwCoroutine;

        private protected override void Awake()
        {
            base.Awake();
            _sounds = GetComponent<IPlayerSounds>();

            _isRunning = false;
            _hasItem = false;
        }

        public void SetHasItem(bool hasItem)
        {
            if (_throwCoroutine != null)
            {
                StopCoroutine(_throwCoroutine);
                _throwCoroutine = null;
            }
            UpdateSetItem(hasItem);
        }

        private void UpdateSetItem(bool hasItem)
        {
            if (_hasItem == hasItem) return;

            _hasItem = hasItem;
            string animationName = GetCurrentAnimationName();
            PlayAnimation(animationName, 0, true);
        }

        private string GetCurrentAnimationName() =>
            (_hasItem, _isRunning) switch
            {
                (true, true) => _animations.RunWithItem,
                (true, false) => _animations.IdleWithItem,
                (false, true) => _animations.Run,
                (false, false) => _animations.Idle
            };

        public void Run()
        {
            _isRunning = true;
            var trackEntry = PlayAnimation(GetCurrentAnimationName(), 0, true);
            _sounds.StartStepSounds(trackEntry.AnimationEnd / 2);
        }

        public void Idle()
        {
            _isRunning = false;
            PlayAnimation(GetCurrentAnimationName(), 0, true);
            _sounds.StopStepSounds();
        }

        public void Throw()
        {
            var trackEntry = PlayAnimation(_animations.Throw, 1, false);
            trackEntry.TimeScale = 1.25f;
            _throwCoroutine = StartCoroutine(WaitAndSetHasItemFalse(trackEntry.AnimationEnd));
            _sounds.PlayThrowClip();
        }

        private IEnumerator WaitAndSetHasItemFalse(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            UpdateSetItem(false);
        }
    }
}