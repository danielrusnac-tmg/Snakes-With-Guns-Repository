using SnakesWithGuns.Gameplay.Objects;
using SnakesWithGuns.Gameplay.Settings;
using SnakesWithGuns.Gameplay.Weapons;
using SnakesWithGuns.Infrastructure;
using UnityEngine;
using UnityEngine.Pool;

namespace SnakesWithGuns.Gameplay.Spawners
{
    public class DummySpawner : MonoBehaviour, ISceneService
    {
        [SerializeField] private Session _session;
        [SerializeField] private float _spawnRadius = 15;

        private ObjectPool<Dummy> _dummyyPool;
        private float _spawnTime;

        private Transform PlayerTransform => _session.Player == null ? transform : _session.Player.transform;

        public void Initialize()
        {
            _dummyyPool =
                new ObjectPool<Dummy>(CreateDummy, OnGetDummy, OnReleaseDummy, OnDestroyDummy, false);
        }

        public void Activate() { }

        public void Tick(float deltaTime)
        {
            _spawnTime += GlobalSettings.SelectedGameMode.DummySpawn.CalculateSpawnRate(_session.GameplayTime) * deltaTime;

            while (_spawnTime > 1)
            {
                Spawn();
                _spawnTime -= 1;
            }
        }

        public void Deactivate() { }

        public void Cleanup() { }

        private void Spawn()
        {
            Dummy dummy = _dummyyPool.Get();
            dummy.transform.position = GetRandomPosition();
            dummy.Initialize(PlayerTransform);
            dummy.gameObject.SetActive(true);
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 offset = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward;
            offset *= _spawnRadius;

            return PlayerTransform.position + offset;
        }

        private void OnDummyDied(ITarget dummy)
        {
            _dummyyPool.Release(dummy as Dummy);
        }

        private Dummy CreateDummy()
        {
            Dummy dummy = Instantiate(GlobalSettings.SelectedGameMode.DummySpawn.GetPrefab(_session.GameplayTime));
            dummy.Died += OnDummyDied;
            return dummy;
        }

        private void OnGetDummy(Dummy dummy) { }

        private void OnReleaseDummy(Dummy dummy)
        {
            dummy.gameObject.SetActive(false);
        }

        private void OnDestroyDummy(Dummy dummy)
        {
            dummy.Died -= OnDummyDied;
            Destroy(dummy.gameObject);
        }
    }
}