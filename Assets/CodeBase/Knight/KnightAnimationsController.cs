using CodeBase.Character;
using CodeBase.Character.Interfaces;
using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightAnimationsController : MonoBehaviour, IAnimationsController
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
        private string _takeDamageAnimationName;

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
        private bool _isAttacking;
        private Coroutine _attackCoroutine;

        private readonly float PoleaxeHitDelay = 0.1334f;
        private readonly float SwordHitDelay = 0.1334f;
        private readonly float FistsFirstHitDelay = 0.0667f;
        private readonly float FistsHitInterval = 0.2001f;

        public void Construct(KnightSounds sounds)
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _spineAnimationState = _skeletonAnimation.AnimationState;
            _skeleton = _skeletonAnimation.Skeleton;
            _isAttacking = false;

            _sounds = sounds;
        }
        public void Run()
        {
            if (_isAttacking)
            {
                return;
            }
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(0, _runAnimationName, true);

            _sounds.StartStepSounds(trackEntry.AnimationEnd / 2);
        }
        public void Idle()
        {
            if (_isAttacking)
            {
                return;
            }
            _spineAnimationState.SetAnimation(0, _idleAnimationName, true);

            _sounds.StopStepSounds();
        }
        public void TakeDamage(float delay)
        {
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(1, _takeDamageAnimationName, false);
            trackEntry.Delay = delay;
            //_sounds.PlayTakeDamageClip(delay); only if has armor?
        }
        public float Die()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _isAttacking = false;
            }
            _sounds.StopStepSounds();

            float time = _spineAnimationState.SetAnimation(0, _deathAnimationName, false).AnimationEnd;

            _sounds.PlayDieClip();

            return time;
        }
        public AttackAnimationInfo Attack()
        {
            if (_isAttacking)
            {
                Debug.LogWarning("Attack animation called before previous attack animation has finished.");
                return null;
            }
            _isAttacking = true;

            float hitDelay = 0;
            float hitInterval = 0;
            TrackEntry attackEntry = null;
            if (_skeleton.Skin.Name == _meleeSkinName)
            {
                attackEntry = _spineAnimationState.SetAnimation(0, _meleeAtackAnimationName, false);
                hitDelay = FistsFirstHitDelay;
                hitInterval = FistsHitInterval;
                _sounds.PlayMeleeAttackClip(3, FistsHitInterval);
            }
            else if (_skeleton.Skin.Name == _swordSkinName)
            {
                attackEntry = _spineAnimationState.SetAnimation(0, _swordAttackAnimationName, false);
                _sounds.PlaySwordAttackClip();
                hitDelay = SwordHitDelay;
            }
            else if (_skeleton.Skin.Name == _poleaxeSkinName)
            {
                attackEntry = _spineAnimationState.SetAnimation(0, _poleaxeAttackAnimationName, false);
                _sounds.PlayPoleaxeAttackClip();
                hitDelay = PoleaxeHitDelay;
            }
            _spineAnimationState.AddAnimation(0, _idleAnimationName, true, 0);

            AttackAnimationInfo animInfo = new(attackEntry.AnimationEnd, hitDelay, hitInterval);

            _attackCoroutine = StartCoroutine(StopAttackAfterTime(animInfo.Duration));

            return animInfo;
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