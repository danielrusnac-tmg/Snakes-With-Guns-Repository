using UnityEngine;

namespace SnakesWithGuns.Gameplay.Messages
{
    public struct SpawnEnergyMessage
    {
        public Vector3 Position;
        public int Amount;
    }
}