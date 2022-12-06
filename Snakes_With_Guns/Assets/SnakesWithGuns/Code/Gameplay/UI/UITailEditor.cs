using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UITailEditor : MonoBehaviour
    {
        private Tail _tail;

        public void Display(Tail tail)
        {
            _tail = tail;
        }

        public void AddSegment()
        {
            _tail.AddSegment();
        }

        public void RemoveSegment()
        {
            _tail.RemoveSegment();
        }
    }
}
