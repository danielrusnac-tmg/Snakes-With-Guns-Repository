using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Objects
{
    public class EnergyCollector : MonoBehaviour
    {
        [SerializeField] private Snake _snake;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Collectable collectable))
            {
                collectable.Collect();
                _snake.Stats.Energy.Value++;
            }
        }
    }
}