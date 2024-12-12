namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.InteropServices;

    public interface IColumnPanelLayoutCalculator
    {
        void CalcLayout(Size<int>[] childSizes, int columnCount, int[] columnOffsets, int[] rowOffsets, out Size<int> panelSize, out Point<int>[] childPositions);
    }
}

