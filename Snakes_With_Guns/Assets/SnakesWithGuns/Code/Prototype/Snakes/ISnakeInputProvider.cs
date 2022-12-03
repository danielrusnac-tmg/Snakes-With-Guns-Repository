using UnityEngine;

namespace SnakesWithGuns.Prototype.Snakes
{
    public interface ISnakeInputProvider
    {
        Vector3 Direction { get; }
    }
}