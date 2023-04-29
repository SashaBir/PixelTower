using UnityEngine;

namespace PixelTower.Addition
{
    public static class PhysicsExpansion
    {
        public static float CalculateForceNoMass(float distance, float gravityScale)
        {
            float g = -Physics2D.gravity.y;
            return Mathf.Sqrt(2 * (g * distance * gravityScale));
        }
    }
}