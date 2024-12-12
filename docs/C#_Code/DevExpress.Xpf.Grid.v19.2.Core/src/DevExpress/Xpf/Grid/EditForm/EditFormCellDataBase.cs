namespace DevExpress.Xpf.Grid.EditForm
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class EditFormCellDataBase : IEditFormLayoutItem
    {
        private int columnSpanCore = 1;
        private int rowSpanCore = 1;

        protected EditFormCellDataBase()
        {
        }

        protected internal virtual void Assign(EditFormColumnSource source)
        {
            if (source != null)
            {
                if (source.ColumnSpan != null)
                {
                    this.ColumnSpan = source.ColumnSpan.Value;
                }
                if (source.RowSpan != null)
                {
                    this.RowSpan = source.RowSpan.Value;
                }
                this.StartNewRow = source.StartNewRow;
            }
        }

        private static int CoerceSpanValue(int spanValue) => 
            (spanValue > 0) ? spanValue : 1;

        public int Column { get; set; }

        public int Row { get; set; }

        public bool StartNewRow { get; internal set; }

        public EditFormLayoutItemType ItemType =>
            this.ItemTypeCore;

        protected abstract EditFormLayoutItemType ItemTypeCore { get; }

        public int ColumnSpan
        {
            get => 
                this.columnSpanCore;
            set
            {
                if (this.columnSpanCore != value)
                {
                    this.columnSpanCore = CoerceSpanValue(value);
                }
            }
        }

        public int RowSpan
        {
            get => 
                this.rowSpanCore;
            set
            {
                if (this.rowSpanCore != value)
                {
                    this.rowSpanCore = CoerceSpanValue(value);
                }
            }
        }
    }
}

