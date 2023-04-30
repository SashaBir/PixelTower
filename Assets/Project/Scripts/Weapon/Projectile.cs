using PixelTower.Entity;
using UnityEngine;

namespace PixelTower.Weapon
{
    public class Projectile : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _health;

        public void Damage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
                Destroy(gameObject);
        }
    }
}
