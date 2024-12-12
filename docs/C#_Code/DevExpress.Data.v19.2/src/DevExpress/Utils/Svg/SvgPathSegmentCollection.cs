namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class SvgPathSegmentCollection : CollectionBase
    {
        private int lockUpdateCore;

        public event CollectionChangeEventHandler CollectionChanged;

        public SvgPathSegmentCollection()
        {
            SvgPoint point = new SvgPoint();
            this.LastPoint = point;
            this.lockUpdateCore = 0;
        }

        public virtual bool Add(SvgPathSegment segment) => 
            this.AddSegment(segment);

        public virtual void AddRange(SvgPathSegment[] segments)
        {
            this.BeginUpdate();
            try
            {
                foreach (SvgPathSegment segment in segments)
                {
                    this.AddSegment(segment);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        private bool AddSegment(SvgPathSegment segment)
        {
            base.List.Add(segment);
            return true;
        }

        public virtual void BeginUpdate()
        {
            this.lockUpdateCore++;
        }

        protected bool CanAdd(SvgPathSegment element) => 
            true;

        public virtual void CancelUpdate()
        {
            this.lockUpdateCore--;
        }

        public SvgPathSegment[] CleanUp()
        {
            SvgPathSegment[] elements = this.ToArray();
            this.RemoveRange(elements);
            return elements;
        }

        public bool Contains(SvgPathSegment element) => 
            base.List.Contains(element);

        public virtual bool Contains(object element) => 
            base.List.Contains(element);

        public void CopyTo(Array target, int index)
        {
            base.List.CopyTo(target, index);
        }

        public virtual void EndUpdate()
        {
            int num = this.lockUpdateCore - 1;
            this.lockUpdateCore = num;
            if (num == 0)
            {
                this.RaiseCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
            }
        }

        public SvgPathSegment FindFirst(Predicate<SvgPathSegment> match)
        {
            SvgPathSegment segment2;
            using (IEnumerator enumerator = base.List.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        SvgPathSegment current = (SvgPathSegment) enumerator.Current;
                        if (!match(current))
                        {
                            continue;
                        }
                        segment2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return segment2;
        }

        public void ForEach(Action<SvgPathSegment> action)
        {
            foreach (SvgPathSegment segment in base.InnerList)
            {
                action(segment);
            }
        }

        [IteratorStateMachine(typeof(<GetTypedEnumerator>d__27))]
        protected internal IEnumerable<SvgPathSegment> GetTypedEnumerator()
        {
            <GetTypedEnumerator>d__27 d__1 = new <GetTypedEnumerator>d__27(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        public virtual int IndexOf(SvgPathSegment segment) => 
            base.List.IndexOf(segment);

        public virtual bool Insert(int index, SvgPathSegment segment)
        {
            base.List.Insert(index, segment);
            return true;
        }

        protected override void OnClear()
        {
            base.OnClear();
            if (base.Count != 0)
            {
                this.BeginUpdate();
                try
                {
                    for (int i = base.Count - 1; i >= 0; i--)
                    {
                        base.RemoveAt(i);
                    }
                }
                finally
                {
                    this.EndUpdate();
                }
            }
        }

        protected virtual void OnElementAdded(SvgPathSegment segment)
        {
            if (segment is SvgPathCloseSegment)
            {
                for (int i = base.Count - 1; i >= 0; i--)
                {
                    if (base.InnerList[i] is SvgPathMoveToSegment)
                    {
                        segment = base.InnerList[i] as SvgPathSegment;
                        break;
                    }
                }
            }
            this.Last = segment;
            this.LastPoint = segment.End;
        }

        protected virtual void OnElementRemoved(SvgPathSegment segment)
        {
        }

        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);
            this.OnElementAdded((SvgPathSegment) value);
            this.RaiseCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
            this.OnElementRemoved((SvgPathSegment) value);
            this.RaiseCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, value));
        }

        protected internal void RaiseCollectionChanged(CollectionChangeEventArgs e)
        {
            if ((this.lockUpdateCore == 0) && (this.CollectionChanged != null))
            {
                this.CollectionChanged(this, e);
            }
        }

        public virtual bool Remove(SvgPathSegment element)
        {
            if (!base.List.Contains(element))
            {
                return false;
            }
            base.List.Remove(element);
            return !base.List.Contains(element);
        }

        public void RemoveRange(SvgPathSegment[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                this.Remove(elements[i]);
            }
        }

        public SvgPathSegment[] ToArray() => 
            base.InnerList.ToArray(typeof(SvgPathSegment)) as SvgPathSegment[];

        public override string ToString() => 
            this.ToStringCore();

        protected virtual string ToStringCore() => 
            string.Empty;

        public SvgPathSegment Last { get; private set; }

        public SvgPoint LastPoint { get; private set; }

        public SvgPathSegment this[int index] =>
            (SvgPathSegment) base.List[index];

        [CompilerGenerated]
        private sealed class <GetTypedEnumerator>d__27 : IEnumerable<SvgPathSegment>, IEnumerable, IEnumerator<SvgPathSegment>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private SvgPathSegment <>2__current;
            private int <>l__initialThreadId;
            public SvgPathSegmentCollection <>4__this;
            private IEnumerator <>7__wrap1;

            [DebuggerHidden]
            public <GetTypedEnumerator>d__27(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                IDisposable disposable = this.<>7__wrap1 as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
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
                        this.<>7__wrap1 = this.<>4__this.List.GetEnumerator();
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
                        this.<>7__wrap1 = null;
                        flag = false;
                    }
                    else
                    {
                        object current = this.<>7__wrap1.Current;
                        this.<>2__current = current as SvgPathSegment;
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
            IEnumerator<SvgPathSegment> IEnumerable<SvgPathSegment>.GetEnumerator()
            {
                SvgPathSegmentCollection.<GetTypedEnumerator>d__27 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new SvgPathSegmentCollection.<GetTypedEnumerator>d__27(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Utils.Svg.SvgPathSegment>.GetEnumerator();

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

            SvgPathSegment IEnumerator<SvgPathSegment>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

