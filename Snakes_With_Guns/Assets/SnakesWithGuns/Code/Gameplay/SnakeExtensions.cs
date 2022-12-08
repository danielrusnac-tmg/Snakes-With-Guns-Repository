using System.Linq;
using SnakesWithGuns.Gameplay.Snakes;

namespace SnakesWithGuns.Gameplay
{
    public static class SnakeExtensions
    {
        public static void AddModule(this Tail tail, SegmentModule module)
        {
            tail.AddSegment();
            tail.Segments.Last().InstallModule(module);
        }
    }
}