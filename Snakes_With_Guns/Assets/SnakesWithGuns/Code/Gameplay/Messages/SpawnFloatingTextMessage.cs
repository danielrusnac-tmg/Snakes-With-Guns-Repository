using UnityEngine;

namespace SnakesWithGuns.Gameplay.Messages
{
    public struct SpawnFloatingTextMessage
    {
        public Vector3 Position;
        public int Value;
        public Color Color;
        public int InstanceID;
    }
}