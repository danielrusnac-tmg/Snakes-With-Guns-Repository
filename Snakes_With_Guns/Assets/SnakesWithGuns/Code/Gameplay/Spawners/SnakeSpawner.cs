using System.Collections.Generic;
using Cinemachine;
using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Settings;
using SnakesWithGuns.Gameplay.Snakes;
using SnakesWithGuns.Infrastructure;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Spawners
{
    public class SnakeSpawner : MonoBehaviour, ISceneService
    {
        [SerializeField] private Session _session;
        [SerializeField] private StaticData _staticData;

        [Header("Player")]
        [SerializeField] private int _goal = 20;

        [SerializeField] private int _goalStep = 5;
        [SerializeField] private Snake _playerPrefab;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Transform _playerPoint;

        [Header("Bots")]
        [SerializeField] private Snake _botPrefab;

        [SerializeField] private Transform _botPointsParent;

        private IChannel<LevelUpMessage> _levelUpChannel;
        private IChannel<LevelProgressMessage> _levelProgressChannel;
        private IChannel<SpawnFloatingTextMessage> _floatingTextChannel;
        private List<Snake> _bots = new();

        private int Goal => (_session.Player.Stats.Level.Value - 1) * _goalStep + _goal;
        private float Progress => Mathf.Clamp01((float)_session.Player.Stats.Energy.Value / Goal);

        public void Initialize()
        {
            _levelUpChannel = Channels.GetChannel<LevelUpMessage>();
            _levelProgressChannel = Channels.GetChannel<LevelProgressMessage>();
            _floatingTextChannel = Channels.GetChannel<SpawnFloatingTextMessage>();
        }

        public void Activate()
        {
            SpawnPlayer();
            SpawnBots();

            _session.Player.Stats.Level.Value++;
        }

        public void Tick(float deltaTime)
        {
        }

        public void Deactivate()
        {
        }

        public void Cleanup()
        {
            _session.Player.Stats.Level.Changed -= OnPlayerLevelChanged;
            _session.Player.Stats.Energy.Changed -= OnPlayerEnergyChanged;
        }

        private void SpawnBots()
        {
            if (GlobalSettings.SelectedGameMode.OpponentCount == 0)
                return;

            for (int i = 0; i < GlobalSettings.SelectedGameMode.OpponentCount; i++)
            {
                int childCount = _botPointsParent.childCount;
                Transform point = _botPointsParent.GetChild((int)Mathf.Repeat(i, childCount));
                _bots.Add(SpawnSnake(_botPrefab, point.position));
            }
        }

        private void SpawnPlayer()
        {
            Snake player = SpawnSnake(_playerPrefab, _playerPoint.position);

            player.Stats.Level.Changed += OnPlayerLevelChanged;
            player.Stats.Energy.Changed += OnPlayerEnergyChanged;

            _virtualCamera.m_Follow = player.transform;
            _virtualCamera.m_LookAt = player.transform;

            _session.AssignPlayer(player);
        }

        private void OnPlayerEnergyChanged(int energy)
        {
            _floatingTextChannel.Publish(new SpawnFloatingTextMessage
            {
                Position = _session.Player.transform.position,
                Value = 1,
                Color = new Color(0f, 0.96f, 0.45f),
                InstanceID = GetInstanceID()
            });

            if (_session.Player.Stats.Energy.Value >= Goal)
            {
                _session.Player.Stats.Energy.Value -= Goal;
                _session.Player.Stats.Level.Value++;
            }

            _levelProgressChannel.Publish(new LevelProgressMessage(Progress));
        }

        private void OnPlayerLevelChanged(int level)
        {
            _levelUpChannel.Publish(new LevelUpMessage
            {
                Level = level,
                Tail = _session.Player.Tail
            });

            AddRandomModuleToBots();
        }

        private void AddRandomModuleToBots()
        {
            foreach (Snake bot in _bots)
                bot.Tail.AddModule(_staticData.GetRandomModule());
        }

        private Snake SpawnSnake(Snake prefab, Vector3 position)
        {
            return Instantiate(prefab, position, Quaternion.identity);
        }
    }
}