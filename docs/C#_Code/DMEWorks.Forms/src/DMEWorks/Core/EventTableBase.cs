namespace DMEWorks.Core
{
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class EventTableBase : TableBase
    {
        private int m_updateCount;

        public event EventHandler Changed;

        public event EventHandler Changing;

        public EventTableBase() : this("")
        {
        }

        public EventTableBase(string tableName) : base(tableName)
        {
            base.TableNewRow += new DataTableNewRowEventHandler(EventTableBase.HandleTableNewRow);
        }

        public virtual void BeginUpdate()
        {
            this.OnChanging(EventArgs.Empty);
            this.m_updateCount++;
        }

        public virtual void EndUpdate()
        {
            this.m_updateCount--;
            this.OnChanged(EventArgs.Empty);
        }

        private static void HandleTableNewRow(object sender, DataTableNewRowEventArgs e)
        {
        }

        protected virtual void OnChanged(EventArgs e)
        {
            if (this.m_updateCount == 0)
            {
                EventHandler changed = this.Changed;
                if (changed != null)
                {
                    changed(this, e);
                }
            }
        }

        protected virtual void OnChanging(EventArgs e)
        {
            if (this.m_updateCount == 0)
            {
                EventHandler changing = this.Changing;
                if (changing != null)
                {
                    changing(this, e);
                }
            }
        }

        protected override void OnColumnChanged(DataColumnChangeEventArgs e)
        {
            try
            {
                this.OnChanged(e);
            }
            finally
            {
                base.OnColumnChanged(e);
            }
        }

        protected override void OnColumnChanging(DataColumnChangeEventArgs e)
        {
            base.OnColumnChanging(e);
            this.OnChanging(e);
        }

        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            try
            {
                this.OnChanged(e);
            }
            finally
            {
                base.OnRowChanged(e);
            }
        }

        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            this.OnChanging(e);
        }

        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            try
            {
                this.OnChanged(e);
            }
            finally
            {
                base.OnRowDeleted(e);
            }
        }

        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            this.OnChanging(e);
        }

        protected override void OnTableCleared(DataTableClearEventArgs e)
        {
            try
            {
                this.OnChanged(e);
            }
            finally
            {
                base.OnTableCleared(e);
            }
        }

        protected override void OnTableClearing(DataTableClearEventArgs e)
        {
            base.OnTableClearing(e);
            this.OnChanging(e);
        }

        protected override void OnTableNewRow(DataTableNewRowEventArgs e)
        {
            try
            {
                this.OnChanged(e);
            }
            finally
            {
                base.OnTableNewRow(e);
            }
        }
    }
}

