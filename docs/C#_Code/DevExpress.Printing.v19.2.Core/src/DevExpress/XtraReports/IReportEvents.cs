namespace DevExpress.XtraReports
{
    using System;

    internal interface IReportEvents
    {
        void PostponeAfterPrint();
        void RaisePostponedAfterPrint();
    }
}

