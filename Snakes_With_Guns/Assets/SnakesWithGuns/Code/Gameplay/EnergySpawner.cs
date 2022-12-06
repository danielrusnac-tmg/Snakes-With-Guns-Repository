﻿using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Objects;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;
using UnityEngine.Pool;

namespace SnakesWithGuns.Gameplay
{
    public class EnergySpawner : MonoBehaviour
    {
        private IChannel<SpawnEnergyMessage> _spawnEnergyChannel;

        [SerializeField] private Collectable _energyPrefab;

        private ObjectPool<Collectable> _energyPool;

        private void Awake()
        {
            _energyPool =
                new ObjectPool<Collectable>(CreateEnergy, OnGetEnergy, OnReleaseEnergy, OnDestroyEnergy, false);
            _spawnEnergyChannel = Channels.GetChannel<SpawnEnergyMessage>();
            _spawnEnergyChannel.Register(OnSpawnEnergyMessage);
        }

        private void OnDestroy()
        {
            _spawnEnergyChannel.Unregister(OnSpawnEnergyMessage);
        }
        
        private void OnSpawnEnergyMessage(SpawnEnergyMessage message)
        {
            Collectable energy = _energyPool.Get();

            Vector3 position = message.Position;
            position.y = 0f;
            
            energy.transform.position = position;
            energy.OnSpawn();
        }

        private void OnEnergyCollected(Collectable energy)
        {
            _energyPool.Release(energy);
        }

        private Collectable CreateEnergy()
        {
            Collectable energy = Instantiate(_energyPrefab);
            energy.Collected += OnEnergyCollected;
            return energy;
        }

        private void OnGetEnergy(Collectable energy)
        {
            energy.gameObject.SetActive(true);
        }

        private void OnReleaseEnergy(Collectable energy)
        {
            energy.gameObject.SetActive(false);
        }

        private void OnDestroyEnergy(Collectable energy)
        {
            energy.Collected -= OnEnergyCollected;
            Destroy(energy.gameObject);
        }
    }
}