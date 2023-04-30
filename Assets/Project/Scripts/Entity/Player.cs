using PixelTower.Addition;
using PixelTower.Environment;
using UnityEngine;

namespace PixelTower.Entity
{
    public class Player : MonoBehaviour, IDamageable, IRepelable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _health;

        public void Damage(float damage)
        {
            _health -= damage;
        }

        public void Repel(Vector2 direction, float distance)
        {
            float force = PhysicsExpansion.CalculateForceNoMass(distance, _rigidbody.gravityScale);
            _rigidbody.velocity = direction * force;
        }
    }
}
