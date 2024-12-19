using System.Collections.Generic;
using CodeBase.Character;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightAttacker : CharacterAttacker
    {
        private List<Weapon> _weapons;
        private Weapon _currentWeapon;

        internal override float AttackCooldown => _currentWeapon.AttackCooldown;

        public void Construct(KnightAnimationsController animator, List<Weapon> weapons)
        {
            base.Construct(animator);
            _weapons = weapons;

            EquipFists();
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

                        if (_animator is KnightAnimationsController knightAnimator)
                        {
                            if (_currentWeapon is Sword)
                            {
                                knightAnimator.SetSwordSkin();
                            }
                            else
                            {
                                knightAnimator.SetPoleaxeSkin();
                            }
                        }
                    }
                } 
            }
        }

        internal override void DoAttack(Transform target, float hitInterval)
        {
            Vector2 attackDirection = target.position - transform.position;
            _currentWeapon.Attack(attackDirection, hitInterval);
        }

        internal override AttackAnimationInfo AttackAnimation()
        {
            AttackAnimationInfo animInfo = _animator.Attack();
            StartCoroutine(ActionAfterTime(animInfo.Duration, AfterAttackAnimation));
            return animInfo;
        }

        private void AfterAttackAnimation()
        {
            if (_currentWeapon.CurrentDurability <= 0)
                EquipFists();
        }

        private void EquipFists()
        {
            foreach (var weapon in _weapons)
            {
                if (weapon is Fists)
                {
                    _currentWeapon = weapon;
                    _currentWeapon.gameObject.SetActive(true);

                    if (_animator is KnightAnimationsController knightAnimator)
                    {
                        knightAnimator.SetMeleeSkin();
                    }
                }
            }
        }
    }
}