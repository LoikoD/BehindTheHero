using System.Collections;
using System.Collections.Generic;
using CodeBase.Logic.Utilities;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightAttacker : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _weapons;
        [SerializeField] private KnightAnimationsController _animator;

        private Weapon _currentWeapon;
        private Fists _fists;
        private HorizontalDirection _horizontalDirection;
        private bool _isOnCooldown;

        private void Start()
        {
            EquipFists();
            _fists = (Fists)_currentWeapon;
            _horizontalDirection = HorizontalDirection.Right;
        }

        public void Attack(Transform target)
        {
            if (_isOnCooldown)
                return;
            
            if (_currentWeapon.CurrentDurability <= 0 && _currentWeapon != _fists)
                EquipFists();
            
            Vector2 attackDirection = target.transform.position - transform.position;

            _animator.Attack();
            StartCoroutine(AttackCdCoroutine());
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
        private IEnumerator AttackCdCoroutine()
        {
            _isOnCooldown = true;

            yield return new WaitForSeconds(_currentWeapon.AttackCooldown);

            _isOnCooldown = false;
        }
    }
}