using UnityEngine;
using UnityEngine.Pool;

namespace SnakesWithGuns.Utilities
{
    public class ComponentPool<T> : ObjectPool<T> where T : Component
    {
        public ComponentPool(T prefab, int defaultCapacity = 10, int maxSize = 10000) : base(
            () => Object.Instantiate(prefab),
            o => o.gameObject.SetActive(false),
            o => o.gameObject.SetActive(true),
            o => Object.Destroy(o.gameObject),
            false, defaultCapacity, maxSize) { }
    }
}