using System;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Objects
{
    public class Collectable : MonoBehaviour
    {
        public event Action<Collectable> Collected; 

        [SerializeField] private SphereCollider _sphereCollider;

        public void OnSpawn()
        {
            _sphereCollider.enabled = true;
        }

        public void Collect()
        {
            _sphereCollider.enabled = false;
            Collected?.Invoke(this);
        }
    }
}