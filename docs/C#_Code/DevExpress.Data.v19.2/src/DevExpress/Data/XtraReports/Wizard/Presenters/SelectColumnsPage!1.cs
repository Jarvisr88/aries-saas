namespace DevExpress.Data.XtraReports.Wizard.Presenters
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.Data.XtraReports.DataProviders;
    using DevExpress.Data.XtraReports.ServiceModel;
    using DevExpress.Data.XtraReports.Wizard;
    using DevExpress.Data.XtraReports.Wizard.Views;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Presenters.SelectColumnsPage<TModel> class from the DevExpress.XtraReports assembly instead.")]
    public class SelectColumnsPage<TModel> : ReportWizardServiceClientPage<ISelectColumnsPageView, TModel> where TModel: ReportModel
    {
        private readonly SelectColumnsPage<TModel>.SortedColumnList availableColumns;
        private readonly List<ColumnInfo> selectedColumns;
        private readonly IColumnInfoCache columnInfoCache;

        public SelectColumnsPage(ISelectColumnsPageView view, IReportWizardServiceClient client, IColumnInfoCache columnInfoCache);
        public override void Begin();
        private void Client_GetColumnsCompleted(object sender, ScalarOperationCompletedEventArgs<IEnumerable<ColumnInfo>> e);
        public override void Commit();
        private void FillViewColumns();
        public override Type GetNextPageType();
        private static void MoveColumns(IEnumerable<ColumnInfo> activeColumns, IList<ColumnInfo> from, IList<ColumnInfo> to);
        private void OnAddRemoveButtonClicked(IEnumerable<ColumnInfo> activeColumns, IList<ColumnInfo> from, IList<ColumnInfo> to);
        private void RefreshButtons();
        protected void RefreshView();
        private void view_ActiveColumnsChanged(object sender, EventArgs e);
        private void view_AddAllColumnsClicked(object sender, EventArgs e);
        private void view_AddColumnsClicked(object sender, EventArgs e);
        private void view_RemoveAllColumnsClicked(object sender, EventArgs e);
        private void view_RemoveColumnsClicked(object sender, EventArgs e);

        protected SelectColumnsPage<TModel>.SortedColumnList AvailableColumns { get; }

        protected List<ColumnInfo> SelectedColumns { get; }

        public override bool MoveNextEnabled { get; }

        public override bool FinishEnabled { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectColumnsPage<TModel>.<>c <>9;
            public static Comparison<ColumnInfo> <>9__12_0;
            public static Func<ColumnInfo, string> <>9__22_0;

            static <>c();
            internal int <.ctor>b__12_0(ColumnInfo x, ColumnInfo y);
            internal string <Commit>b__22_0(ColumnInfo c);
        }

        protected class SortedColumnList : IList<ColumnInfo>, ICollection<ColumnInfo>, IEnumerable<ColumnInfo>, IEnumerable
        {
            private readonly List<ColumnInfo> innerList;
            private readonly Comparison<ColumnInfo> comparison;

            public SortedColumnList(Comparison<ColumnInfo> comparison);
            public void Add(ColumnInfo item);
            public void AddRange(IEnumerable<ColumnInfo> collection);
            public void Clear();
            public bool Contains(ColumnInfo item);
            public void CopyTo(ColumnInfo[] array, int arrayIndex);
            public ColumnInfo Find(Predicate<ColumnInfo> predicate);
            public IEnumerator<ColumnInfo> GetEnumerator();
            public int IndexOf(ColumnInfo item);
            public void Insert(int index, ColumnInfo item);
            public bool Remove(ColumnInfo item);
            public void RemoveAt(int index);
            private void SortInnerList();
            IEnumerator IEnumerable.GetEnumerator();

            public ColumnInfo this[int index] { get; set; }

            public int Count { get; }

            public bool IsReadOnly { get; }
        }
    }
}

