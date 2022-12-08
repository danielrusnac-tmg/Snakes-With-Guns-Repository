using Cinemachine;
using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Spawners
{
    public class SnakeSpawner : MonoBehaviour
    {
        [SerializeField] private Session _session;

        [Header("Player")]
        [SerializeField] private Snake _playerPrefab;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Transform _playerPoint;

        [Header("Bots")]
        [SerializeField] private Snake _botPrefab;
        [SerializeField] private Transform[] _botPoints;

        private void Start()
        {
            SpawnPlayer();
            SpawnBots();
        }

        private void SpawnBots()
        {
            foreach (Transform point in _botPoints)
                SpawnSnake(_botPrefab, point.position);
        }

        private void SpawnPlayer()
        {
            Snake player = SpawnSnake(_playerPrefab, _playerPoint.position);
            _virtualCamera.m_Follow = player.transform;
            _virtualCamera.m_LookAt = player.transform;
            _session.AssignPlayer(player);;
        }

        private Snake SpawnSnake(Snake prefab, Vector3 position)
        {
            return Instantiate(prefab, position, Quaternion.identity);
        }
    }
}