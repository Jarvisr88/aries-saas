namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class AsyncServerModeListSource : IListSource
    {
        private readonly AsyncVisibleListWrapper asyncVisibleListWrapper;

        public AsyncServerModeListSource(AsyncVisibleListWrapper asyncVisibleListWrapper, IDataControllerOwner owner)
        {
            this.asyncVisibleListWrapper = asyncVisibleListWrapper;
            asyncVisibleListWrapper.Initialize(new AsyncGridControlApplyServerWrapper(asyncVisibleListWrapper, owner));
        }

        public IList GetList() => 
            new AsyncServerModeListSourceWrapper(this.Wrapper);

        private AsyncVisibleListWrapper Wrapper =>
            this.asyncVisibleListWrapper;

        public bool ContainsListCollection =>
            false;
    }
}

