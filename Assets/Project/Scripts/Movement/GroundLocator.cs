using UnityEngine;

namespace PixelTower.Movement
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class GroundLocator : MonoBehaviour
    {
        [SerializeField] private float _lenght;
        [SerializeField][Min(0)] private int _groundLayerId;

        public bool IsGround { get; private set; }

        private void Update()
        {
            IsGround = Physics2D.Raycast(transform.position, Vector2.down, _lenght, 1 << _groundLayerId);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(new Ray(transform.position, Vector2.down * _lenght));
        }
    }
}