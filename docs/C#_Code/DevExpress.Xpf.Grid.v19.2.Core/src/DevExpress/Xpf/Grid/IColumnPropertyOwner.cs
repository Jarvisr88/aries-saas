namespace DevExpress.Xpf.Grid
{
    using System;

    public interface IColumnPropertyOwner
    {
        FixedStyle GetActualFixedStyle();

        BaseColumn Column { get; }

        double ActualWidth { get; }
    }
}

