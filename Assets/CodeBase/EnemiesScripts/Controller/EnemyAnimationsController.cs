using CodeBase.Character.Base;
using CodeBase.Character;
using CodeBase.Character.Interfaces;
using UnityEngine;
using CodeBase.Character.Data;

namespace CodeBase.EnemiesScripts.Controller
{
    public class EnemyAnimationsController : BaseAnimationsController, IAnimationsController
    {
        [SerializeField] private EnemyAnimationNames _animations;
        [SerializeField] private EnemyAttackAnimationData _attackData;

        private EnemySounds _sounds;

        public void Construct(EnemySounds sounds)
        {
            _sounds = sounds;
        }

        public void Run() => PlayAnimation(_animations.Run, 0, true);

        public void Idle() => PlayAnimation(_animations.Idle, 0, true);

        public float Die()
        {
            var trackEntry = PlayAnimation(_animations.Death, 0, false);
            return trackEntry.AnimationEnd;
        }

        public AttackAnimationInfo Attack()
        {
            var trackEntry = PlayAnimation(_animations.Attack, 0, false);
            AddAnimation(_animations.Idle, 0, true, 0);
            _sounds.PlayAttackClip(_attackData.HitDelay * 0.8f);
            return new AttackAnimationInfo(trackEntry.AnimationEnd, _attackData.HitDelay, 0);
        }

        public void TakeDamage(float delay)
        {
            var trackEntry = PlayAnimation(_animations.TakeDamage, 1, false);
            trackEntry.Delay = delay;
        }

        public void TakeDamageFromWeapon(float delay)
        {
            TakeDamage(delay);
            _sounds.PlayTakeDamageFromWeaponClip(delay);
        }

        public void TakeDamageFromFists(float delay, float interval)
        {
            var trackEntry = PlayAnimation(_animations.TakeDamage, 1, false);
            trackEntry.Delay = delay;
            AddAnimation(_animations.TakeDamage, 1, false, interval);
            AddAnimation(_animations.TakeDamage, 1, false, interval);

            for (int i = 0; i < 3; i++)
                _sounds.PlayTakeDamageFromFistsClip(delay + interval * i);
        }
    }
}