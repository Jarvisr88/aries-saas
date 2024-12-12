namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class LazyValue<T> where T: class
    {
        private T value;
        private bool loaded;
        private Func<T> loader;

        public LazyValue(Func<T> loader);

        public bool Loaded { get; }

        public T Value { get; }
    }
}

