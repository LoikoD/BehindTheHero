using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightAnimationsController : MonoBehaviour
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
        private string _meleeAtackAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _swordAttackAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _poleaxeAttackAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _takeDamamgeAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _stunAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _deathAnimationName;

        [SpineSkin]
        [SerializeField]
        private string _meleeSkinName;

        [SpineSkin]
        [SerializeField]
        private string _swordSkinName;

        [SpineSkin]
        [SerializeField]
        private string _poleaxeSkinName;
        #endregion

        private SkeletonAnimation _skeletonAnimation;
        private Spine.AnimationState _spineAnimationState;
        private Skeleton _skeleton;
        private KnightSounds _sounds;
        private bool _isRunning;
        private bool _isAttacking;
        private Coroutine _attackCoroutine;


        void Awake()
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _spineAnimationState = _skeletonAnimation.AnimationState;
            _skeleton = _skeletonAnimation.Skeleton;
            _isRunning = false;

            _sounds = GetComponent<KnightSounds>();
        }
        public void Run()
        {
            if (_isAttacking)
            {
                return;
            }
            _isRunning = true;
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(0, _runAnimationName, true);

            _sounds.StartStepSounds(trackEntry.AnimationEnd / 2);
        }
        public void Idle()
        {
            if (_isAttacking)
            {
                return;
            }
            _isRunning = false;
            _spineAnimationState.SetAnimation(0, _idleAnimationName, true);

            _sounds.StopStepSounds();
        }
        public void TakeDamage()
        {
            if (_isAttacking)
            {
                return;
            }
            _spineAnimationState.SetAnimation(1, _takeDamamgeAnimationName, false);
        }
        public float Die()
        {
            float time = _spineAnimationState.SetAnimation(0, _deathAnimationName, false).AnimationEnd;

            _sounds.PlayDieClip();

            return time;
        }
        public void Attack()
        {
            if (_isAttacking)
            {
                return;
            }
            _isAttacking = true;

            TrackEntry attackEntry = null;
            _sounds.StopStepSounds();
            if (_skeleton.Skin.Name == _meleeSkinName)
            {
                attackEntry = _spineAnimationState.SetAnimation(0, _meleeAtackAnimationName, false);
                _sounds.PlayMeleeAttackClip();
            }
            else if (_skeleton.Skin.Name == _swordSkinName)
            {
                attackEntry = _spineAnimationState.SetAnimation(0, _swordAttackAnimationName, false);
                _sounds.PlaySwordAttackClip();
            }
            else if (_skeleton.Skin.Name == _poleaxeSkinName)
            {
                attackEntry = _spineAnimationState.SetAnimation(0, _poleaxeAttackAnimationName, false);
                _sounds.PlayPoleaxeAttackClip();
            }

            if (_isRunning)
            {
                TrackEntry trackEntry = _spineAnimationState.AddAnimation(0, _runAnimationName, true, 0);
                _sounds.StartStepSounds(trackEntry.AnimationEnd / 2);
            }
            else
            {
                _spineAnimationState.AddAnimation(0, _idleAnimationName, true, 0);
            }

            _attackCoroutine = StartCoroutine(StopAttackAfterTime(attackEntry.AnimationEnd));
        }

        private IEnumerator StopAttackAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            _isAttacking = false;

        }

        public void Turn()
        {
            _skeleton.ScaleX *= -1;
        }

        public void SetMeleeSkin()
        {
            _skeleton.SetSkin(_meleeSkinName);
            _skeleton.SetSlotsToSetupPose();
        }
        public void SetSwordSkin()
        {
            _skeleton.SetSkin(_swordSkinName);
            _skeleton.SetSlotsToSetupPose();
        }
        public void SetPoleaxeSkin()
        {
            _skeleton.SetSkin(_poleaxeSkinName);
            _skeleton.SetSlotsToSetupPose();
        }
    }
}