namespace DevExpress.Office
{
    using System;

    public abstract class OfficeDataControllerAdapterBase : IDisposable
    {
        private EventHandler onCurrentRowChaned;
        private EventHandler onDataSourceChanged;

        public event EventHandler CurrentRowChanged
        {
            add
            {
                this.onCurrentRowChaned += value;
            }
            remove
            {
                this.onCurrentRowChaned -= value;
            }
        }

        public event EventHandler DataSourceChanged
        {
            add
            {
                this.onDataSourceChanged += value;
            }
            remove
            {
                this.onDataSourceChanged -= value;
            }
        }

        protected OfficeDataControllerAdapterBase()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public abstract int GetColumnIndex(string name);
        public abstract object GetCurrentRow();
        public abstract object GetCurrentRowValue(int columnIndex);
        protected virtual void RaiseCurrentRowChangedEvent()
        {
            if (this.onCurrentRowChaned != null)
            {
                this.onCurrentRowChaned(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseDataSourceChanged()
        {
            if (this.onDataSourceChanged != null)
            {
                this.onDataSourceChanged(this, EventArgs.Empty);
            }
        }

        public abstract bool IsReady { get; }

        public abstract int ListSourceRowCount { get; }

        public abstract int CurrentControllerRow { get; set; }

        public abstract object DataSource { get; set; }

        public abstract string DataMember { get; set; }
    }
}

