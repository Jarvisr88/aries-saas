namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.Browsing.Design;
    using DevExpress.Data.WizardFramework;
    using DevExpress.Data.XtraReports.DataProviders;
    using DevExpress.Data.XtraReports.Wizard;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Presenters.AddGroupingLevelPage<TModel> class from the DevExpress.XtraReports assembly instead.")]
    public class AddGroupingLevelPage<TModel> : WizardPageBase<IAddGroupingLevelPageView, TModel> where TModel: ReportModel
    {
        private readonly IColumnInfoCache columnInfoCache;
        private readonly List<ColumnInfo> availableColumns;
        private readonly List<GroupingLevelInfo> groupingLevels;
        private string[] summaryColumnNames;

        public AddGroupingLevelPage(IAddGroupingLevelPageView view, IColumnInfoCache columnInfoCache);
        public override void Begin();
        private bool CanCreateSummaryForType(TypeSpecifics typeSpecifics);
        public override void Commit();
        private void FillViewControls();
        public override Type GetNextPageType();
        private void MoveActiveGroupingLevel(bool moveDown);
        private void RefreshButtons();
        private void RefreshView();
        private void UpdateSummaryColumnNames();
        private void view_ActiveAvailableColumnsChanged(object sender, EventArgs e);
        private void view_ActiveGroupingLevelChanged(object sender, EventArgs e);
        private void view_AddGroupingLevelClicked(object sender, EventArgs e);
        private void view_CombineGroupingLevelClicked(object sender, EventArgs e);
        private void view_GroupingLevelDownClicked(object sender, EventArgs e);
        private void view_GroupingLevelUpClicked(object sender, EventArgs e);
        private void view_RemoveGroupingLevelClicked(object sender, EventArgs e);

        public override bool FinishEnabled { get; }

        public override bool MoveNextEnabled { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AddGroupingLevelPage<TModel>.<>c <>9;
            public static Func<ColumnNameSummaryOptions, string> <>9__20_1;
            public static Func<ColumnInfo, string> <>9__21_0;
            public static Func<string, ColumnNameSummaryOptions> <>9__21_1;

            static <>c();
            internal string <Begin>b__20_1(ColumnNameSummaryOptions x);
            internal string <Commit>b__21_0(ColumnInfo c);
            internal ColumnNameSummaryOptions <Commit>b__21_1(string c);
        }
    }
}

