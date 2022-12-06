using System;
using System.Collections.Generic;

namespace SnakesWithGuns.Infrastructure.PubSub
{
    public static class Channels
    {
        private static Dictionary<Type, object> s_channels;

        static Channels()
        {
            s_channels = new Dictionary<Type, object>();
        }

        public static IChannel<T> GetChannel<T>()
        {
            Type type = typeof(T);

            if (!s_channels.ContainsKey(type))
                s_channels.Add(type, new Channel<T>());

            return (IChannel<T>)s_channels[type];
        }
    }
}