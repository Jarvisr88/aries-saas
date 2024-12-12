namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomPrepareTopGroupInfoEventArgs : EventArgs
    {
        private CriteriaOperator where;
        private ServerModeSummaryDescriptor[] summaries;

        public CustomPrepareTopGroupInfoEventArgs(CriteriaOperator where, ServerModeSummaryDescriptor[] summaries);

        public bool Handled { get; set; }

        public ServerModeGroupInfoData Result { get; set; }

        public CriteriaOperator Where { get; }

        public ServerModeSummaryDescriptor[] Summaries { get; }
    }
}

