using UnityEngine;
using UnityEngine.Pool;

namespace SnakesWithGuns.Utilities
{
    public class GameObjectPool : ObjectPool<GameObject>
    {
        public GameObjectPool(GameObject prefab, int defaultCapacity = 10, int maxSize = 10000) : base(
            () => Object.Instantiate(prefab),
            o => o.SetActive(false),
            o => o.SetActive(true),
            o => Object.Destroy(o),
            false, defaultCapacity, maxSize) { }
    }
}