using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.EnemiesScripts.Controller
{
    public class EnemyAnimationsController : MonoBehaviour
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


        void Awake()
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _spineAnimationState = _skeletonAnimation.AnimationState;
            _skeleton = _skeletonAnimation.Skeleton;
            _sounds = GetComponent<EnemySounds>();
        }
        public void Run()
        {
            _spineAnimationState.SetAnimation(0, _runAnimationName, true);
        }
        public void TakeDamage()
        {
            _spineAnimationState.SetAnimation(1, _takeDamamgeAnimationName, false);
            _sounds.PlayTakeDamageClip();
        }
        public float Die()
        {
            TrackEntry trackEntry = _spineAnimationState.SetAnimation(0, _deathAnimationName, false);
            return trackEntry.AnimationEnd;
        }
        public void Attack()
        {
            _spineAnimationState.SetAnimation(0, _atackAnimationName, false);
            _spineAnimationState.AddEmptyAnimation(0, 0.2f, 0);
            _sounds.PlayAttackClip();
        }
        public void Turn()
        {
            _skeleton.ScaleX *= -1;
        }

        public enum AttackType
        {
            Weapon,
            Fists
        }
    }
}