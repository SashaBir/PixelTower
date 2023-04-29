using PixelTower.Addition;
using PixelTower.Input;
using UnityEngine;

namespace PixelTower.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private GroundLocator _locator;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;

        private InputSystem _input;
        private float _direction = 0;

        public Vector2 LookedDirection { get; private set; } = Vector2.zero;

        public Vector2 Velocity => _rigidbody.velocity;

        public bool IsOnGround => _locator.IsGround;

        private void Awake()
        {
            _input = new();
        }

        private void OnEnable()
        {
            _input.Player.Move.performed += OnSetDirection;
            _input.Player.Move.canceled += OnSetDirection;
            _input.Player.Jump.performed += OnJump;

            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Player.Move.performed -= OnSetDirection;
            _input.Player.Move.canceled -= OnSetDirection;
            _input.Player.Jump.performed -= OnJump;

            _input.Disable();
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity != Vector2.zero)
                LookedDirection = _rigidbody.velocity.x > 0 ? Vector2.right : Vector2.left;

            _rigidbody.velocity = new Vector2
            {
                x = _direction * _speed,
                y = _rigidbody.velocity.y
            };
        }

        private void OnSetDirection(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>().x;
        }

        private void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (_locator.IsGround == true)
                Jump();
        }

        private void Jump()
        {
            float force = PhysicsExpansion.CalculateForceNoMass(_jumpHeight, _rigidbody.gravityScale);

            _rigidbody.velocity = new Vector2
            {
                x = _rigidbody.velocity.x,
                y = force
            };
        }
    }
}