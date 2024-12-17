using CodeBase.Knight.KnightFSM;
using CodeBase.ThrowableObjects;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightPickupObjects : MonoBehaviour
    {
        private KnightAttacker _attacker;
        private KnightStateMachine _stateMachine;

        private CircleCollider2D _collider;

        public void Construct(KnightStateMachine stateMachine, KnightAttacker attacker, float pickupRadius)
        {
            _stateMachine = stateMachine;
            _attacker = attacker;
            _collider = GetComponent<CircleCollider2D>();
            _collider.radius = pickupRadius;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_stateMachine.HasDied)
                return;

            if (other.TryGetComponent(out IEquippable equipment))
            {
                if (equipment.CanBeEquipped)
                {
                    if (other.TryGetComponent(out Weapon weapon))
                    {
                        _attacker.Equip(weapon);
                        equipment.AfterEquipped();
                    }

                }
            }
        }
    }
}