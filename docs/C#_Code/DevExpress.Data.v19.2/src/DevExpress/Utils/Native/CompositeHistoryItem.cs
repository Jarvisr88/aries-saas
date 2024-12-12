namespace DevExpress.Utils.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class CompositeHistoryItem : DisposableObject, IHistoryItem, IDisposable
    {
        private List<IHistoryItem> historyItems = new List<IHistoryItem>();

        void IHistoryItem.Redo()
        {
            foreach (IHistoryItem item in this.HistoryItems)
            {
                item.Redo();
            }
        }

        void IHistoryItem.Undo()
        {
            foreach (IHistoryItem item in this.UndoHistoryItems)
            {
                item.Undo();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IHistoryItem item in this.HistoryItems)
                {
                    item.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("CompositeHistoryItem (");
            bool flag = false;
            foreach (IHistoryItem item in this.historyItems)
            {
                if (flag)
                {
                    builder.Append(",");
                }
                builder.Append(item.ToString());
                flag = true;
            }
            builder.Append(")");
            return builder.ToString();
        }

        internal List<IHistoryItem> HistoryItems =>
            this.historyItems;

        private IEnumerable<IHistoryItem> UndoHistoryItems =>
            new <get_UndoHistoryItems>d__4(-2) { <>4__this=this };

        public object ObjectToSelect { get; set; }

        object IHistoryItem.ObjectToSelect =>
            this.ObjectToSelect;

        [CompilerGenerated]
        private sealed class <get_UndoHistoryItems>d__4 : IEnumerable<IHistoryItem>, IEnumerable, IEnumerator<IHistoryItem>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IHistoryItem <>2__current;
            private int <>l__initialThreadId;
            public CompositeHistoryItem <>4__this;
            private int <i>5__1;

            [DebuggerHidden]
            public <get_UndoHistoryItems>d__4(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<i>5__1 = this.<>4__this.historyItems.Count - 1;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    int num2 = this.<i>5__1;
                    this.<i>5__1 = num2 - 1;
                }
                if (this.<i>5__1 < 0)
                {
                    return false;
                }
                this.<>2__current = this.<>4__this.historyItems[this.<i>5__1];
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<IHistoryItem> IEnumerable<IHistoryItem>.GetEnumerator()
            {
                CompositeHistoryItem.<get_UndoHistoryItems>d__4 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new CompositeHistoryItem.<get_UndoHistoryItems>d__4(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Utils.Native.IHistoryItem>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            IHistoryItem IEnumerator<IHistoryItem>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

