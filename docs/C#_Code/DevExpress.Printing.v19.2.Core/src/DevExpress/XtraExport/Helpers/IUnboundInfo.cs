namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Data;
    using System;

    public interface IUnboundInfo
    {
        string UnboundExpression { get; }

        UnboundColumnType UnboundType { get; }
    }
}

