using UnityEngine;

namespace SnakesWithGuns.Gameplay.Snakes
{
    public interface ISnakeInputProvider
    {
        Vector3 Direction { get; }
    }
}