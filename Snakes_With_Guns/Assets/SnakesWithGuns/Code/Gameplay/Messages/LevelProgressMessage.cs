namespace SnakesWithGuns.Gameplay.Messages
{
    public struct LevelProgressMessage
    {
        public float Progress;
        
        public LevelProgressMessage(float progress)
        {
            Progress = progress;
        }
    }
}