namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    internal abstract class RowExpandService : IAutoExpandService
    {
        private readonly DataViewBase view;
        private DateTime? rowTimestamp;
        private RowPointer currentRow;
        private Func<DateTime> timeProvider;

        public RowExpandService(DataViewBase view)
        {
            Guard.ArgumentNotNull(view, "view");
            this.view = view;
            Func<DateTime> func1 = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<DateTime> local1 = <>c.<>9__7_0;
                func1 = <>c.<>9__7_0 = () => DateTime.Now;
            }
            this.timeProvider = func1;
        }

        protected abstract void Expand(RowPointer rowPointer);
        private static double Subtract(DateTime time1, DateTime time2) => 
            time1.Subtract(time2).TotalMilliseconds;

        public void Update(DropInfo dropInfo)
        {
            if (!this.view.AutoExpandOnDrag)
            {
                this.currentRow = null;
                this.rowTimestamp = null;
            }
            else
            {
                DateTime time = this.TimeProvider();
                if (!dropInfo.RowPointer.Equals(this.currentRow))
                {
                    this.currentRow = dropInfo.RowPointer;
                    this.rowTimestamp = new DateTime?(time);
                }
                if ((this.rowTimestamp != null) && ((this.currentRow != null) && (Subtract(time, this.rowTimestamp.Value) >= this.view.AutoExpandDelayOnDrag)))
                {
                    this.Expand(dropInfo.RowPointer);
                    this.currentRow = null;
                    this.rowTimestamp = null;
                }
            }
        }

        public Func<DateTime> TimeProvider
        {
            get => 
                this.timeProvider;
            set => 
                this.timeProvider = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RowExpandService.<>c <>9 = new RowExpandService.<>c();
            public static Func<DateTime> <>9__7_0;

            internal DateTime <.ctor>b__7_0() => 
                DateTime.Now;
        }
    }
}

