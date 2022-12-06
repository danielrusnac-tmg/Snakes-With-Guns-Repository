using System;
using SnakesWithGuns.Gameplay.Weapons;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Objects
{
    public class Health : MonoBehaviour, IDamageable
    {
        public event Action<ChangeData> Changed;
        public event Action Died;

        [SerializeField] private int _maxHealth = 100;

        public int Current { get; private set; }
        public int Max => _maxHealth;
        public float Percent => Mathf.Clamp01((float)Current / Max);

        private void Awake()
        {
           ResetHealth();
        }

        public void ResetHealth()
        {
            Current = Max;
        }

        public void DealDamage(int amount)
        {
            int newHealth = Mathf.Max(0, Current - amount);

            if (Current == newHealth)
                return;

            Changed?.Invoke(new ChangeData(Current, newHealth));
            Current = newHealth;

            if (Current == 0)
                Died?.Invoke();
        }
        
        public struct ChangeData
        {
            public int OldHealth;
            public int NewHealth;
            public int Delta;

            public ChangeData(int oldHealth, int newHealth)
            {
                OldHealth = oldHealth;
                NewHealth = newHealth;
                Delta = newHealth - oldHealth;
            }
        }
    }
}