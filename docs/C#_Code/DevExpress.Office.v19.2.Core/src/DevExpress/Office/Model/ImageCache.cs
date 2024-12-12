namespace DevExpress.Office.Model
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class ImageCache : ImageCacheBase
    {
        private readonly Dictionary<IUniqueImageId, WeakReference> imagesByIds;
        private readonly Dictionary<int, WeakReference> imagesByKeys;

        public ImageCache(IDocumentModel owner) : base(owner)
        {
            this.imagesByIds = new Dictionary<IUniqueImageId, WeakReference>();
            this.imagesByKeys = new Dictionary<int, WeakReference>();
        }

        protected override void AddImage(OfficeNativeImage image, int key)
        {
            this.CleanDeadItems<int>(this.imagesByKeys);
            this.CleanDeadItems<IUniqueImageId>(this.imagesByIds);
            this.imagesByKeys.Add(key, new WeakReference(image));
            this.imagesByIds.Add(image.Id, new WeakReference(image));
        }

        protected internal virtual void CleanDeadItems<T>(Dictionary<T, WeakReference> dictionary)
        {
            List<T> list = new List<T>();
            foreach (T local in dictionary.Keys)
            {
                WeakReference reference = dictionary[local];
                OfficeImage target = (OfficeImage) reference.Target;
                if (target == null)
                {
                    list.Add(local);
                }
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                dictionary.Remove(list[i]);
            }
        }

        public override void Clear()
        {
            foreach (int num in this.imagesByKeys.Keys)
            {
                WeakReference reference = this.imagesByKeys[num];
                OfficeImage target = (OfficeImage) reference.Target;
                if (target != null)
                {
                    target.Dispose();
                }
            }
            this.imagesByKeys.Clear();
            foreach (IUniqueImageId id in this.imagesByIds.Keys)
            {
                WeakReference reference2 = this.imagesByIds[id];
                OfficeImage target = (OfficeImage) reference2.Target;
                if (target != null)
                {
                    target.Dispose();
                }
            }
            this.imagesByIds.Clear();
            base.Clear();
        }

        [IteratorStateMachine(typeof(<GetEnumeratorByIds>d__9))]
        public override IEnumerator<KeyValuePair<int, OfficeNativeImage>> GetEnumeratorByIds()
        {
            <GetEnumeratorByIds>d__9 d__1 = new <GetEnumeratorByIds>d__9(0);
            d__1.<>4__this = this;
            return d__1;
        }

        protected override OfficeNativeImage GetNativeImageById(IUniqueImageId id) => 
            this.GetNativeImageFromDictionary<IUniqueImageId>(this.imagesByIds, id);

        protected override OfficeNativeImage GetNativeImageByKey(int key) => 
            this.GetNativeImageFromDictionary<int>(this.imagesByKeys, key);

        protected OfficeNativeImage GetNativeImageFromDictionary<T>(Dictionary<T, WeakReference> dict, T id)
        {
            WeakReference reference;
            if (!dict.TryGetValue(id, out reference))
            {
                return null;
            }
            OfficeNativeImage target = (OfficeNativeImage) reference.Target;
            if (target == null)
            {
                dict.Remove(id);
                return null;
            }
            this.CleanDeadItems<T>(dict);
            return target;
        }

        [CompilerGenerated]
        private sealed class <GetEnumeratorByIds>d__9 : IEnumerator<KeyValuePair<int, OfficeNativeImage>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<int, OfficeNativeImage> <>2__current;
            public ImageCache <>4__this;
            private Dictionary<int, WeakReference>.Enumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumeratorByIds>d__9(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                this.<>7__wrap1.Dispose();
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<>7__wrap1 = this.<>4__this.imagesByKeys.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = new Dictionary<int, WeakReference>.Enumerator();
                        flag = false;
                    }
                    else
                    {
                        KeyValuePair<int, WeakReference> current = this.<>7__wrap1.Current;
                        this.<>2__current = new KeyValuePair<int, OfficeNativeImage>(current.Key, this.<>4__this.GetNativeImageByKey(current.Key));
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            KeyValuePair<int, OfficeNativeImage> IEnumerator<KeyValuePair<int, OfficeNativeImage>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

