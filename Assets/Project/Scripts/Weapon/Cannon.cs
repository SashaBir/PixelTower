using PixelTower.Addition;
using PixelTower.Entity;
using PixelTower.Environment;
using System.Collections;
using UnityEngine;

namespace PixelTower.Weapon
{
    public class Cannon : MonoBehaviour, IDamageable, IRepelable
    {
        [Header("Interacting")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _health;

        [Header("Attcking")]
        [SerializeField] private Rigidbody2D _projectile;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private float _distance;
        [SerializeField] private float _delay;

        [Header("Viewer")]
        [SerializeField] private Animator _animator;

        private readonly int _attacking = Animator.StringToHash("Attacking");

        private void Start()
        {
            StartCoroutine(Attack());
        }

        public void Damage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
                Destroy(gameObject);
        }

        public void Repel(Vector2 direction, float distance)
        {
            float force = PhysicsExpansion.CalculateForceNoMass(distance, _rigidbody.gravityScale);
            _rigidbody.velocity = direction * force;
            _rigidbody.AddTorque(Random.Range(-36, 36));
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(Random.Range(3, 5f));

            var delay = new WaitForSeconds(_delay);
            do
            {
                yield return delay;

                Shoot();
                AnimateAttack();
            }
            while (true);
        }

        private void Shoot()
        {
            var projectile = Instantiate(_projectile, _muzzle.position, _muzzle.rotation);
            var force = PhysicsExpansion.CalculateForceNoMass(_distance, projectile.gravityScale);
            projectile.velocity = _direction * force;
        }

        private void AnimateAttack()
        {
            _animator.SetTrigger(_attacking);
        }
    }
}
