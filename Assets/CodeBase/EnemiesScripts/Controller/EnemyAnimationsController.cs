using CodeBase.Character;
using CodeBase.Character.Interfaces;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace CodeBase.EnemiesScripts.Controller
{
    public class EnemyAnimationsController : MonoBehaviour, IAnimationsController
    {
        #region Inspector
        [SpineAnimation]
        [SerializeField]
        private string _runAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _atackAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _takeDamageAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _deathAnimationName;

        [SpineAnimation]
        [SerializeField]
        private string _stunAnimationName;
        #endregion

        private SkeletonAnimation _skeletonAnimation;
        private Spine.AnimationState _spineAnimationState;
        private Skeleton _skeleton;
        private EnemySounds _sounds;

        private const float HitDelay = 0.3332f;

        public void Construct(EnemySounds sounds)
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _spineAnimationState = _skeletonAnimation.AnimationState;
            _skeleton = _skeletonAnimation.Skeleton;
            _sounds = sounds;
        }
        public void Run()
        {
            _spineAnimationState.SetAnimation(0, _runAnimationName, true);
        }
        public void Idle()
        {
            _spineAnimationState.SetAnimation(0, _stunAnimationName, true);
        }
        public float Die()
        {
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(0, _deathAnimationName, false);
            return trackEntry.AnimationEnd;
        }
        public AttackAnimationInfo Attack()
        {
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(0, _atackAnimationName, false);
            _spineAnimationState.AddAnimation(0, _stunAnimationName, true, 0);
            _sounds.PlayAttackClip(HitDelay * 0.8f);
            return new AttackAnimationInfo(trackEntry.AnimationEnd, HitDelay, 0);
        }
        public void Turn()
        {
            _skeleton.ScaleX *= -1;
        }
        public void TakeDamage(float delay)
        {
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(1, _takeDamageAnimationName, false);
            trackEntry.Delay = delay;
        }
        public void TakeDamageFromWeapon(float delay)
        {
            TakeDamage(delay);
            _sounds.PlayTakeDamageFromWeaponClip(delay);
        }
        public void TakeDamageFromFists(float delay, float interval)
        {
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(1, _takeDamageAnimationName, false);
            trackEntry.Delay = delay;
            _spineAnimationState.AddAnimation(1, _takeDamageAnimationName, false, interval);
            _spineAnimationState.AddAnimation(1, _takeDamageAnimationName, false, interval);

            _sounds.PlayTakeDamageFromFistsClip(delay);
            _sounds.PlayTakeDamageFromFistsClip(delay + interval);
            _sounds.PlayTakeDamageFromFistsClip(delay + interval * 2);
        }
    }
}