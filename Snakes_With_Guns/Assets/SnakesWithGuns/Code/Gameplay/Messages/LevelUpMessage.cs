namespace SnakesWithGuns.Gameplay.Messages
{
    public struct LevelUpMessage
    {
        public int Level;

        public LevelUpMessage(int level)
        {
            Level = level;
        }
    }
}