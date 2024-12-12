namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices;

    public class DataFormLayoutBuilder<T, TParentBuilder> : LayoutBuilderBase<T, DataFormLayoutBuilder<T, TParentBuilder>> where TParentBuilder: ClassMetadataBuilder<T>
    {
        private readonly DataFormLayoutBuilder<T, TParentBuilder> parent;

        internal DataFormLayoutBuilder(ClassMetadataBuilder<T> owner) : base(owner)
        {
        }

        internal DataFormLayoutBuilder(string groupName, ClassMetadataBuilder<T> owner, DataFormLayoutBuilder<T, TParentBuilder> parent, GroupView groupView, Orientation? orientation, string start) : base(groupName, owner, groupView, orientation, start)
        {
            this.parent = parent;
        }

        public DataFormLayoutBuilder<T, TParentBuilder> ContainsProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression) => 
            base.ContainsPropertyCore<TProperty>(propertyExpression);

        public DataFormLayoutBuilder<T, TParentBuilder> EndGroup() => 
            this.parent;

        public DataFormLayoutBuilder<T, TParentBuilder> Group(string groupName, Orientation? orientation = new Orientation?()) => 
            new DataFormLayoutBuilder<T, TParentBuilder>(groupName, base.owner, (DataFormLayoutBuilder<T, TParentBuilder>) this, GroupView.Group, orientation, base.GroupPathStart);

        public DataFormLayoutBuilder<T, TParentBuilder> GroupBox(string groupName, Orientation? orientation = new Orientation?()) => 
            new DataFormLayoutBuilder<T, TParentBuilder>(groupName, base.owner, (DataFormLayoutBuilder<T, TParentBuilder>) this, GroupView.GroupBox, orientation, base.GroupPathStart);

        public DataFormLayoutBuilder<T, TParentBuilder> TabbedGroup(string groupName) => 
            new DataFormLayoutBuilder<T, TParentBuilder>(groupName, base.owner, (DataFormLayoutBuilder<T, TParentBuilder>) this, GroupView.Tabs, null, base.GroupPathStart);

        internal override DevExpress.Mvvm.Native.LayoutType LayoutType =>
            DevExpress.Mvvm.Native.LayoutType.DataForm;
    }
}

