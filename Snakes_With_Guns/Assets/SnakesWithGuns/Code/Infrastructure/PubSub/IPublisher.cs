using System;

namespace SnakesWithGuns.Infrastructure.PubSub
{
    public interface IChannel<T>
    {
        void Publish(T message);
        void Register(Action<T> action);
        void Unregister(Action<T> action);
    }
}
