using CodeBase.Knight.KnightFSM;
using CodeBase.ThrowableObjects;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using UnityEngine;

namespace CodeBase.Knight
{
    public class KnightPickupObjects : MonoBehaviour
    {
        [SerializeField] private KnightAttacker _attacker;
        private KnightStateMachine _stateMachine;

        private CircleCollider2D _collider;

        public void Construct(KnightStateMachine stateMachine, float pickupRadius)
        {
            _stateMachine = stateMachine;
            _collider = GetComponent<CircleCollider2D>();
            _collider.radius = pickupRadius;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_stateMachine.HasDied)
                return;

            if (other.TryGetComponent(out ThrowableObject pickup))
            {
                if (pickup.State == ThrowableObjectState.Moving)
                {
                    if (pickup is Weapon weapon)
                    {
                        _attacker.Equip(weapon);
                        pickup.Equip(transform.position);
                    }
                }
            }
        }
    }
}