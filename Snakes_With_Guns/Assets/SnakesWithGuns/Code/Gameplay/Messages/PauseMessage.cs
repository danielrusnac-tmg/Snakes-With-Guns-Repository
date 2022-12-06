namespace SnakesWithGuns.Gameplay.Messages
{
    public struct PauseMessage
    {
        public bool IsPaused;
        
        public PauseMessage(bool isPaused)
        {
            IsPaused = isPaused;
        }
    }
}