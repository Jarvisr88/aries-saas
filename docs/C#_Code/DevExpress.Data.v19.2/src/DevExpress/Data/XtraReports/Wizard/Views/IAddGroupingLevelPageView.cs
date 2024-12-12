namespace DevExpress.Data.XtraReports.Wizard.Views
{
    using DevExpress.Data.XtraReports.DataProviders;
    using DevExpress.Data.XtraReports.Wizard;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.Views.IAddGroupingLevelPageView class from the DevExpress.XtraReports assembly instead.")]
    public interface IAddGroupingLevelPageView
    {
        event EventHandler ActiveAvailableColumnsChanged;

        event EventHandler ActiveGroupingLevelChanged;

        event EventHandler AddGroupingLevelClicked;

        event EventHandler CombineGroupingLevelClicked;

        event EventHandler GroupingLevelDownClicked;

        event EventHandler GroupingLevelUpClicked;

        event EventHandler RemoveGroupingLevelClicked;

        void EnableAddGroupingLevelButton(bool enable);
        void EnableCombineGroupingLevelButton(bool enable);
        void EnableGroupingLevelDown(bool enable);
        void EnableGroupingLevelUp(bool enable);
        void EnableRemoveGroupingLevelButton(bool enable);
        void FillAvailableColumns(ColumnInfo[] columns);
        void FillGroupingLevels(GroupingLevelInfo[] groupingLevels);
        ColumnInfo[] GetActiveAvailableColumns();
        GroupingLevelInfo GetActiveGroupingLevel();
        void SetActiveGroupingLevel(GroupingLevelInfo groupingLevel);
        void ShowWaitIndicator(bool show);
    }
}

