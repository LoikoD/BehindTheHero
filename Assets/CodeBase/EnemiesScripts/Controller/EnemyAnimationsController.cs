using CodeBase.Character.Interfaces;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using Spine;
using Spine.Unity;
using System.Collections;
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
        private string _takeDamamgeAnimationName;

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
            _spineAnimationState.SetAnimation(0, _stunAnimationName, false);
        }
        public void TakeDamage(Weapon weapon) 
        {
            _spineAnimationState.SetAnimation(1, _takeDamamgeAnimationName, false);
            _sounds.PlayTakeDamageClip(weapon);
        }
        public void TakeDamageFromFists(Weapon weapon, float attackAnimation)
        {
            StartCoroutine(TakeDamageXTimesRoutine(weapon, 3, attackAnimation / 3));
        }
        public float Die()
        {
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(0, _deathAnimationName, false);
            return trackEntry.AnimationEnd;
        }
        public float Attack()
        {
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(0, _atackAnimationName, false);
            _spineAnimationState.AddAnimation(0, _stunAnimationName, true, 0);
            _sounds.PlayAttackClip();
            return trackEntry.AnimationEnd;
        }
        public void Turn()
        {
            _skeleton.ScaleX *= -1;
        }
        private IEnumerator TakeDamageXTimesRoutine(Weapon weapon, int times, float interval)
        {
            for (int i = 0; i < times; ++i)
            {
                TakeDamage(weapon);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}