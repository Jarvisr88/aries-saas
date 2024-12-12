namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.Browsing.Design;
    using DevExpress.Data.WizardFramework;
    using DevExpress.Data.XtraReports.DataProviders;
    using DevExpress.Data.XtraReports.Native;
    using DevExpress.Data.XtraReports.Wizard;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This class is no longer used in the current Report Wizard implementation.")]
    public class SelectHierarchicalDataSourceColumnsPage<TModel> : WizardPageBase<ISelectHierarchicalDataSourceColumnsPageView, TModel> where TModel: ReportModel
    {
        private readonly PickManagerBase pickManager;
        private readonly List<ColumnInfo> selectedColumns;
        private readonly IColumnInfoCache columnInfoCache;

        public SelectHierarchicalDataSourceColumnsPage(ISelectHierarchicalDataSourceColumnsPageView view, PickManagerBase pickManager, IColumnInfoCache columnInfoCache);
        public override void Begin();
        private bool CanAddDataMemberNode(IPickManagerDataMemberNode node);
        public override void Commit();
        public override Type GetNextPageType();
        private void pickManager_FillContentCompleted();
        private void RefreshButtons();
        private void RefreshView();
        private void view_ActiveAvailableColumnChanged(object sender, EventArgs e);
        private void view_ActiveSelectedColumnsChanged(object sender, EventArgs e);
        private void view_AddColumnClicked(object sender, EventArgs e);
        private void view_RemoveAllColumnsClicked(object sender, EventArgs e);
        private void view_RemoveColumnsClicked(object sender, EventArgs e);

        public override bool MoveNextEnabled { get; }

        public override bool FinishEnabled { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectHierarchicalDataSourceColumnsPage<TModel>.<>c <>9;
            public static Func<ColumnInfo, string> <>9__16_0;

            static <>c();
            internal string <Commit>b__16_0(ColumnInfo c);
        }
    }
}

