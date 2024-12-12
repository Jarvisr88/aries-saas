namespace DevExpress.XtraPrinting.DataNodes
{
    using System;

    public interface IDataNode
    {
        bool CanGetChild(int index);
        IDataNode GetChild(int index);

        IDataNode Parent { get; }

        int Index { get; }

        bool IsDetailContainer { get; }

        bool PageBreakBefore { get; }

        bool PageBreakAfter { get; }
    }
}

