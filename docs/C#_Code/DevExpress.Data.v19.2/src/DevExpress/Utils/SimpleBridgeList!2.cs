namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class SimpleBridgeList<T, TKey> : BridgeList<T, TKey> where T: class
    {
        private static Func<TKey, T> defaultCast;
        private readonly Func<TKey, T> cast;
        private readonly Func<T, TKey> castBack;

        static SimpleBridgeList()
        {
            SimpleBridgeList<T, TKey>.defaultCast = key => key as T;
        }

        public SimpleBridgeList(IList<TKey> keys, Func<TKey, T> cast = null, Func<T, TKey> castBack = null) : base(keys)
        {
            Func<TKey, T> defaultCast = cast;
            if (cast == null)
            {
                Func<TKey, T> local1 = cast;
                defaultCast = SimpleBridgeList<T, TKey>.defaultCast;
            }
            this.cast = defaultCast;
            this.castBack = castBack;
        }

        protected override void AddCore(T item)
        {
            if (this.castBack != null)
            {
                base.keys.Add(this.castBack(item));
            }
            else
            {
                base.AddCore(item);
            }
        }

        protected override int AddObjectCore(object item) => 
            ((this.castBack == null) || !(base.keys is IList)) ? base.AddObjectCore(item) : ((IList) base.keys).Add(this.castBack((T) item));

        protected override void ClearCore()
        {
            if (this.castBack != null)
            {
                base.keys.Clear();
            }
            else
            {
                base.ClearCore();
            }
        }

        protected override T GetItemByKey(TKey key, int index) => 
            this.cast(key);

        protected override void InsertCore(int index, T item)
        {
            if (this.castBack != null)
            {
                base.keys.Insert(index, this.castBack(item));
            }
            else
            {
                base.InsertCore(index, item);
            }
        }

        protected override bool IsReadOnlyCore() => 
            (this.castBack == null) ? base.IsReadOnlyCore() : base.keys.IsReadOnly;

        protected override void RemoveAtCore(int index)
        {
            if (this.castBack != null)
            {
                base.keys.RemoveAt(index);
            }
            else
            {
                base.RemoveAtCore(index);
            }
        }

        protected override bool RemoveCore(T item) => 
            (this.castBack == null) ? base.RemoveCore(item) : base.keys.Remove(this.castBack(item));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SimpleBridgeList<T, TKey>.<>c <>9;

            static <>c()
            {
                SimpleBridgeList<T, TKey>.<>c.<>9 = new SimpleBridgeList<T, TKey>.<>c();
            }

            internal T <.cctor>b__12_0(TKey key) => 
                key as T;
        }
    }
}

