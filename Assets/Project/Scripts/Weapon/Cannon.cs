using PixelTower.Addition;
using System.Collections;
using UnityEngine;

namespace PixelTower.Weapon
{
    public class Cannon : MonoBehaviour
    {
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
