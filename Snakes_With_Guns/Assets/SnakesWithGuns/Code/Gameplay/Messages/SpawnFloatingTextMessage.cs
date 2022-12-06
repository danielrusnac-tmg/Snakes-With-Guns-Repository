using UnityEngine;

namespace SnakesWithGuns.Gameplay.Messages
{
    public struct SpawnFloatingTextMessage
    {
        public Vector3 Position;
        public string Message;
        public Color Color;

        public SpawnFloatingTextMessage(Vector3 position, string message) : this(position, message, Color.white) {}
        
        public SpawnFloatingTextMessage(Vector3 position, string message, Color color)
        {
            Position = position;
            Message = message;
            Color = color;
        }
    }
}