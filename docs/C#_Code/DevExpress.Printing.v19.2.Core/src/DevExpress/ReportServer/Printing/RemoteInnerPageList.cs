namespace DevExpress.ReportServer.Printing
{
    using DevExpress.ReportServer.Printing.Services;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class RemoteInnerPageList : IList<Page>, ICollection<Page>, IEnumerable<Page>, IEnumerable, IDisposable
    {
        private readonly IPageListService pageListService;

        public event EventHandler<EventArgs> PageCountChanged;

        public RemoteInnerPageList(IPageListService pageListService)
        {
            if (pageListService == null)
            {
                throw new ArgumentNullException();
            }
            this.pageListService = pageListService;
        }

        public void Clear()
        {
            this.pageListService.Clear();
        }

        public bool Contains(Page item)
        {
            bool flag;
            using (IEnumerator enumerator = this.pageListService.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        if (!current.Equals(item))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public void CopyTo(Page[] array, int arrayIndex)
        {
            for (int i = 0; i < this.pageListService.Capacity; i++)
            {
                array.SetValue(this.pageListService[i], (int) (i + arrayIndex));
            }
        }

        public void Dispose()
        {
            this.Clear();
            this.IsDisposed = true;
        }

        public IEnumerator GetEnumerator() => 
            this.pageListService.GetEnumerator();

        public int IndexOf(Page item)
        {
            for (int i = 0; i < this.pageListService.Capacity; i++)
            {
                if (this.pageListService[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }

        private void RaisePageCountChanged()
        {
            if (this.PageCountChanged != null)
            {
                this.PageCountChanged(this, EventArgs.Empty);
            }
        }

        internal void ReplaceCachedPage(int i, Page page)
        {
            this.pageListService[i] = page;
        }

        public void SetCount(int value)
        {
            if (!this.IsDisposed && this.pageListService.EnlargeCapacity(value))
            {
                this.RaisePageCountChanged();
            }
        }

        void ICollection<Page>.Add(Page item)
        {
            throw new NotSupportedException();
        }

        bool ICollection<Page>.Remove(Page item)
        {
            throw new NotSupportedException();
        }

        [IteratorStateMachine(typeof(<System-Collections-Generic-IEnumerable<DevExpress-XtraPrinting-Page>-GetEnumerator>d__27))]
        IEnumerator<Page> IEnumerable<Page>.GetEnumerator()
        {
            <System-Collections-Generic-IEnumerable<DevExpress-XtraPrinting-Page>-GetEnumerator>d__27 d__1 = new <System-Collections-Generic-IEnumerable<DevExpress-XtraPrinting-Page>-GetEnumerator>d__27(0);
            d__1.<>4__this = this;
            return d__1;
        }

        void IList<Page>.Insert(int index, Page item)
        {
            throw new NotSupportedException();
        }

        void IList<Page>.RemoveAt(int index)
        {
            this.pageListService.RemoveAt(index);
        }

        private bool IsDisposed { get; set; }

        public Page this[int index]
        {
            get => 
                this.pageListService[index];
            set
            {
                throw new NotSupportedException();
            }
        }

        public int Count =>
            this.pageListService.Capacity;

        bool ICollection<Page>.IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [CompilerGenerated]
        private sealed class <System-Collections-Generic-IEnumerable<DevExpress-XtraPrinting-Page>-GetEnumerator>d__27 : IEnumerator<Page>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Page <>2__current;
            public RemoteInnerPageList <>4__this;
            private IEnumerator <enumerator>5__1;

            [DebuggerHidden]
            public <System-Collections-Generic-IEnumerable<DevExpress-XtraPrinting-Page>-GetEnumerator>d__27(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<enumerator>5__1 = this.<>4__this.GetEnumerator();
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                if (!this.<enumerator>5__1.MoveNext())
                {
                    return false;
                }
                this.<>2__current = (Page) this.<enumerator>5__1.Current;
                this.<>1__state = 1;
                return true;
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

            Page IEnumerator<Page>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

