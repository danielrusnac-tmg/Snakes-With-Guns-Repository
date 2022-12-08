using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;

namespace SnakesWithGuns.Gameplay
{
    public class Session : MonoBehaviour
    {
        public Snake Player { get; private set; }
        public float GameplayTime { get; private set; }

        private void Update()
        {
            GameplayTime += Time.deltaTime;
        }

        public void AssignPlayer(Snake player)
        {
            Player = player;
        }
    }
}