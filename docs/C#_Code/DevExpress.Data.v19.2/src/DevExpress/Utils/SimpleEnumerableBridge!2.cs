namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SimpleEnumerableBridge<T, Key> : IEnumerable<T>, IEnumerable where T: class
    {
        private IEnumerable<Key> keys;
        private Func<Key, T> cast;
        private static Func<Key, T> defaultCast;

        static SimpleEnumerableBridge()
        {
            SimpleEnumerableBridge<T, Key>.defaultCast = key => key as T;
        }

        public SimpleEnumerableBridge(IEnumerable<Key> keys) : this(keys, SimpleEnumerableBridge<T, Key>.defaultCast)
        {
        }

        protected SimpleEnumerableBridge(IEnumerable<Key> keys, Func<Key, T> cast)
        {
            this.keys = keys;
            this.cast = cast;
        }

        public IEnumerator<T> GetEnumerator() => 
            new EnumerableBridgeEnumerator<T, Key>(this.keys.GetEnumerator(), this.cast);

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SimpleEnumerableBridge<T, Key>.<>c <>9;

            static <>c()
            {
                SimpleEnumerableBridge<T, Key>.<>c.<>9 = new SimpleEnumerableBridge<T, Key>.<>c();
            }

            internal T <.cctor>b__7_0(Key key) => 
                key as T;
        }
    }
}

