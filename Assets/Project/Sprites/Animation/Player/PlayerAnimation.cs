using UnityEngine;

namespace PixelTower.Animation
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _renderer;

        private readonly int _running = Animator.StringToHash("IsRunning");

        private void Update()
        {
            _animator.SetBool(_running, _rigidbody.velocity != Vector2.zero);
            _renderer.flipX = _rigidbody.velocity.x <= 0;
        }
    }
}