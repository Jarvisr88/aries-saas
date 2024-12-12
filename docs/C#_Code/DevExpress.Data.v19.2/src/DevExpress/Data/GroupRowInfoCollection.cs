namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class GroupRowInfoCollection : Collection<GroupRowInfo>, IDisposable
    {
        private DataControllerBase controller;
        private bool autoExpandAllGroups;
        private int lastExpandableLevel;
        private DataColumnSortInfoCollection sortInfo;
        private VisibleListSourceRowCollection visibleListSourceRows;
        private int alwaysVisibleLevelIndex;
        private List<GroupRowInfo> delayedDeleteGroups;

        public GroupRowInfoCollection(DataControllerBase controller, DataColumnSortInfoCollection sortInfo, VisibleListSourceRowCollection visibleListSourceRows);
        public virtual GroupRowInfo Add(byte level, int ChildControllerRow, GroupRowInfo parentGroup);
        private void AddReverceGroupChildren(GroupRowInfo groupRow, List<GroupRowInfo> list);
        private void AddReverseGroup(GroupRowInfo groupRow, List<GroupRowInfo> list);
        public bool ChangeAllExpanded(bool expanded);
        public bool ChangeChildExpanded(GroupRowInfo groupRow, bool expanded);
        public bool ChangeExpanded(int groupRowHandle, bool expanded, bool recursive);
        public bool ChangeExpandedLevel(int groupLevel, bool expanded, bool recursive);
        protected bool ChangeGroupRowExpanded(GroupRowInfo groupRow, bool expanded);
        public bool ChangeLevelExpanded(int level, bool expanded);
        public void ClearSummary();
        protected virtual GroupRowInfo CreateGroupRowInfo(byte level, int childControllerRow, GroupRowInfo parentGroupRow);
        private GroupRowInfo CreateNewGroup(int controllerRow, GroupRowInfo parent, int groupLevel, int newGroupIndex, DataControllerChangedItemCollection changedItems);
        private GroupRowInfo CreateNewGroup(int controllerRow, GroupRowInfo prevGroup, GroupRowInfo nextGroup, int groupLevelNext, int groupLevelPrev, DataControllerChangedItemCollection changedItems);
        public virtual void Dispose();
        public GroupRowInfo DoRowAdded(int controllerRow, DataControllerChangedItemCollection changedItems);
        private GroupRowInfo DoRowAddedCore(int controllerRow, DataControllerChangedItemCollection changedItems);
        public void DoRowChanged(VisibleIndexCollection visibleIndexes, int oldControllerRow, int newControllerRow, DataControllerChangedItemCollection changedItems);
        public void DoRowDeleted(int controllerRow, DataControllerChangedItemCollection changedItems);
        protected void DoRowDeleted(int controllerRow, GroupRowInfo groupRow, DataControllerChangedItemCollection changedItems, out GroupRowInfo summaryUpdateRequired);
        public int GetChildCount(GroupRowInfo groupRow);
        public int GetChildCount(int groupRowHandle);
        public int GetChildrenGroupCount(GroupRowInfo groupRow);
        public void GetChildrenGroups(GroupRowInfo groupRow, IList<GroupRowInfo> list);
        public void GetChildrenGroups(GroupRowInfo groupRow, IList<GroupRowInfo> list, int level);
        public int GetChildRow(GroupRowInfo groupRow, int childIndex);
        public int GetChildRow(int groupRowHandle, int childIndex);
        public GroupRowInfo GetGroupRowInfoByControllerRowHandle(int controllerRowHandle);
        public GroupRowInfo GetGroupRowInfoByControllerRowHandleBinary(int controllerRowHandle);
        public GroupRowInfo GetGroupRowInfoByHandle(int groupRowHandle);
        private int GetNewGroupIndex(GroupRowInfo parent, int controllerRow);
        public int GetParentRow(GroupRowInfo groupRow);
        public int GetParentRow(int groupRowHandle);
        public GroupRowInfo GetRootGroup(int index);
        public int GetTotalChildrenGroupCount(GroupRowInfo groupRow);
        public int GetTotalGroupsCountByLevel(int level);
        public int GetVisibleRowsCount(GroupRowInfo groupRow);
        private void IncrementChildControllerRowCount(GroupRowInfo groupRow, DataControllerChangedItemCollection changedItems);
        private void IncrementChildControllerRowCount(GroupRowInfo groupRow, DataControllerChangedItemCollection changedItems, bool addItemAtCurrentGroup);
        private void IncrementChildControllerRowCount(int controllerRow, GroupRowInfo groupRow, DataControllerChangedItemCollection changedItems, bool decrementChildVisibleRow, bool addItemAtCurrentGroup);
        public bool IsLastLevel(GroupRowInfo groupRow);
        private bool IsSameGroup(GroupRowInfo oldGroup, GroupRowInfo newGroup);
        public bool MakeVisible(GroupRowInfo groupRow, bool showChildren);
        public void MoveFromEndToMiddle(int startIndex, int count, int moveTo);
        protected override void RemoveItem(int index);
        private void RenumIndexes(int controllerRow, bool increment);
        private void ReverseGroups(int level);
        public virtual void ReverseLevel(int level);
        protected bool ReverseLevelCore(int level);
        private int ReverseParentGroup(GroupRowInfo parentGroupRow, List<GroupRowInfo> list);
        private void UpdateChildControllerRows();
        public void UpdateIndexes();
        public void UpdateIndexes(int startFrom);
        private void UpdateVisibleListSourceRowCollection();

        protected List<GroupRowInfo> ListCore { get; }

        protected DataControllerBase Controller { get; }

        protected virtual DataColumnSortInfoCollection SortInfo { get; }

        public virtual VisibleListSourceRowCollection VisibleListSourceRows { get; }

        public bool AutoExpandAllGroups { get; set; }

        public bool AllowPartialGrouping { get; set; }

        public int LastExpandableLevel { get; set; }

        public bool IsGrouped { get; }

        public int LevelCount { get; }

        public int AlwaysVisibleLevelIndex { get; set; }

        public virtual int RootGroupCount { get; }
    }
}

