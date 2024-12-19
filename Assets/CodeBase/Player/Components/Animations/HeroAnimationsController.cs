using CodeBase.Player.Components.Sounds;
using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

namespace CodeBase.Player.Components.Animations
{
    public class HeroAnimationsController : MonoBehaviour, IHeroAnimationsController
    {
        #region Inspector
        [SpineAnimation]
        [SerializeField]
        private string _runAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _idleAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _runWithItemAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _idleWithItemAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _stunAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _throwAnimationName;
        #endregion

        private SkeletonAnimation _skeletonAnimation;
        private Spine.AnimationState _spineAnimationState;
        private Skeleton _skeleton;
        private IPlayerSounds _sounds;

        private bool _isRunning = false;
        private bool _hasItem = false;
        private Coroutine _throwCoroutine;

        private void Awake()
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _spineAnimationState = _skeletonAnimation.AnimationState;
            _skeleton = _skeletonAnimation.Skeleton;
            _sounds = GetComponent<IPlayerSounds>();

            _isRunning = false;
            _hasItem = false;
        }

        public void SetHasItem(bool hasItem)
        {
            if (_throwCoroutine != null)
            {
                StopCoroutine(_throwCoroutine);
            }
            UpdateSetItem(hasItem);
        }

        private void UpdateSetItem(bool hasItem)
        {
            if (_hasItem == hasItem) return;

            _hasItem = hasItem;
            if (_hasItem)
            {
                if (_isRunning)
                {
                    _spineAnimationState.SetAnimation(0, _runWithItemAnimationName, true);
                }
                else if (!_isRunning)
                {
                    _spineAnimationState.SetAnimation(0, _idleWithItemAnimationName, true);
                }
            }
            else
            {
                if (_isRunning)
                {
                    _spineAnimationState.SetAnimation(0, _runAnimationName, true);
                }
                else if (!_isRunning)
                {
                    _spineAnimationState.SetAnimation(0, _idleAnimationName, true);
                }
            }
        }

        public void Run()
        {
            TrackEntry trackEntry;
            if (_hasItem)
            {
                trackEntry = _spineAnimationState.SetAnimation(0, _runWithItemAnimationName, true);
            }
            else
            {
                trackEntry = _spineAnimationState.SetAnimation(0, _runAnimationName, true);
            }
            _sounds.StartStepSounds(trackEntry.AnimationEnd / 2);
            _isRunning = true;
        }

        public void Idle()
        {
            if (_hasItem)
            {
                _spineAnimationState.SetAnimation(0, _idleWithItemAnimationName, true);
            }
            else
            {
                _spineAnimationState.SetAnimation(0, _idleAnimationName, true);
            }
            _sounds.StopStepSounds();
            _isRunning = false;
        }

        public void Throw()
        {
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(1, _throwAnimationName, false);
            trackEntry.TimeScale = 1.25f;
            _throwCoroutine = StartCoroutine(WaitAndSetHasItemFalse(trackEntry.AnimationEnd));

            _sounds.PlayThrowClip();
        }

        private IEnumerator WaitAndSetHasItemFalse(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            UpdateSetItem(false);
        }

        public void Turn()
        {
            _skeleton.ScaleX *= -1;
        }
    }
}