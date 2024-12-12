namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.IO;

    public class NewListSourceRowKeeper : IClassicRowKeeper, IDisposable
    {
        private bool hasData;
        private SelectionKeeper selection;
        private ExpandedGroupKeeper expandedGroups;
        private DataController controller;

        public NewListSourceRowKeeper(DataController controller, ExpandedGroupKeeper groupsKeeper, SelectionKeeper selectionKeeper);
        protected virtual void ClearCore();
        protected virtual void ClearSelection();
        protected virtual ExpandedGroupKeeper CreateExpandedGroupsKeeper();
        protected virtual SelectionKeeper CreateSelectionKeeper();
        void IClassicRowKeeper.Clear();
        void IClassicRowKeeper.ClearSelection();
        bool IClassicRowKeeper.GroupsRestoreFromStream(Stream stream);
        void IClassicRowKeeper.GroupsSaveToStream(Stream stream);
        bool IClassicRowKeeper.Restore();
        bool IClassicRowKeeper.RestoreIncremental();
        bool IClassicRowKeeper.RestoreStream();
        void IClassicRowKeeper.Save();
        void IClassicRowKeeper.SaveOnFilter();
        void IClassicRowKeeper.SaveOnRefresh(bool isEndUpdate);
        protected void ResetHasData();
        protected virtual void RestoreClear();
        protected virtual bool RestoreCore();
        protected virtual bool RestoreIncrementalCore();
        protected virtual void RestoreSelection();
        protected virtual void SaveCore();
        protected virtual void SaveOnFilterCore();
        protected virtual void SaveSelection();
        void IDisposable.Dispose();

        protected ExpandedGroupKeeper ExpandedGroups { get; }

        protected SelectionKeeper Selection { get; }

        protected bool HasData { get; }

        protected DataController Controller { get; }

        protected virtual bool HasSelection { get; }
    }
}

