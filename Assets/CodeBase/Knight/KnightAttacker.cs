using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Knight.KnightFSM;
using CodeBase.Logic.Utilities;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightAttacker : MonoBehaviour
    {
        private KnightAnimationsController _animator;
        private List<Weapon> _weapons;
        private Weapon _currentWeapon;
        private bool _isOnCooldown;

        public void Construct(KnightAnimationsController animator, List<Weapon> weapons)
        {
            _animator = animator;
            _weapons = weapons;

            EquipFists();

            _isOnCooldown = false;
        }

        public void Attack(Transform target)
        {
            if (_isOnCooldown)
                return;
            
            Vector2 attackDirection = target.transform.position - transform.position;

            float attackAnimationTime = _animator.Attack();
            StartCoroutine(ActionAfterTime(attackAnimationTime, AfterAttackAnimation));

            AttackCd();

            _currentWeapon.Attack(transform.position, attackDirection);

        }

        public void Equip(Weapon weapon)
        {
            if (_currentWeapon.GetType() == weapon.GetType())
            {
                _currentWeapon.CurrentDurability = _currentWeapon.MaxDurability;
            }
            else
            {
                foreach (var stashed in _weapons)
                {
                    if (stashed.GetType() == weapon.GetType())
                    {
                        _currentWeapon.gameObject.SetActive(false);
                        _currentWeapon = stashed;
                        _currentWeapon.gameObject.SetActive(true);
                        _currentWeapon.CurrentDurability = _currentWeapon.MaxDurability;

                        if (_currentWeapon is Sword)
                        {
                            _animator.SetSwordSkin();
                        }
                        else
                        {
                            _animator.SetPoleaxeSkin();
                        }
                    }
                } 
            }
        }

        private void EquipFists()
        {
            foreach (var weapon in _weapons)
            {
                if (weapon is Fists)
                {
                    _currentWeapon = weapon;
                    _currentWeapon.gameObject.SetActive(true);
                    _animator.SetMeleeSkin();
                }
            }
        }

        private void AttackCd()
        {
            _isOnCooldown = true;
            StartCoroutine(ActionAfterTime(_currentWeapon.AttackCooldown, () => { _isOnCooldown = false; }));
        }

        private void AfterAttackAnimation()
        {
            if (_currentWeapon.CurrentDurability <= 0)
                EquipFists();
        }

        private IEnumerator ActionAfterTime(float secondsToWait, Action action)
        {
            yield return new WaitForSeconds(secondsToWait);

            action();
        }
    }
}