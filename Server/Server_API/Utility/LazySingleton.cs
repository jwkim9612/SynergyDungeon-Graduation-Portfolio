using System;

namespace Server_API.Utility
{
    public class LazySingleton<T> where T : class, new()
    {
        protected LazySingleton() { }
        private static readonly Lazy<T> lazy = new Lazy<T>(InitLazy);
        private static T InitLazy() { return new T(); }
        public static T Instance => lazy.Value;
    }
}
