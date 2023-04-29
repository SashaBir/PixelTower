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
            Flip();
            ManageRunning();
            ManageJumping();
            ManageFalling();
        }

        private void Flip()
        {
            if (_rigidbody.velocity.x != 0)
                _renderer.flipX = _rigidbody.velocity.x <= 0;
        }

        private void ManageRunning()
        {
            _animator.SetBool(_running, _groundLocator.IsGround == true && _rigidbody.velocity.x != 0);
        }

        private void ManageJumping()
        {
            _animator.SetBool(_jumping, _groundLocator.IsGround == false && _rigidbody.velocity.y > 0);
        }

        private void ManageFalling()
        {
            _animator.SetBool(_falling, _groundLocator.IsGround == false && _rigidbody.velocity.y < 0);
        }
    }
}