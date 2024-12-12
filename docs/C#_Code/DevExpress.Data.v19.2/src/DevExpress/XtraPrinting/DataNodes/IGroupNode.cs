namespace DevExpress.XtraPrinting.DataNodes
{
    using System;

    public interface IGroupNode : IDataNode
    {
        GroupUnion Union { get; }

        bool RepeatHeaderEveryPage { get; }
    }
}

