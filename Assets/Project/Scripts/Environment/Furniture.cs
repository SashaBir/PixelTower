using PixelTower.Addition;
using PixelTower.Entity;
using UnityEngine;

namespace PixelTower.Environment
{
    public class Furniture : MonoBehaviour, IDamageable, IRepelable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _health;

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
    }
}