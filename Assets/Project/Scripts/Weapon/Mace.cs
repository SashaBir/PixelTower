using PixelTower.Entity;
using PixelTower.Environment;
using PixelTower.Input;
using PixelTower.Movement;
using UnityEngine;

namespace PixelTower.Weapon
{
    public class Mace : WeaponBase
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private float _damage;
        [SerializeField] private float _repeledDistance;
        [SerializeField] private float _detectedDistance;
        [SerializeField] private float _delay;

        private InputSystem _input;
        private bool _canAttacking = true;

        private void Awake()
        {
            _input = new();
        }

        private void OnEnable()
        {
            _input.Player.Attack.performed += OnAttack;
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Player.Attack.performed -= OnAttack;
            _input.Disable();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(new Ray(transform.position, _movement.LookedDirection * _detectedDistance));
        }

        private void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (_canAttacking == false)
                return;

            _canAttacking = false;

            Invoke(nameof(CountDelay), _delay);
            Attack();
        }

        protected override void Attack()
        {
            var direction = _movement.LookedDirection;
            var hits = Physics2D.RaycastAll(transform.position, direction, _detectedDistance);
            if (hits.Length == 0)
                return;

            var repeledDirection = direction + new Vector2(0, 0.5f);
            foreach (var hit in hits)
            {
                if (hit.transform == null)
                    continue;

                if (hit.transform.TryGetComponent(out IRepelable repelable) == true)
                    repelable.Repel(repeledDirection, _repeledDistance);

                if (hit.transform.TryGetComponent(out IDamageable damageable) == true)
                    damageable.Damage(_damage);
            }

            base.Attack();
        }

        private void CountDelay()
        {
            _canAttacking = true;
        }
    }
}
