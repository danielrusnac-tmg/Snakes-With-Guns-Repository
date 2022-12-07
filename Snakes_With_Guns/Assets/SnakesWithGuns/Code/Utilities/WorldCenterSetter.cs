using UnityEngine;

namespace SnakesWithGuns.Utilities
{
    public class WorldCenterSetter : MonoBehaviour
    {
        private static readonly int WORLD_CENTER = Shader.PropertyToID("_WorldCenter");

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            Shader.SetGlobalVector(WORLD_CENTER, _transform.position);
        }
    }
}