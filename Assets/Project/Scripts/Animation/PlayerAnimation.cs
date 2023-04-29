using PixelTower.Movement;
using PixelTower.Weapon;
using UnityEngine;

namespace PixelTower.Animation
{
    public class PlayerAnimation : MonoBehaviour
    {
        [Header("Management")]
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private WeaponBase _weapon;

        [Header("Viewer")]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;

        private readonly int _running = Animator.StringToHash("IsRunning");
        private readonly int _jumping = Animator.StringToHash("IsJumping");
        private readonly int _falling = Animator.StringToHash("IsFalling");
        private readonly int _attacking = Animator.StringToHash("Attacking");

        public Vector2 Velocity => _movement.Velocity;

        private bool IsGround => _movement.IsOnGround;

        private void OnEnable()
        {
            _weapon.OnAttacked += OnManageAttack;
        }

        private void OnDisable()
        {
            _weapon.OnAttacked -= OnManageAttack;
        }

        private void Update()
        {
            Flip();
            ManageRunning();
            ManageJumping();
            ManageFalling();
        }

        private void OnManageAttack()
        {
            _animator.SetTrigger(_attacking);
        }

        private void Flip()
        {
            _renderer.flipX = _movement.LookedDirection.x <= 0;
        }

        private void ManageRunning()
        {
            _animator.SetBool(_running, IsGround == true && Velocity.x != 0);
        }

        private void ManageJumping()
        {
            _animator.SetBool(_jumping, IsGround == false && Velocity.y > 0);
        }

        private void ManageFalling()
        {
            _animator.SetBool(_falling, IsGround == false && Velocity.y < 0);
        }
    }
}