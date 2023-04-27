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
            float g = -Physics2D.gravity.y;
            float h = _jumpHeight;
            float gs = _rigidbody.gravityScale;

            float force = Mathf.Sqrt(2 * (g * h * gs));

            _rigidbody.velocity = new Vector2
            {
                x = _rigidbody.velocity.x,
                y = force
            };
        }
    }
}