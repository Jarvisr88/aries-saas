namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public class ToolBarPageGroupLayoutBuilder<T> : CommandGroupLayoutBuilderBase<T, ToolBarPageGroupLayoutBuilder<T>>
    {
        private readonly ToolBarPageLayoutBuilder<T> parent;
        private readonly string pageName;
        private readonly string pageGroupName;

        internal ToolBarPageGroupLayoutBuilder(ClassMetadataBuilder<T> owner, ToolBarPageLayoutBuilder<T> parent, string pageName, string pageGroupName) : base(owner)
        {
            this.parent = parent;
            this.pageName = pageName;
            this.pageGroupName = pageGroupName;
        }

        protected override ToolBarPageGroupLayoutBuilder<T> ContainsCommandCore<TCommandBuilder>(CommandMetadataBuilderBase<T, TCommandBuilder> commandBuilder) where TCommandBuilder: CommandMetadataBuilderBase<T, TCommandBuilder>
        {
            commandBuilder.AddOrModifyAttribute<ToolBarItemAttribute>(delegate (ToolBarItemAttribute x) {
                x.Page = base.pageName;
                x.PageGroup = base.pageGroupName;
                int currentToolbarLayoutOrder = base.owner.CurrentToolbarLayoutOrder;
                base.owner.CurrentToolbarLayoutOrder = currentToolbarLayoutOrder + 1;
                x.Order = currentToolbarLayoutOrder;
            });
            return (ToolBarPageGroupLayoutBuilder<T>) this;
        }

        public ToolBarPageLayoutBuilder<T> EndPageGroup() => 
            this.parent;
    }
}

