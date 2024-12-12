namespace DevExpress.Xpf.Grid.EditForm
{
    using System;

    public interface IEditFormLayoutItem
    {
        int Column { get; set; }

        int Row { get; set; }

        int ColumnSpan { get; set; }

        int RowSpan { get; set; }

        bool StartNewRow { get; }

        EditFormLayoutItemType ItemType { get; }
    }
}

