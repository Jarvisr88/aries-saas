namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.ComponentModel;

    public enum ScheduledTaskMode
    {
        [Obsolete("The ScheduledTaskMode.Report member is now obsolete. Use ScheduledTaskMode.Document instead."), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        Report = 0,
        Document = 0,
        BillingStatement = 1
    }
}

