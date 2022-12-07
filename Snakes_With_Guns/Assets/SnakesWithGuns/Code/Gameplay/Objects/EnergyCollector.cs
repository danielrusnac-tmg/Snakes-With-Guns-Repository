﻿using System;
using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Snakes;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Objects
{
    public class EnergyCollector : MonoBehaviour
    {
        [SerializeField] private int _goal = 20;
        [SerializeField] private int _goalStep = 5;
        [SerializeField] private int _level = 1;
        [SerializeField] private Tail _tail;

        private int _current;
        private IChannel<LevelUpMessage> _levelUpChannel;
        private IChannel<LevelProgressMessage> _levelProgressChannel;
        private IChannel<SpawnFloatingTextMessage> _floatingTextChannel;

        private int Goal => (_level - 1) * _goalStep + _goal;
        private float Progress => Mathf.Clamp01((float)_current / Goal);

        private void Awake()
        {
            _levelUpChannel = Channels.GetChannel<LevelUpMessage>();
            _levelProgressChannel = Channels.GetChannel<LevelProgressMessage>();
            _floatingTextChannel = Channels.GetChannel<SpawnFloatingTextMessage>();
        }

        private void Start()
        {
            OnLevelUp();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Collectable collectable))
            {
                collectable.Collect();
                _current++;
                _floatingTextChannel.Publish(new SpawnFloatingTextMessage
                {
                    Position = collectable.transform.position,
                    Value = 1,
                    Color = new Color(0f, 0.96f, 0.45f),
                    InstanceID = GetInstanceID()
                });

                OnEnergyChanged();
            }
        }

        private void OnEnergyChanged()
        {
            if (_current >= Goal)
            {
                _current -= Goal;
                _level++;
                
                OnLevelUp();
            }

            _levelProgressChannel.Publish(new LevelProgressMessage(Progress));
        }

        private void OnLevelUp()
        {
            _levelUpChannel.Publish(new LevelUpMessage
            {
                Level = _level,
                Tail = _tail
            });
        }
    }
}