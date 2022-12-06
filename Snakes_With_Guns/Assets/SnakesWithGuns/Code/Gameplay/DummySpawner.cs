using SnakesWithGuns.Gameplay.Objects;
using SnakesWithGuns.Gameplay.Snakes;
using SnakesWithGuns.Gameplay.Weapons;
using UnityEngine;
using UnityEngine.Pool;

namespace SnakesWithGuns.Gameplay
{
    public class DummySpawner : MonoBehaviour
    {
        [SerializeField] private Dummy[] _dummyPrefabs;
        [SerializeField] private Snake _player;
        [SerializeField] private float _spawnRate = 2;
        [SerializeField] private float _spawnRadius = 15;

        private ObjectPool<Dummy> _dummyyPool;
        private float _spawnTime;

        private void Awake()
        {
            _dummyyPool =
                new ObjectPool<Dummy>(CreateDummy, OnGetDummy, OnReleaseDummy, OnDestroyDummy, false);
        }

        private void Update()
        {
            _spawnTime += _spawnRate * Time.deltaTime;

            while (_spawnTime > 1)
            {
                Spawn();
                _spawnTime -= 1;
            }
        }

        private void Spawn()
        {
            Dummy dummy = _dummyyPool.Get();
            dummy.transform.position = GetRandomPosition();
            dummy.Initialize(_player.transform);
            dummy.gameObject.SetActive(true);
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 offset = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward;
            offset *= _spawnRadius;
            
            return _player.transform.position + offset;
        }

        private void OnDummyDied(ITarget dummy)
        {
            _dummyyPool.Release(dummy as Dummy);
        }

        private Dummy CreateDummy()
        {
            Dummy dummy = Instantiate(_dummyPrefabs[Random.Range(0, _dummyPrefabs.Length)]);
            dummy.Died += OnDummyDied;
            return dummy;
        }

        private void OnGetDummy(Dummy dummy)
        {
           
        }

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