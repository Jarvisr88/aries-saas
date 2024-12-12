namespace DevExpress.Xpf.Grid.EditForm
{
    using System;
    using System.Runtime.CompilerServices;

    internal class CaptionedLayoutItem : IEditFormLayoutItem
    {
        private readonly IEditFormLayoutItem captionCore;
        private readonly IEditFormLayoutItem editorCore;
        private int columnCore;
        private int rowCore;
        private bool startNewRowCore;

        public CaptionedLayoutItem(IEditFormLayoutItem caption, IEditFormLayoutItem editor)
        {
            this.captionCore = caption;
            this.editorCore = editor;
            this.ColumnSpan = this.editorCore.ColumnSpan;
            this.RowSpan = this.editorCore.RowSpan;
            this.startNewRowCore = this.editorCore.StartNewRow;
            this.Caption.ColumnSpan = 1;
            this.Caption.RowSpan = 1;
            this.Editor.ColumnSpan = (this.Editor.ColumnSpan * 2) - 1;
            this.SyncColumn();
            this.SyncRow();
        }

        private void SyncColumn()
        {
            this.Caption.Column = this.Column * 2;
            this.Editor.Column = this.Caption.Column + 1;
        }

        private void SyncRow()
        {
            this.Caption.Row = this.Row;
            this.Editor.Row = this.Row;
        }

        private IEditFormLayoutItem Caption =>
            this.captionCore;

        private IEditFormLayoutItem Editor =>
            this.editorCore;

        public int Column
        {
            get => 
                this.columnCore;
            set
            {
                if (this.columnCore != value)
                {
                    this.columnCore = value;
                    this.SyncColumn();
                }
            }
        }

        public int Row
        {
            get => 
                this.rowCore;
            set
            {
                if (this.rowCore != value)
                {
                    this.rowCore = value;
                    this.SyncRow();
                }
            }
        }

        public int ColumnSpan { get; set; }

        public int RowSpan { get; set; }

        public bool StartNewRow =>
            this.startNewRowCore;

        public EditFormLayoutItemType ItemType =>
            EditFormLayoutItemType.None;
    }
}

