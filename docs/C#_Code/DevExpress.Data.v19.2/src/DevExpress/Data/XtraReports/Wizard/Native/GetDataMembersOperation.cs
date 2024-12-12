namespace DevExpress.Data.XtraReports.Wizard.Native
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.Data.XtraReports.DataProviders;
    using DevExpress.Data.XtraReports.ServiceModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This class is no longer used in the current Report Wizard implementation.")]
    public class GetDataMembersOperation
    {
        private readonly IReportWizardServiceClient client;
        private string dataSourceName;

        public event EventHandler<AsyncCompletedEventArgs> GetDataMembersCompleted;

        public GetDataMembersOperation(IReportWizardServiceClient client);
        private void client_GetDataMembersCompleted(object sender, ScalarOperationCompletedEventArgs<IEnumerable<TableInfo>> e);
        private bool ErrorOrCancelled(AsyncCompletedEventArgs e);
        public void GetDataMembersAsync(string dataSourceName, object asyncState);
        private void RaiseCompleted(AsyncCompletedEventArgs e);

        public IEnumerable<TableInfo> Tables { get; private set; }

        public IEnumerable<TableInfo> Views { get; private set; }

        public IEnumerable<TableInfo> StoredProcedures { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GetDataMembersOperation.<>c <>9;
            public static Func<TableInfo, bool> <>9__19_0;
            public static Func<TableInfo, bool> <>9__19_1;
            public static Func<TableInfo, bool> <>9__19_2;

            static <>c();
            internal bool <client_GetDataMembersCompleted>b__19_0(TableInfo x);
            internal bool <client_GetDataMembersCompleted>b__19_1(TableInfo x);
            internal bool <client_GetDataMembersCompleted>b__19_2(TableInfo x);
        }
    }
}

