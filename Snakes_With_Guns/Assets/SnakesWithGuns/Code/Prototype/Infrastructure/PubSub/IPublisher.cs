using System;

namespace SnakesWithGuns.Prototype.Infrastructure.PubSub
{
    public interface IChannel<T>
    {
        void Publish(T message);
        void Register(Action<T> action);
        void Unregister(Action<T> action);
    }
}
