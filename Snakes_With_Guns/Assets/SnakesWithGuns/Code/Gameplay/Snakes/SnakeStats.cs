using System;

namespace SnakesWithGuns.Gameplay.Snakes
{
    [Serializable]
    public class SnakeStat
    {
        public event Action<int> Changed;

        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke(value);
            }
        }

        public SnakeStat() { }

        public SnakeStat(int value)
        {
            Value = value;
        }
    }

    [Serializable]
    public class SnakeStats
    {
        public SnakeStat Level = new();
        public SnakeStat Energy = new();
    }
}