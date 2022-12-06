using System;
using System.Collections.Generic;

namespace SnakesWithGuns.Infrastructure.PubSub
{
    public class Channel<T> : IChannel<T>
    {
        private readonly HashSet<Action<T>> _actionsWithParameter;

        public Channel()
        {
            _actionsWithParameter = new HashSet<Action<T>>();
        }

        public void Publish(T message)
        {
            foreach (Action<T> action in _actionsWithParameter)
                action(message);
        }

        public void Register(Action<T> action)
        {
            _actionsWithParameter.Add(action);
        }

        public void Unregister(Action<T> action)
        {
            _actionsWithParameter.Remove(action);
        }
    }
}