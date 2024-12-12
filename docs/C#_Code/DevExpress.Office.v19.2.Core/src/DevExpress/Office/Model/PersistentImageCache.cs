namespace DevExpress.Office.Model
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class PersistentImageCache : ImageCacheBase
    {
        private readonly Dictionary<IUniqueImageId, OfficeNativeImage> imagesByIds;
        private readonly Dictionary<int, OfficeNativeImage> imagesByKeys;

        public PersistentImageCache(IDocumentModel owner) : base(owner)
        {
            this.imagesByIds = new Dictionary<IUniqueImageId, OfficeNativeImage>();
            this.imagesByKeys = new Dictionary<int, OfficeNativeImage>();
        }

        protected override void AddImage(OfficeNativeImage image, int key)
        {
            this.imagesByKeys.Add(key, image);
            this.imagesByIds.Add(image.Id, image);
        }

        public override void Clear()
        {
            foreach (IUniqueImageId id in this.imagesByIds.Keys)
            {
                this.imagesByIds[id].Dispose();
            }
            this.imagesByIds.Clear();
            foreach (int num in this.imagesByKeys.Keys)
            {
                this.imagesByKeys[num].Dispose();
            }
            this.imagesByKeys.Clear();
            base.Clear();
        }

        [IteratorStateMachine(typeof(<GetEnumeratorByIds>d__7))]
        public override IEnumerator<KeyValuePair<int, OfficeNativeImage>> GetEnumeratorByIds()
        {
            <GetEnumeratorByIds>d__7 d__1 = new <GetEnumeratorByIds>d__7(0);
            d__1.<>4__this = this;
            return d__1;
        }

        protected override OfficeNativeImage GetNativeImageById(IUniqueImageId id)
        {
            OfficeNativeImage image;
            return (!this.imagesByIds.TryGetValue(id, out image) ? null : image);
        }

        protected override OfficeNativeImage GetNativeImageByKey(int imageId)
        {
            OfficeNativeImage image;
            return (!this.imagesByKeys.TryGetValue(imageId, out image) ? null : image);
        }

        [CompilerGenerated]
        private sealed class <GetEnumeratorByIds>d__7 : IEnumerator<KeyValuePair<int, OfficeNativeImage>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<int, OfficeNativeImage> <>2__current;
            public PersistentImageCache <>4__this;
            private Dictionary<int, OfficeNativeImage>.Enumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumeratorByIds>d__7(int <>1__state)
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
                        this.<>7__wrap1 = new Dictionary<int, OfficeNativeImage>.Enumerator();
                        flag = false;
                    }
                    else
                    {
                        KeyValuePair<int, OfficeNativeImage> current = this.<>7__wrap1.Current;
                        this.<>2__current = current;
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

