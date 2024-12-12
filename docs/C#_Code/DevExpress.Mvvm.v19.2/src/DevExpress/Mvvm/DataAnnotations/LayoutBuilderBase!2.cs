namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq.Expressions;
    using System.Windows.Controls;

    public abstract class LayoutBuilderBase<T, TBuilder> where TBuilder: LayoutBuilderBase<T, TBuilder>
    {
        protected readonly ClassMetadataBuilder<T> owner;
        private readonly bool isRoot;
        private readonly string groupName;
        private readonly GroupView groupView;
        private readonly string start;
        private readonly Orientation? orientation;

        protected LayoutBuilderBase(ClassMetadataBuilder<T> owner)
        {
            this.owner = owner;
            this.isRoot = true;
        }

        protected LayoutBuilderBase(string groupName, ClassMetadataBuilder<T> owner, GroupView groupView, Orientation? orientation, string start)
        {
            this.owner = owner;
            this.groupName = groupName;
            this.groupView = groupView;
            this.start = start;
            this.orientation = orientation;
        }

        protected TBuilder ContainsPropertyCore<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            this.owner.PropertyCore<TProperty>(propertyExpression).GroupName(this.GroupPathStart, this.LayoutType);
            return (TBuilder) this;
        }

        private char? GetOrientation()
        {
            Orientation? nullable = this.orientation;
            if (nullable != null)
            {
                Orientation valueOrDefault = nullable.GetValueOrDefault();
                if (valueOrDefault == Orientation.Horizontal)
                {
                    return new char?(LayoutGroupInfoConstants.HorizontalGroupMark);
                }
                if (valueOrDefault == Orientation.Vertical)
                {
                    return new char?(LayoutGroupInfoConstants.VerticalGroupMark);
                }
            }
            return null;
        }

        private char GetPrefix()
        {
            switch (this.groupView)
            {
                case GroupView.Group:
                    return LayoutGroupInfoConstants.BorderlessGroupMarkStart;

                case GroupView.GroupBox:
                    return LayoutGroupInfoConstants.GroupBoxMarkStart;

                case GroupView.Tabs:
                    return LayoutGroupInfoConstants.TabbedGroupMarkStart;
            }
            throw new NotSupportedException();
        }

        private char GetSuffix()
        {
            switch (this.groupView)
            {
                case GroupView.Group:
                    return LayoutGroupInfoConstants.BorderlessGroupMarkEnd;

                case GroupView.GroupBox:
                    return LayoutGroupInfoConstants.GroupBoxMarkEnd;

                case GroupView.Tabs:
                    return LayoutGroupInfoConstants.TabbedGroupMarkEnd;
            }
            throw new NotSupportedException();
        }

        protected string GroupPathStart =>
            this.isRoot ? string.Empty : (this.start + LayoutGroupInfoConstants.GroupPathSeparator.ToString() + this.CurrentLevelPath);

        private string CurrentLevelPath
        {
            get
            {
                object[] objArray1 = new object[] { this.GetPrefix().ToString(), this.groupName, this.GetOrientation(), this.GetSuffix().ToString() };
                return string.Concat(objArray1);
            }
        }

        internal abstract DevExpress.Mvvm.Native.LayoutType LayoutType { get; }
    }
}

