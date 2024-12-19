using CodeBase.Character;
using CodeBase.Character.Base;
using CodeBase.Character.Data;
using CodeBase.Character.Interfaces;
using Spine;
using System.Collections;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightAnimationsController : BaseAnimationsController, IAnimationsController
    {
        [SerializeField] private KnightAnimationNames _animations;
        [SerializeField] private KnightSkinNames _skins;
        [SerializeField] private KnightAttackAnimationData _attackData;

        private KnightSounds _sounds;

        private bool _isAttacking;
        private Coroutine _attackCoroutine;

        public void Construct(KnightSounds sounds)
        {
            _sounds = sounds;
            _isAttacking = false;
        }

        public void Run()
        {
            if (_isAttacking) return;

            var trackEntry = PlayAnimation(_animations.Run, 0, true);
            _sounds.StartStepSounds(trackEntry.AnimationEnd / 2);
        }

        public void Idle()
        {
            if (_isAttacking) return;

            PlayAnimation(_animations.Idle, 0, true);
            _sounds.StopStepSounds();
        }

        public void TakeDamage(float delay)
        {
            var trackEntry = PlayAnimation(_animations.TakeDamage, 1, false);
            trackEntry.Delay = delay;
            //_sounds.PlayTakeDamageClip(delay); only if has armor?
        }

        public float Die()
        {
            StopAttackIfNeeded();
            _sounds.StopStepSounds();

            var trackEntry = PlayAnimation(_animations.Death, 0, false);
            _sounds.PlayDieClip();

            return trackEntry.AnimationEnd;
        }

        public AttackAnimationInfo Attack()
        {
            if (_isAttacking)
            {
                Debug.LogWarning("Attack animation called before previous attack animation has finished.");
                return null;
            }

            _isAttacking = true;
            var (animationName, hitDelay, hitInterval) = GetAttackParameters();

            var attackEntry = PlayAnimation(animationName, 0, false);
            AddAnimation(_animations.Idle, 0, true, 0);

            PlayAttackSound(hitInterval);

            var animInfo = new AttackAnimationInfo(attackEntry.AnimationEnd, hitDelay, hitInterval);
            _attackCoroutine = StartCoroutine(StopAttackAfterTime(animInfo.Duration));

            return animInfo;
        }

        public void SetMeleeSkin() => SetSkin(_skins.Melee);
        public void SetSwordSkin() => SetSkin(_skins.Sword);
        public void SetPoleaxeSkin() => SetSkin(_skins.Poleaxe);

        private (string animation, float delay, float interval) GetAttackParameters() =>
            Skeleton.Skin.Name switch
            {
                var name when name == _skins.Melee =>
                    (_animations.MeleeAttack, _attackData.FistsFirstHitDelay, _attackData.FistsHitInterval),
                var name when name == _skins.Sword =>
                    (_animations.SwordAttack, _attackData.SwordHitDelay, 0),
                var name when name == _skins.Poleaxe =>
                    (_animations.PoleaxeAttack, _attackData.PoleaxeHitDelay, 0),
                _ => throw new System.ArgumentException($"Unknown skin: {Skeleton.Skin.Name}")
            };

        private void PlayAttackSound(float interval)
        {
            switch (Skeleton.Skin.Name)
            {
                case var name when name == _skins.Melee:
                    _sounds.PlayMeleeAttackClip(3, interval);
                    break;
                case var name when name == _skins.Sword:
                    _sounds.PlaySwordAttackClip();
                    break;
                case var name when name == _skins.Poleaxe:
                    _sounds.PlayPoleaxeAttackClip();
                    break;
            }
        }

        private IEnumerator StopAttackAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            _isAttacking = false;
        }

        private void StopAttackIfNeeded()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _isAttacking = false;
            }
        }
    }
}