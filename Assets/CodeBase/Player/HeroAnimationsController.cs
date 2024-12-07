using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player
{
    public class HeroAnimationsController : MonoBehaviour
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
        private PlayerSounds _sounds;

        private bool _hasItem = false;
        private Coroutine _throwCoroutine;


        void Awake()
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _spineAnimationState = _skeletonAnimation.AnimationState;
            _skeleton = _skeletonAnimation.Skeleton;
            _sounds = GetComponent<PlayerSounds>();
        }

        public void SetHasItem(bool hasItem)
        {
            if (_throwCoroutine != null)
            {
                StopCoroutine(_throwCoroutine);
                _hasItem = false;
            }
            UpdateSetItem(hasItem);
        }

        private void UpdateSetItem(bool hasItem)
        {
            if (_hasItem == hasItem) return;

            string curAnimName = _spineAnimationState.GetCurrent(0).Animation.Name;
            _hasItem = hasItem;
            if (_hasItem)
            {
                if (curAnimName == _runAnimationName)
                {
                    _spineAnimationState.SetAnimation(0, _runWithItemAnimationName, true);
                }
                else if (curAnimName == _idleAnimationName)
                {
                    _spineAnimationState.SetAnimation(0, _idleWithItemAnimationName, true);
                }
            }
            else
            {
                if (curAnimName == _runWithItemAnimationName)
                {
                    _spineAnimationState.SetAnimation(0, _runAnimationName, true);
                }
                else if (curAnimName == _idleWithItemAnimationName)
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

        public void Idle(bool hasItem)
        {
            if (hasItem)
            {
                _spineAnimationState.SetAnimation(0, _idleWithItemAnimationName, true);
            }
            else
            {
                _spineAnimationState.SetAnimation(0, _idleAnimationName, true);
            }
        }
    }
}