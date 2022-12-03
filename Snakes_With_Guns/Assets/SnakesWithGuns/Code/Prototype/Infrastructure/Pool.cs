using System;
using System.Collections.Generic;

namespace SnakesWithGuns.Prototype.Infrastructure
{
    public class Pool<T> : IPool<T>
    {
        private Queue<T> _instances;
        private Func<T> _onCreate;

        public Pool(Func<T> onCreate, int defaultSize = 0)
        {
            _onCreate = onCreate;
            _instances = new Queue<T>(defaultSize);

            if (defaultSize == 0)
                return;

            for (int i = 0; i < defaultSize; i++)
                _instances.Enqueue(onCreate());
        }

        public T GetInstance()
        {
            return _instances.Count == 0
                ? _onCreate()
                : _instances.Dequeue();
        }

        public void ReturnInstance(T instance)
        {
            _instances.Enqueue(instance);
        }
    }
}