namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class WeakKeyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey: class
    {
        private readonly List<WeakReference> keys;
        private readonly List<TValue> values;

        public WeakKeyDictionary()
        {
            this.keys = new List<WeakReference>();
            this.values = new List<TValue>();
        }

        public void Add(TKey key, TValue value)
        {
            this.Insert(key, value, true);
        }

        public void Clear()
        {
            this.keys.Clear();
            this.values.Clear();
        }

        public bool ContainsKey(TKey key) => 
            this.FindEntry(key) >= 0;

        public bool ContainsValue(TValue value) => 
            this.values.IndexOf(value) >= 0;

        private int FindEntry(TKey key)
        {
            int num = -1;
            for (int i = this.Count - 1; i >= 0; i--)
            {
                WeakReference reference = this.keys[i];
                TKey target = (TKey) reference.Target;
                if (target == null)
                {
                    this.keys.RemoveAt(i);
                    this.values.RemoveAt(i);
                    num--;
                }
                else if (Equals(target, key))
                {
                    num = i;
                }
            }
            return ((num < 0) ? -1 : num);
        }

        [IteratorStateMachine(typeof(<GetEnumeratorCore>d__19))]
        private IEnumerator<KeyValuePair<TKey, TValue>> GetEnumeratorCore()
        {
            <GetEnumeratorCore>d__19<TKey, TValue> d__1 = new <GetEnumeratorCore>d__19<TKey, TValue>(0);
            d__1.<>4__this = (WeakKeyDictionary<TKey, TValue>) this;
            return d__1;
        }

        private void Insert(TKey key, TValue value, bool add)
        {
            if (key == null)
            {
                throw new ArgumentException();
            }
            int num = this.FindEntry(key);
            if (num < 0)
            {
                this.keys.Add(new WeakReference(key));
                this.values.Add(value);
            }
            else
            {
                if (add)
                {
                    throw new ArgumentException();
                }
                this.values[num] = value;
            }
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentException();
            }
            int index = this.FindEntry(key);
            if (index < 0)
            {
                return false;
            }
            this.keys.RemoveAt(index);
            this.values.RemoveAt(index);
            return true;
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => 
            this.GetEnumeratorCore();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumeratorCore();

        public bool TryGetValue(TKey key, out TValue value)
        {
            int num = this.FindEntry(key);
            if (num >= 0)
            {
                value = this.values[num];
                return true;
            }
            value = default(TValue);
            return false;
        }

        public IList Keys =>
            this.keys;

        public TValue this[TKey key]
        {
            get
            {
                int num = this.FindEntry(key);
                if (num < 0)
                {
                    throw new KeyNotFoundException();
                }
                return this.values[num];
            }
            set => 
                this.Insert(key, value, false);
        }

        public int Count =>
            this.keys.Count;

        [CompilerGenerated]
        private sealed class <GetEnumeratorCore>d__19 : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<TKey, TValue> <>2__current;
            public WeakKeyDictionary<TKey, TValue> <>4__this;
            private int <i>5__1;

            [DebuggerHidden]
            public <GetEnumeratorCore>d__19(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<i>5__1 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    goto TR_0007;
                }
            TR_0005:
                if (this.<i>5__1 >= this.<>4__this.Count)
                {
                    return false;
                }
                TKey target = (TKey) this.<>4__this.keys[this.<i>5__1].Target;
                if (target != null)
                {
                    this.<>2__current = new KeyValuePair<TKey, TValue>(target, this.<>4__this.values[this.<i>5__1]);
                    this.<>1__state = 1;
                    return true;
                }
            TR_0007:
                while (true)
                {
                    int num2 = this.<i>5__1;
                    this.<i>5__1 = num2 + 1;
                    break;
                }
                goto TR_0005;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

