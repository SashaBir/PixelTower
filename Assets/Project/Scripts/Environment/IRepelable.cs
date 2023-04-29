using UnityEngine;

namespace PixelTower.Environment
{

    public interface IRepelable
    {
        void Repel(Vector2 direction, float force);
    }
}