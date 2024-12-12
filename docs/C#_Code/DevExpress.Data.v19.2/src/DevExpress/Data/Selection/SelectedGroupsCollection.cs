namespace DevExpress.Data.Selection
{
    using DevExpress.Data;
    using System;

    public class SelectedGroupsCollection : BaseSelectionCollection<GroupRowInfo>
    {
        public SelectedGroupsCollection(SelectionController selectionController);
        protected internal override int CalcCRC();
        public int[] CopyToArray();
        public void CopyToArray(int[] array, int startIndex);
        protected internal override GroupRowInfo GetRowObjectByControllerRow(int controllerRow);
        internal void OnGroupDeleted(GroupRowInfo groupInfo);
        internal void OnReplaceGroupSelection(GroupRowInfo oldGroupInfo, GroupRowInfo newGroupInfo);
    }
}

