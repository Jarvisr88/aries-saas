namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq.Expressions;

    public class TableGroupLayoutBuilder<T> : LayoutBuilderBase<T, TableGroupLayoutBuilder<T>>
    {
        private readonly TableGroupContainerLayoutBuilder<T> parent;

        internal TableGroupLayoutBuilder(string groupName, ClassMetadataBuilder<T> owner, TableGroupContainerLayoutBuilder<T> parent, string start) : base(groupName, owner, GroupView.Group, nullable, start)
        {
            this.parent = parent;
        }

        public TableGroupLayoutBuilder<T> ContainsProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression) => 
            base.ContainsPropertyCore<TProperty>(propertyExpression);

        public TableGroupContainerLayoutBuilder<T> EndGroup() => 
            this.parent;

        internal override DevExpress.Mvvm.Native.LayoutType LayoutType =>
            DevExpress.Mvvm.Native.LayoutType.Table;
    }
}

