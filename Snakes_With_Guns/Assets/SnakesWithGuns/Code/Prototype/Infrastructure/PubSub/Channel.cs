using System;
using System.Collections.Generic;

namespace SnakesWithGuns.Prototype.Infrastructure.PubSub
{
    public class Channel<T> : IChannel<T>
    {
        private readonly HashSet<Action<T>> _actionsWithParameter;
        private readonly HashSet<Action> _actionsEmpty;

        public Channel()
        {
            _actionsWithParameter = new HashSet<Action<T>>();
            _actionsEmpty = new HashSet<Action>();
        }

        public void Publish(T message)
        {
            foreach (Action<T> action in _actionsWithParameter)
                action(message);
            
            foreach (Action action in _actionsEmpty)
                action();
        }

        public void Register(Action<T> action)
        {
            _actionsWithParameter.Add(action);
        }

        public void Register(Action action)
        {
            _actionsEmpty.Add(action);
        }

        public void Unregister(Action<T> action)
        {
            _actionsWithParameter.Remove(action);
        }

        public void Unregister(Action action)
        {
            _actionsEmpty.Remove(action);
        }
    }
}