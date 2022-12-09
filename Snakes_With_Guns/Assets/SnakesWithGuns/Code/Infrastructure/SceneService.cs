namespace SnakesWithGuns.Infrastructure
{
    public interface ISceneService
    {
        void Initialize();
        void Activate();
        void Tick(float deltaTime);
        void Deactivate();
        void Cleanup();
    }
}