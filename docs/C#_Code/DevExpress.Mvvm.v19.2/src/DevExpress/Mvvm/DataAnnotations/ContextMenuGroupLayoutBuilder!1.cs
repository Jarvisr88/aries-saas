namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public class ContextMenuGroupLayoutBuilder<T> : CommandGroupLayoutBuilderBase<T, ContextMenuGroupLayoutBuilder<T>>
    {
        private readonly ContextMenuLayoutBuilder<T> parent;
        private readonly string pageGroupName;

        internal ContextMenuGroupLayoutBuilder(ClassMetadataBuilder<T> owner, ContextMenuLayoutBuilder<T> parent, string pageGroupName) : base(owner)
        {
            this.parent = parent;
            this.pageGroupName = pageGroupName;
        }

        protected override ContextMenuGroupLayoutBuilder<T> ContainsCommandCore<TCommandBuilder>(CommandMetadataBuilderBase<T, TCommandBuilder> commandBuilder) where TCommandBuilder: CommandMetadataBuilderBase<T, TCommandBuilder>
        {
            commandBuilder.AddOrModifyAttribute<ContextMenuItemAttribute>(delegate (ContextMenuItemAttribute x) {
                x.Group = base.pageGroupName;
                int currentContextMenuLayoutOrder = base.owner.CurrentContextMenuLayoutOrder;
                base.owner.CurrentContextMenuLayoutOrder = currentContextMenuLayoutOrder + 1;
                x.Order = currentContextMenuLayoutOrder;
            });
            return (ContextMenuGroupLayoutBuilder<T>) this;
        }

        public ContextMenuLayoutBuilder<T> EndGroup() => 
            this.parent;
    }
}

