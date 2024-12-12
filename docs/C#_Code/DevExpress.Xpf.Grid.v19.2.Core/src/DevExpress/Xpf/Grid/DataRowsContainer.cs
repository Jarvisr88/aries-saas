namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class DataRowsContainer : RowsContainer
    {
        internal const string IsGroupRowsContainerPropertyName = "IsGroupRowsContainer";
        protected static readonly DependencyPropertyKey IsGroupRowsContainerPropertyKey;
        public static readonly DependencyProperty IsGroupRowsContainerProperty;
        public static readonly DependencyProperty GroupLevelProperty;
        private static readonly DependencyPropertyKey GroupLevelPropertyKey;
        protected readonly DataTreeBuilder treeBuilder;

        static DataRowsContainer()
        {
            Type ownerType = typeof(DataRowsContainer);
            IsGroupRowsContainerPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsGroupRowsContainer", typeof(bool), ownerType, new PropertyMetadata(false));
            IsGroupRowsContainerProperty = IsGroupRowsContainerPropertyKey.DependencyProperty;
            GroupLevelPropertyKey = DependencyPropertyManager.RegisterReadOnly("GroupLevel", typeof(int), ownerType, new PropertyMetadata(0));
            GroupLevelProperty = GroupLevelPropertyKey.DependencyProperty;
        }

        public DataRowsContainer(DataTreeBuilder treeBuilder, int level)
        {
            this.treeBuilder = treeBuilder;
            this.GroupLevel = level;
            base.InitItemsCollection();
        }

        internal override bool BaseSyncronize(NodeContainer nodeContainer)
        {
            if (nodeContainer == null)
            {
                return false;
            }
            DataNodeContainer container = (DataNodeContainer) nodeContainer;
            this.GroupLevel = container.GroupLevel;
            bool isGroupRowsContainer = this.IsGroupRowsContainer;
            this.IsGroupRowsContainer = container.IsGroupRowsContainer;
            return ((this.View.CacheVersion != ((ISupportCacheVersion) base.Items).CacheVersion) || (isGroupRowsContainer != this.IsGroupRowsContainer));
        }

        internal override Guid GetCacheVersion() => 
            this.View.CacheVersion;

        public bool IsGroupRowsContainer
        {
            get => 
                (bool) base.GetValue(IsGroupRowsContainerProperty);
            private set => 
                base.SetValue(IsGroupRowsContainerPropertyKey, value);
        }

        public int GroupLevel
        {
            get => 
                (int) base.GetValue(GroupLevelProperty);
            private set => 
                base.SetValue(GroupLevelPropertyKey, value);
        }

        protected internal DataViewBase View =>
            this.TreeBuilder.View;

        internal DataTreeBuilder TreeBuilder =>
            this.treeBuilder;

        internal override MasterRowsContainer MasterRootRowsContainer =>
            this.TreeBuilder.MasterRootRowsContainer;

        internal override DevExpress.Xpf.Grid.Native.SynchronizationQueues SynchronizationQueues =>
            ((VisualDataTreeBuilder) this.TreeBuilder).SynchronizationQueues;
    }
}

