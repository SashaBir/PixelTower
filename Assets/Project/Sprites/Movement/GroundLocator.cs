using UnityEngine;

namespace PixelTower.Movement
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class GroundLocator : MonoBehaviour
    {
        [SerializeField][Min(0)] private int _groundLayerId;

        public bool IsGround { get; private set; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == _groundLayerId)
                IsGround = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.layer == _groundLayerId)
                IsGround = false;
        }
    }
}