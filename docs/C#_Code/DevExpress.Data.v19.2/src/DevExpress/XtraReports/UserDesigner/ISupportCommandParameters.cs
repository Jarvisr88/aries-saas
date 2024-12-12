namespace DevExpress.XtraReports.UserDesigner
{
    using System;

    public interface ISupportCommandParameters : ISupportCommand
    {
        object[] Parameters { get; set; }
    }
}

