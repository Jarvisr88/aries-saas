namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class GridColumnHeaderBase : Button
    {
        public static readonly DependencyProperty IsSelectedInDesignTimeProperty;
        public static readonly DependencyProperty ColumnPositionProperty;
        public static readonly DependencyProperty DataColumnPositionProperty;
        private static readonly DependencyPropertyKey DataColumnPositionPropertyKey;
        public static readonly DependencyProperty HasTopElementProperty;
        public static readonly DependencyProperty HasBottomElementProperty;
        public static readonly DependencyProperty HasRightSiblingProperty;
        public static readonly DependencyProperty HasLeftSiblingProperty;

        static GridColumnHeaderBase()
        {
            Type ownerType = typeof(GridColumnHeaderBase);
            IsSelectedInDesignTimeProperty = DependencyPropertyManager.RegisterAttached("IsSelectedInDesignTime", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(GridColumnHeaderBase.OnIsSelectedInDesignTimeChanged)));
            ColumnPositionProperty = DependencyPropertyManager.Register("ColumnPosition", typeof(DevExpress.Xpf.Grid.ColumnPosition), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.ColumnPosition.Middle, (d, e) => ((GridColumnHeaderBase) d).OnColumnPositionChanged()));
            DataColumnPositionPropertyKey = DependencyProperty.RegisterReadOnly("DataColumnPosition", typeof(DevExpress.Xpf.Grid.ColumnPosition), ownerType, new PropertyMetadata(DevExpress.Xpf.Grid.ColumnPosition.Standalone));
            DataColumnPositionProperty = DataColumnPositionPropertyKey.DependencyProperty;
            HasTopElementProperty = DependencyPropertyManager.Register("HasTopElement", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((GridColumnHeaderBase) d).OnColumnPositionChanged()));
            HasBottomElementProperty = DependencyPropertyManager.Register("HasBottomElement", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((GridColumnHeaderBase) d).OnHasBottomElementChanged()));
            HasRightSiblingProperty = DependencyProperty.Register("HasRightSibling", typeof(bool), typeof(GridColumnHeaderBase), new PropertyMetadata(true));
            HasLeftSiblingProperty = DependencyProperty.Register("HasLeftSibling", typeof(bool), typeof(GridColumnHeaderBase), new PropertyMetadata(false));
        }

        public GridColumnHeaderBase()
        {
            this.UpdateDataColumnPosition();
        }

        public static bool GetIsSelectedInDesignTime(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(IsSelectedInDesignTimeProperty);
        }

        protected virtual void OnColumnPositionChanged()
        {
            this.UpdateHasEmptySiblingState();
            this.UpdateDataColumnPosition();
        }

        private void OnHasBottomElementChanged()
        {
        }

        private void OnIsSelectedInDesignTimeChanged()
        {
            this.UpdateDesignTimeSelectionControl();
            this.OnIsSelectedInDesignTimeChangedCore();
        }

        private static void OnIsSelectedInDesignTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GridColumnHeaderBase)
            {
                ((GridColumnHeaderBase) d).OnIsSelectedInDesignTimeChanged();
            }
        }

        protected virtual void OnIsSelectedInDesignTimeChangedCore()
        {
        }

        protected internal virtual void SetIsBestFitElement()
        {
        }

        public static void SetIsSelectedInDesignTime(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(IsSelectedInDesignTimeProperty, value);
        }

        protected virtual void UpdateDataColumnPosition()
        {
            this.DataColumnPosition = this.ColumnPosition;
        }

        protected virtual void UpdateDesignTimeSelectionControl()
        {
        }

        protected virtual void UpdateHasEmptySiblingState()
        {
        }

        [Description("Gets or sets a column's position within a View. This is a dependency property.")]
        public DevExpress.Xpf.Grid.ColumnPosition ColumnPosition
        {
            get => 
                (DevExpress.Xpf.Grid.ColumnPosition) base.GetValue(ColumnPositionProperty);
            set => 
                base.SetValue(ColumnPositionProperty, value);
        }

        [Description("Gets a column's position within a View. This is a dependency property.")]
        public DevExpress.Xpf.Grid.ColumnPosition DataColumnPosition
        {
            get => 
                (DevExpress.Xpf.Grid.ColumnPosition) base.GetValue(DataColumnPositionProperty);
            protected set => 
                base.SetValue(DataColumnPositionPropertyKey, value);
        }

        [Description("This member supports the internal infrastructure and is not intended to be used directly from your code.")]
        public bool HasTopElement
        {
            get => 
                (bool) base.GetValue(HasTopElementProperty);
            set => 
                base.SetValue(HasTopElementProperty, value);
        }

        [Description("This member supports the internal infrastructure and is not intended to be used directly from your code.")]
        public bool HasBottomElement
        {
            get => 
                (bool) base.GetValue(HasBottomElementProperty);
            set => 
                base.SetValue(HasBottomElementProperty, value);
        }

        public bool HasRightSibling
        {
            get => 
                (bool) base.GetValue(HasRightSiblingProperty);
            set => 
                base.SetValue(HasRightSiblingProperty, value);
        }

        public bool HasLeftSibling
        {
            get => 
                (bool) base.GetValue(HasLeftSiblingProperty);
            set => 
                base.SetValue(HasLeftSiblingProperty, value);
        }

        protected FrameworkElement DesignTimeSelectionControl { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridColumnHeaderBase.<>c <>9 = new GridColumnHeaderBase.<>c();

            internal void <.cctor>b__8_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridColumnHeaderBase) d).OnColumnPositionChanged();
            }

            internal void <.cctor>b__8_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridColumnHeaderBase) d).OnColumnPositionChanged();
            }

            internal void <.cctor>b__8_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridColumnHeaderBase) d).OnHasBottomElementChanged();
            }
        }
    }
}

