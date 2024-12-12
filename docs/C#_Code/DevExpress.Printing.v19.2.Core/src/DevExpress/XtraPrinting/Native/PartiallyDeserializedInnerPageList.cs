namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class PartiallyDeserializedInnerPageList : IList<Page>, ICollection<Page>, IEnumerable<Page>, IEnumerable
    {
        private LimitedDeque<Page> buffer = new LimitedDeque<Page>(0x100);
        private List<WeakPage> list = new List<WeakPage>();
        private PartiallyDeserializedDocument document;

        public PartiallyDeserializedInnerPageList(PartiallyDeserializedDocument document)
        {
            this.document = document;
        }

        public void Add(Page page)
        {
            this.list.Add(new WeakPage(page));
        }

        public void Clear()
        {
            this.list = new List<WeakPage>();
        }

        public void ClearPageBuffer()
        {
            this.buffer = new LimitedDeque<Page>(this.buffer.Capacity);
        }

        public bool Contains(Page page) => 
            this.document.Storage.PageDataList.GetPageIndex(page.ID) >= 0;

        public void CopyTo(Page[] array, int arrayIndex)
        {
            for (int i = 0; i < this.Count; i++)
            {
                array[i] = this[i];
            }
        }

        public void Expand(int count)
        {
            for (int i = 0; i < count; i++)
            {
                WeakPage item = new WeakPage();
                this.list.Add(item);
            }
        }

        public IEnumerator<Page> GetEnumerator() => 
            new PartiallyDeserializedPageListEnumerator(this);

        public int IndexOf(Page item) => 
            item.Index;

        public void Insert(int index, Page item)
        {
            this.list.Insert(index, new WeakPage(item));
        }

        public void Push(Page page)
        {
            this.buffer.Push(page);
        }

        public bool Remove(Page item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        internal void SetPageBufferSize(int size)
        {
            this.buffer = new LimitedDeque<Page>(size);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public Page this[int index]
        {
            get
            {
                try
                {
                    WeakPage page = this.list[index];
                    Page page2 = page.GetPage(() => this.document.CreateEmptyPage(index));
                    this.list[index] = page;
                    return page2;
                }
                catch
                {
                    WeakPage page4 = new WeakPage();
                    return page4.GetPage(() => this.document.CreateEmptyPage(index));
                }
            }
            set
            {
                WeakPage page = this.list[index];
                page.SetPage(value);
                this.list[index] = page;
            }
        }

        public int Count =>
            this.list.Count;

        public bool IsReadOnly =>
            true;

        internal IEnumerable<Page> PageBuffer =>
            this.buffer;

        internal IEnumerable<Page> AlivePages
        {
            get
            {
                Func<WeakPage, Page> selector = <>c.<>9__15_0;
                if (<>c.<>9__15_0 == null)
                {
                    Func<WeakPage, Page> local1 = <>c.<>9__15_0;
                    selector = <>c.<>9__15_0 = w => w.Page;
                }
                Func<Page, bool> predicate = <>c.<>9__15_1;
                if (<>c.<>9__15_1 == null)
                {
                    Func<Page, bool> local2 = <>c.<>9__15_1;
                    predicate = <>c.<>9__15_1 = p => p != null;
                }
                return this.list.Select<WeakPage, Page>(selector).Where<Page>(predicate);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PartiallyDeserializedInnerPageList.<>c <>9 = new PartiallyDeserializedInnerPageList.<>c();
            public static Func<PartiallyDeserializedInnerPageList.WeakPage, Page> <>9__15_0;
            public static Func<Page, bool> <>9__15_1;

            internal Page <get_AlivePages>b__15_0(PartiallyDeserializedInnerPageList.WeakPage w) => 
                w.Page;

            internal bool <get_AlivePages>b__15_1(Page p) => 
                p != null;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WeakPage
        {
            private WeakReference reference;
            public WeakPage(DevExpress.XtraPrinting.Page page)
            {
                this.reference = new WeakReference(page);
            }

            public bool IsAlive =>
                (this.reference != null) && (this.reference.Target != null);
            public DevExpress.XtraPrinting.Page Page =>
                (this.reference != null) ? ((DevExpress.XtraPrinting.Page) this.reference.Target) : null;
            public DevExpress.XtraPrinting.Page GetPage(Func<DevExpress.XtraPrinting.Page> creator)
            {
                DevExpress.XtraPrinting.Page page = (this.reference != null) ? (this.reference.Target as DevExpress.XtraPrinting.Page) : null;
                if (page == null)
                {
                    page = creator();
                    this.reference ??= new WeakReference(null);
                    this.reference.Target = page;
                }
                return page;
            }

            public void SetPage(DevExpress.XtraPrinting.Page page)
            {
                this.reference ??= new WeakReference(null);
                this.reference.Target = page;
            }

            public override string ToString()
            {
                object target = this.reference?.Target;
                return ((target != null) ? target.ToString() : "{null}");
            }
        }
    }
}

