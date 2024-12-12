namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class SyncServerModeListSource : IListSource
    {
        private readonly SyncVisibleListWrapper syncVisibleListWrapper;

        public SyncServerModeListSource(SyncVisibleListWrapper syncVisibleListWrapper, IDataControllerOwner owner)
        {
            this.syncVisibleListWrapper = syncVisibleListWrapper;
            syncVisibleListWrapper.Initialize(new GridControlApplyServerWrapper(syncVisibleListWrapper, owner));
        }

        public IList GetList() => 
            new SyncServerModeListSourceWrapper(this.Wrapper);

        private SyncVisibleListWrapper Wrapper =>
            this.syncVisibleListWrapper;

        public bool ContainsListCollection =>
            false;
    }
}

