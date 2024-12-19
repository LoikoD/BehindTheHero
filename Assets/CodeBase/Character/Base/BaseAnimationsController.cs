using Spine.Unity;
using Spine;
using UnityEngine;

namespace CodeBase.Character.Base
{
    public abstract class BaseAnimationsController : MonoBehaviour
    {
        private protected SkeletonAnimation SkeletonAnimation { get; private set; }
        private protected Spine.AnimationState SpineAnimationState { get; private set; }
        private protected Skeleton Skeleton { get; private set; }

        private protected virtual void Awake()
        {
            SkeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            SpineAnimationState = SkeletonAnimation.AnimationState;
            Skeleton = SkeletonAnimation.Skeleton;
        }

        public virtual void Turn() =>
            Skeleton.ScaleX *= -1;

        private protected TrackEntry PlayAnimation(string animationName, int track = 0, bool loop = false)
        {
            return SpineAnimationState.SetAnimation(track, animationName, loop);
        }

        private protected TrackEntry AddAnimation(string animationName, int track = 0, bool loop = false, float delay = 0)
        {
            return SpineAnimationState.AddAnimation(track, animationName, loop, delay);
        }

        private protected void SetSkin(string skinName)
        {
            Skeleton.SetSkin(skinName);
            Skeleton.SetSlotsToSetupPose();
        }
    }
}
