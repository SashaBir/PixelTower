using PixelTower.Entity;
using PixelTower.Environment;
using UnityEngine;

namespace PixelTower.Weapon
{
    public class Projectile : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _health;
        [SerializeField] private float _destroyAfterSpawning;
        [SerializeField] private float _distanceRepeled;
        [SerializeField] private float _radiusDetected;

        private void Start()
        {
            Destroy(gameObject, _destroyAfterSpawning);
        }

        private void OnDestroy()
        {
            Explosion();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out IRepelable repelable) == true)
            {
                var direction = (collision.transform.position - transform.position).normalized;
                repelable.Repel(direction, _distanceRepeled);
            }

            if (collision.transform.TryGetComponent(out IDamageable damageable) == true)
                damageable.Damage(_damage);


            Destroy(gameObject);
        }

        public void Damage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
                Destroy(gameObject);
        }

        private void Explosion()
        {
            var hits = Physics2D.CircleCastAll(transform.position, _radiusDetected, Vector2.zero);
            foreach (var hit in hits)
            {
                if (hit.transform == null)
                    continue;

                var direction = hit.transform.position - transform.position;
                if (hit.transform.TryGetComponent(out IRepelable repelable) == true)
                    repelable.Repel(direction.normalized, _distanceRepeled);

                if (hit.transform.TryGetComponent(out IDamageable damageable) == true)
                    damageable.Damage(_damage);
            }
        }
    }
}
