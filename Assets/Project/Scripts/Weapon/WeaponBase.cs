using System;
using UnityEngine;

namespace PixelTower.Weapon
{
    public class WeaponBase : MonoBehaviour
    {
        public event Action OnAttacked;

        protected virtual void Attack()
        {
            OnAttacked?.Invoke();
        }
    }
}
