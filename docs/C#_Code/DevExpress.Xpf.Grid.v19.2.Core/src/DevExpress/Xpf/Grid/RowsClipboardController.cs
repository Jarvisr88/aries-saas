namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class RowsClipboardController
    {
        private DataViewBase view;

        protected RowsClipboardController(DataViewBase view)
        {
            this.view = view;
        }

        protected static void AppendIndent(StringBuilder sb, KeyValuePair<DataControlBase, int> row, List<DataControlBase> printableDataControls)
        {
            int num = printableDataControls.Intersect<DataControlBase>(GetThisAndParentDataControls(row.Key)).Count<DataControlBase>();
            sb.Append('\t', num - 1);
        }

        protected virtual bool CanAddRowToSelectedData(DataControlBase dataControl, int rowHandle) => 
            true;

        public void CopyRangeToClipboard(int startRowHandle, int endRowHandle)
        {
            if ((startRowHandle != -2147483648) && (endRowHandle != -2147483648))
            {
                int rowVisibleIndexByHandle = this.view.DataProviderBase.GetRowVisibleIndexByHandle(startRowHandle);
                int num2 = this.view.DataProviderBase.GetRowVisibleIndexByHandle(endRowHandle);
                if ((rowVisibleIndexByHandle >= 0) && (num2 >= 0))
                {
                    if (rowVisibleIndexByHandle > num2)
                    {
                        num2 = rowVisibleIndexByHandle;
                        rowVisibleIndexByHandle = num2;
                    }
                    int[] rows = new int[(num2 - rowVisibleIndexByHandle) + 1];
                    for (int i = rowVisibleIndexByHandle; i <= num2; i++)
                    {
                        rows[i - rowVisibleIndexByHandle] = this.view.DataControl.GetRowHandleByVisibleIndexCore(i);
                    }
                    this.CopyRowsToClipboard(rows);
                }
            }
        }

        internal void CopyRowsToClipboard(IEnumerable<KeyValuePair<DataControlBase, int>> rows)
        {
            List<int> rowHandles = null;
            if (!this.IsMasterDetail())
            {
                Func<KeyValuePair<DataControlBase, int>, int> selector = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<KeyValuePair<DataControlBase, int>, int> local1 = <>c.<>9__6_0;
                    selector = <>c.<>9__6_0 = row => row.Value;
                }
                rowHandles = rows.Select<KeyValuePair<DataControlBase, int>, int>(selector).ToList<int>();
            }
            this.CopyToClipboard(() => this.CreateRowsCopyingToClipboardEventArgs(rowHandles), new RowsClipboardDataProvider(rows, this));
        }

        public void CopyRowsToClipboard(IEnumerable<int> rows)
        {
            IEnumerable<KeyValuePair<DataControlBase, int>> enumerable2 = from x in this.SortRowHandlesByVisibleIndex(rows) select new KeyValuePair<DataControlBase, int>(this.View.DataControl, x);
            this.CopyToClipboard(() => this.CreateRowsCopyingToClipboardEventArgs(rows), new RowsClipboardDataProvider(enumerable2, this));
        }

        protected void CopyToClipboard(Func<CopyingToClipboardEventArgsBase> createClipboardCopyingEventArgs, IClipboardDataProvider clipboardDataProvider)
        {
            if (((this.view.ActualClipboardCopyAllowed && !this.view.DataControl.RaiseCopyingToClipboard(createClipboardCopyingEventArgs())) && !this.view.RaiseCopyingToClipboard(createClipboardCopyingEventArgs())) && !this.view.SetDataAwareClipboardData(clipboardDataProvider as IClipboardFormatted, this.view.DataControl.CallCopyClipboardFromCommand))
            {
                DXClipboard.SetDataFromClipboardDataProvider(clipboardDataProvider);
            }
        }

        protected abstract CopyingToClipboardEventArgsBase CreateRowsCopyingToClipboardEventArgs(IEnumerable<int> rows);
        protected virtual int GetCountCopyRows(IEnumerable<KeyValuePair<DataControlBase, int>> rows) => 
            rows.Count<KeyValuePair<DataControlBase, int>>();

        [IteratorStateMachine(typeof(<GetDataAndHeaderRows>d__11))]
        private IEnumerable<KeyValuePair<DataControlBase, int>> GetDataAndHeaderRows(IEnumerable<KeyValuePair<DataControlBase, int>> rows, List<DataControlBase> printableDataControls)
        {
            KeyValuePair<DataControlBase, int> current;
            IEnumerator<KeyValuePair<DataControlBase, int>> <>7__wrap2;
            DataControlBase dataControl = null;
            IEnumerator<KeyValuePair<DataControlBase, int>> enumerator = rows.GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                current = enumerator.Current;
                if (ReferenceEquals(this.GetOriginationDataControl(dataControl), this.GetOriginationDataControl(current.Key)) || !this.View.ActualClipboardCopyWithHeaders)
                {
                    goto TR_0004;
                }
                else
                {
                    List<DataControlBase> second = GetThisAndParentDataControls(dataControl);
                    <>7__wrap2 = GetHeadersForRow(current, printableDataControls.Except<DataControlBase>(second)).GetEnumerator();
                }
            }
            else
            {
                enumerator = null;
            }
        TR_0007:
            if (<>7__wrap2.MoveNext())
            {
                KeyValuePair<DataControlBase, int> current = <>7__wrap2.Current;
                yield return current;
                goto TR_0007;
            }
            else
            {
                <>7__wrap2 = null;
            }
        TR_0004:
            dataControl = current.Key;
            yield return current;
            current = new KeyValuePair<DataControlBase, int>();
            goto Label_PostSwitchInIterator;
        }

        private static KeyValuePair<DataControlBase, int> GetHeaderRow(DataControlBase dataControl) => 
            new KeyValuePair<DataControlBase, int>(dataControl, -2147483648);

        [IteratorStateMachine(typeof(<GetHeadersForRow>d__14))]
        private static IEnumerable<KeyValuePair<DataControlBase, int>> GetHeadersForRow(KeyValuePair<DataControlBase, int> row, IEnumerable<DataControlBase> printableDataControls)
        {
            <GetHeadersForRow>d__14 d__1 = new <GetHeadersForRow>d__14(-2);
            d__1.<>3__row = row;
            d__1.<>3__printableDataControls = printableDataControls;
            return d__1;
        }

        protected void GetHeadersText(StringBuilder sb)
        {
            this.view.GetDataRowText(sb, -2147483648);
        }

        private List<int> GetListVisibleIndexFromArrayRowsHandle(IEnumerable<int> rows)
        {
            List<int> list = new List<int>();
            foreach (int num in rows)
            {
                int rowVisibleIndexByHandle = this.view.DataProviderBase.GetRowVisibleIndexByHandle(num);
                if (rowVisibleIndexByHandle >= 0)
                {
                    list.Add(rowVisibleIndexByHandle);
                }
            }
            list.Sort();
            return list;
        }

        private DataControlBase GetOriginationDataControl(DataControlBase dataControl) => 
            dataControl?.GetOriginationDataControl();

        protected virtual object GetSelectedData(IEnumerable<KeyValuePair<DataControlBase, int>> rows)
        {
            if (rows == null)
            {
                return null;
            }
            ArrayList list = new ArrayList();
            int countCopyRows = this.GetCountCopyRows(rows);
            foreach (KeyValuePair<DataControlBase, int> pair in rows)
            {
                if (countCopyRows-- <= 0)
                {
                    break;
                }
                if (this.CanAddRowToSelectedData(pair.Key, pair.Value))
                {
                    list.Add(pair.Key.DataProviderBase.GetRowValue(pair.Value));
                }
            }
            return list;
        }

        protected object GetSelectedData(IEnumerable<int> rows) => 
            (rows != null) ? this.GetSelectedData((IEnumerable<KeyValuePair<DataControlBase, int>>) (from row in rows select new KeyValuePair<DataControlBase, int>(this.View.DataControl, row))) : null;

        protected string GetTextInRows(IEnumerable<KeyValuePair<DataControlBase, int>> rows)
        {
            StringBuilder sb = new StringBuilder();
            int countCopyRows = this.GetCountCopyRows(rows);
            Func<KeyValuePair<DataControlBase, int>, DataControlBase> selector = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<KeyValuePair<DataControlBase, int>, DataControlBase> local1 = <>c.<>9__9_0;
                selector = <>c.<>9__9_0 = row => row.Key;
            }
            List<DataControlBase> printableDataControls = rows.Select<KeyValuePair<DataControlBase, int>, DataControlBase>(selector).Distinct<DataControlBase>().ToList<DataControlBase>();
            IEnumerable<KeyValuePair<DataControlBase, int>> dataAndHeaderRows = this.GetDataAndHeaderRows(rows.Take<KeyValuePair<DataControlBase, int>>(countCopyRows), printableDataControls);
            return this.GetTextInRowsCore(dataAndHeaderRows, sb, printableDataControls, countCopyRows);
        }

        protected virtual string GetTextInRowsCore(IEnumerable<KeyValuePair<DataControlBase, int>> actualRows, StringBuilder sb, List<DataControlBase> printableDataControls, int countCopyRows)
        {
            foreach (KeyValuePair<DataControlBase, int> pair in actualRows)
            {
                if (sb.Length > 0)
                {
                    sb.Append(Environment.NewLine);
                }
                AppendIndent(sb, pair, printableDataControls);
                pair.Key.DataView.GetDataRowText(sb, pair.Value);
            }
            if ((countCopyRows == 0) && this.View.ActualClipboardCopyWithHeaders)
            {
                this.GetHeadersText(sb);
            }
            return sb.ToString();
        }

        private static List<DataControlBase> GetThisAndParentDataControls(DataControlBase dataControl)
        {
            List<DataControlBase> parents = new List<DataControlBase>();
            if (dataControl != null)
            {
                dataControl.EnumerateThisAndParentDataControls(delegate (DataControlBase parent) {
                    parents.Add(parent);
                });
            }
            return parents;
        }

        internal bool IsMasterDetail() => 
            (this.View.RootView.DataControl.DetailDescriptorCore != null) && this.View.RootView.AllowMasterDetailCore;

        private IEnumerable<int> SortRowHandlesByVisibleIndex(IEnumerable<int> rows) => 
            from x in this.GetListVisibleIndexFromArrayRowsHandle(rows) select this.View.DataControl.GetRowHandleByVisibleIndexCore(x);

        protected DataViewBase View =>
            this.view;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RowsClipboardController.<>c <>9 = new RowsClipboardController.<>c();
            public static Func<KeyValuePair<DataControlBase, int>, int> <>9__6_0;
            public static Func<KeyValuePair<DataControlBase, int>, DataControlBase> <>9__9_0;

            internal int <CopyRowsToClipboard>b__6_0(KeyValuePair<DataControlBase, int> row) => 
                row.Value;

            internal DataControlBase <GetTextInRows>b__9_0(KeyValuePair<DataControlBase, int> row) => 
                row.Key;
        }


        [CompilerGenerated]
        private sealed class <GetHeadersForRow>d__14 : IEnumerable<KeyValuePair<DataControlBase, int>>, IEnumerable, IEnumerator<KeyValuePair<DataControlBase, int>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<DataControlBase, int> <>2__current;
            private int <>l__initialThreadId;
            private KeyValuePair<DataControlBase, int> row;
            public KeyValuePair<DataControlBase, int> <>3__row;
            private IEnumerable<DataControlBase> printableDataControls;
            public IEnumerable<DataControlBase> <>3__printableDataControls;
            private IEnumerator<DataControlBase> <>7__wrap1;

            [DebuggerHidden]
            public <GetHeadersForRow>d__14(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
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
                        List<DataControlBase> thisAndParentDataControls = RowsClipboardController.GetThisAndParentDataControls(this.row.Key);
                        this.<>7__wrap1 = thisAndParentDataControls.Reverse<DataControlBase>().GetEnumerator();
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
                    while (true)
                    {
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            DataControlBase current = this.<>7__wrap1.Current;
                            if (!this.printableDataControls.Contains<DataControlBase>(current))
                            {
                                continue;
                            }
                            this.<>2__current = RowsClipboardController.GetHeaderRow(current);
                            this.<>1__state = 1;
                            flag = true;
                        }
                        break;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<KeyValuePair<DataControlBase, int>> IEnumerable<KeyValuePair<DataControlBase, int>>.GetEnumerator()
            {
                RowsClipboardController.<GetHeadersForRow>d__14 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new RowsClipboardController.<GetHeadersForRow>d__14(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.row = this.<>3__row;
                d__.printableDataControls = this.<>3__printableDataControls;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<DevExpress.Xpf.Grid.DataControlBase,System.Int32>>.GetEnumerator();

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

            KeyValuePair<DataControlBase, int> IEnumerator<KeyValuePair<DataControlBase, int>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        public class RowsClipboardDataProvider : IClipboardDataProvider, IClipboardFormatted
        {
            private IEnumerable<KeyValuePair<DataControlBase, int>> rows;
            private RowsClipboardController owner;

            public RowsClipboardDataProvider(IEnumerable<KeyValuePair<DataControlBase, int>> rows, RowsClipboardController owner)
            {
                this.rows = rows;
                this.owner = owner;
            }

            int IClipboardFormatted.GetSelectedCellsCount(DataControlBase dataControl) => 
                (this.rows != null) ? ((from x in this.rows
                    where x.Key == dataControl
                    select x).Count<KeyValuePair<DataControlBase, int>>() * ((IClipboardFormatted) this).GetSelectedColumns(dataControl).Count<ColumnBase>()) : 0;

            IEnumerable<ColumnBase> IClipboardFormatted.GetSelectedColumns(DataControlBase dataControl) => 
                ((dataControl == null) || ((dataControl.DataView == null) || (dataControl.DataView.VisibleColumnsCore == null))) ? new List<ColumnBase>() : dataControl.DataView.VisibleColumnsCore.Cast<ColumnBase>().ToList<ColumnBase>();

            IEnumerable<int> IClipboardFormatted.GetSelectedRows(DataControlBase dataControl)
            {
                if (this.rows == null)
                {
                    return new List<int>();
                }
                Func<KeyValuePair<DataControlBase, int>, int> selector = <>c.<>9__7_1;
                if (<>c.<>9__7_1 == null)
                {
                    Func<KeyValuePair<DataControlBase, int>, int> local1 = <>c.<>9__7_1;
                    selector = <>c.<>9__7_1 = x => x.Value;
                }
                return (from x in this.rows
                    where x.Key == dataControl
                    select x).Select<KeyValuePair<DataControlBase, int>, int>(selector).Distinct<int>().ToList<int>();
            }

            bool IClipboardFormatted.IsSelect(int rowHanle, ColumnBase column, DataControlBase dataControl) => 
                (this.rows != null) ? (this.rows.FirstOrDefault<KeyValuePair<DataControlBase, int>>(x => ((x.Key == dataControl) && (x.Value == rowHanle))).Key != null) : false;

            public object GetObjectFromClipboard() => 
                this.owner.GetSelectedData(this.rows);

            public string GetTextFromClipboard() => 
                this.owner.GetTextInRows(this.rows);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly RowsClipboardController.RowsClipboardDataProvider.<>c <>9 = new RowsClipboardController.RowsClipboardDataProvider.<>c();
                public static Func<KeyValuePair<DataControlBase, int>, int> <>9__7_1;

                internal int <DevExpress.Xpf.Grid.Native.IClipboardFormatted.GetSelectedRows>b__7_1(KeyValuePair<DataControlBase, int> x) => 
                    x.Value;
            }
        }
    }
}

