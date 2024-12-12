namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;

    public class TableGroupContainerLayoutBuilder<T> : LayoutBuilderBase<T, TableGroupContainerLayoutBuilder<T>>
    {
        private readonly TableGroupContainerLayoutBuilder<T> parent;

        internal TableGroupContainerLayoutBuilder(ClassMetadataBuilder<T> owner) : base(owner)
        {
        }

        internal TableGroupContainerLayoutBuilder(string groupName, ClassMetadataBuilder<T> owner, TableGroupContainerLayoutBuilder<T> parent, string start) : base(groupName, owner, GroupView.Group, nullable, start)
        {
            this.parent = parent;
        }

        public TableGroupContainerLayoutBuilder<T> EndGroupContainer() => 
            this.parent;

        public TableGroupLayoutBuilder<T> Group(string groupName) => 
            new TableGroupLayoutBuilder<T>(groupName, base.owner, (TableGroupContainerLayoutBuilder<T>) this, base.GroupPathStart);

        public TableGroupContainerLayoutBuilder<T> GroupContainer(string groupName) => 
            new TableGroupContainerLayoutBuilder<T>(groupName, base.owner, (TableGroupContainerLayoutBuilder<T>) this, base.GroupPathStart);

        internal override DevExpress.Mvvm.Native.LayoutType LayoutType =>
            DevExpress.Mvvm.Native.LayoutType.Table;
    }
}

