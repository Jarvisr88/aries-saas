namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DXWebControlCollection : ICollection, IEnumerable
    {
        private List<DXWebControlBase> controls;
        private int growthFactor;
        private DXWebControlBase owner;
        private string readOnlyErrorMsg;
        private int version;

        public DXWebControlCollection(DXWebControlBase owner)
        {
            this.controls = new List<DXWebControlBase>(5);
            this.growthFactor = 4;
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }
            this.owner = owner;
        }

        internal DXWebControlCollection(DXWebControlBase owner, int growthFactor)
        {
            this.controls = new List<DXWebControlBase>(5);
            Guard.ArgumentNotNull(owner, "owner");
            growthFactor = 4;
            this.owner = owner;
            this.growthFactor = growthFactor;
        }

        public virtual void Add(DXWebControlBase child)
        {
            Guard.ArgumentNotNull(child, "child");
            if (this.readOnlyErrorMsg != null)
            {
                throw new Exception(this.readOnlyErrorMsg);
            }
            this.controls.Add(child);
            this.version++;
            this.owner.AddedControl(child, this.controls.Count - 1);
        }

        public virtual void AddAt(int index, DXWebControlBase child)
        {
            if (index == -1)
            {
                this.Add(child);
            }
            else
            {
                Guard.ArgumentNotNull(child, "child");
                if ((index < 0) || (index > this.controls.Count))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                if (this.readOnlyErrorMsg != null)
                {
                    throw new Exception(this.readOnlyErrorMsg);
                }
                this.controls.Insert(index, child);
                this.version++;
                this.owner.AddedControl(child, index);
            }
        }

        public virtual void Clear()
        {
            if (this.controls != null)
            {
                for (int i = this.controls.Count - 1; i >= 0; i--)
                {
                    this.RemoveAt(i);
                }
            }
        }

        public virtual bool Contains(DXWebControlBase c) => 
            (c != null) && this.controls.Contains(c);

        public virtual void CopyTo(Array array, int index)
        {
            if (!(array is DXWebControlBase[]) || (array.Rank != 1))
            {
                throw new Exception("InvalidArgumentValue");
            }
            Array.Copy(this.controls.ToArray(), 0, array, index, this.controls.Count);
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__24))]
        public virtual IEnumerator GetEnumerator()
        {
            <GetEnumerator>d__24 d__1 = new <GetEnumerator>d__24(0);
            d__1.<>4__this = this;
            return d__1;
        }

        public virtual int IndexOf(DXWebControlBase value) => 
            this.controls.IndexOf(value);

        public virtual void Remove(DXWebControlBase value)
        {
            this.controls.Remove(value);
        }

        public virtual void RemoveAt(int index)
        {
            if (this.readOnlyErrorMsg != null)
            {
                throw new Exception(this.readOnlyErrorMsg);
            }
            DXWebControlBase control = this[index];
            this.controls.RemoveAt(index);
            this.version++;
            this.owner.RemovedControl(control);
        }

        internal string SetCollectionReadOnly(string errorMsg)
        {
            string readOnlyErrorMsg = this.readOnlyErrorMsg;
            this.readOnlyErrorMsg = errorMsg;
            return readOnlyErrorMsg;
        }

        public virtual int Count =>
            this.controls.Count;

        public bool IsReadOnly =>
            this.readOnlyErrorMsg != null;

        public bool IsSynchronized =>
            false;

        public virtual DXWebControlBase this[int index]
        {
            get
            {
                if ((index < 0) || (index >= this.controls.Count))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return this.controls[index];
            }
        }

        protected DXWebControlBase Owner =>
            this.owner;

        public object SyncRoot =>
            this;

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__24 : IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            public DXWebControlCollection <>4__this;
            private int <i>5__1;

            [DebuggerHidden]
            public <GetEnumerator>d__24(int <>1__state)
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
                    int num2 = this.<i>5__1;
                    this.<i>5__1 = num2 + 1;
                }
                if (this.<i>5__1 >= this.<>4__this.Count)
                {
                    return false;
                }
                this.<>2__current = this.<>4__this[this.<i>5__1];
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

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

