namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.WizardFramework;
    using DevExpress.Data.XtraReports.Wizard;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Presenters.ChooseSummaryOptionsPage<TModel> class from the DevExpress.XtraReports assembly instead.")]
    public class ChooseSummaryOptionsPage<TModel> : WizardPageBase<IChooseSummaryOptionsPageView, TModel> where TModel: ReportModel
    {
        private readonly List<ColumnInfoSummaryOptions> summaryOptions;
        private readonly IColumnInfoCache columnInfoCache;
        private bool ignoreNullValues;

        public ChooseSummaryOptionsPage(IChooseSummaryOptionsPageView view, IColumnInfoCache columnInfoCache);
        public override void Begin();
        public override void Commit();
        public override Type GetNextPageType();
        private void RefreshView();

        public override bool MoveNextEnabled { get; }

        public override bool FinishEnabled { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ChooseSummaryOptionsPage<TModel>.<>c <>9;
            public static Func<ColumnInfoSummaryOptions, ColumnNameSummaryOptions> <>9__11_0;

            static <>c();
            internal ColumnNameSummaryOptions <Commit>b__11_0(ColumnInfoSummaryOptions x);
        }
    }
}

