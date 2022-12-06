using System;
using SnakesWithGuns.Gameplay.Weapons;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Objects
{
    public class Health : MonoBehaviour, IDamageable
    {
        public event Action Changed;
        public event Action Died;

        [SerializeField] private int _maxHealth = 100;

        public int Current { get; private set; }
        public int Max => _maxHealth;
        public float Percent => Mathf.Clamp01((float)Current / Max);

        private void Awake()
        {
            Current = Max;
        }

        public void DealDamage(int amount)
        {
            int newHealth = Mathf.Max(0, Current - amount);

            if (Current == newHealth)
                return;

            Current = newHealth;

            Changed?.Invoke();

            if (Current == 0)
                Died?.Invoke();
        }
    }
}