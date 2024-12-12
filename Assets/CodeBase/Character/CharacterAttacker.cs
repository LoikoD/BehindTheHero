﻿using CodeBase.Character.CharacterFSM;
using CodeBase.Character.Interfaces;
using CodeBase.Knight;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
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

            AttackAnimation();

            AttackCd();

            DoAttack(target);

        }

        internal abstract void DoAttack(Transform target);

        internal virtual void AttackAnimation()
        {
            _animator.Attack();
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
