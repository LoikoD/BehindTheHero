using CodeBase.Character.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Character
{
    public abstract class CharacterAttacker : MonoBehaviour, IAttacker
    {
        internal IAnimationsController _animator;
        internal bool _isOnCooldown;

        internal virtual float AttackCooldown { get; }

        internal void Construct(IAnimationsController animator)
        {
            _animator = animator;

            _isOnCooldown = false;
        }

        public void Attack(Transform target)
        {
            if (_isOnCooldown)
                return;

            AttackAnimationInfo animInfo = AttackAnimation();

            AttackCd();

            DoAttack(target, animInfo);

        }

        internal abstract void DoAttack(Transform target, AttackAnimationInfo animInfo);

        internal virtual AttackAnimationInfo AttackAnimation()
        {
            return _animator.Attack();
        }

        private void AttackCd()
        {
            _isOnCooldown = true;
            StartCoroutine(ActionAfterTime(AttackCooldown, () => { _isOnCooldown = false; }));
        }

        internal IEnumerator ActionAfterTime(float secondsToWait, Action action)
        {
            yield return new WaitForSeconds(secondsToWait);

            action();
        }
    }
}
