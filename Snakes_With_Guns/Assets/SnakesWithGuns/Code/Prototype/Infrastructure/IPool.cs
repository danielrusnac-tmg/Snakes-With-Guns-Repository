namespace SnakesWithGuns.Prototype.Infrastructure
{
    public interface IPool<T>
    {
        T GetInstance();
        void ReturnInstance(T instance);
    }
}