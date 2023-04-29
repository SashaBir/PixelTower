using PixelTower.Movement;
using UnityEngine;

namespace PixelTower.Animation
{
    public class PlayerAnimation : MonoBehaviour
    {
        [Header("Management")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private GroundLocator _groundLocator;

        [Header("Viewer")]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;

        private readonly int _running = Animator.StringToHash("IsRunning");
        private readonly int _jumping = Animator.StringToHash("IsJumping");
        private readonly int _falling = Animator.StringToHash("IsFalling");

        private void Update()
        {
            if (_rigidbody.velocity.x != 0)
                _renderer.flipX = _rigidbody.velocity.x <= 0;

            if (_groundLocator.IsGround == false && _rigidbody.velocity.y > 0)
            {
                _animator.SetBool(_jumping, true);
                _animator.SetBool(_falling, false);
                _animator.SetBool(_running, false);
            }

            if (_groundLocator.IsGround == false && _rigidbody.velocity.y < 0)
            {
                _animator.SetBool(_jumping, false);
                _animator.SetBool(_falling, true);
                _animator.SetBool(_running, false);
            }

            if (_groundLocator.IsGround == true && _rigidbody.velocity.x != 0)
            {
                _animator.SetBool(_jumping, false);
                _animator.SetBool(_falling, false);
                _animator.SetBool(_running, true);
            }
        }
    }
}