namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class GroupPanel : psvGrid
    {
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty LastChildFillProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty LayoutItemProperty;
        private bool isLayoutDirty;
        private readonly Locker layoutItemLocker = new Locker();
        private double defectiveMeasureCount;
        private DocumentGroup documentHost;
        private readonly Locker rebuildLocker = new Locker();

        static GroupPanel()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<GroupPanel> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<GroupPanel>();
            registrator.Register<System.Windows.Controls.Orientation>("Orientation", ref OrientationProperty, System.Windows.Controls.Orientation.Horizontal, (dObj, e) => ((GroupPanel) dObj).OnOrientatonChanged((System.Windows.Controls.Orientation) e.NewValue), null);
            registrator.Register<bool>("LastChildFill", ref LastChildFillProperty, true, (dObj, e) => ((GroupPanel) dObj).OnLastChildFillChanged((bool) e.NewValue), null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(GroupPanel), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<GroupPanel>.New().AddOwner<BaseLayoutItem>(System.Linq.Expressions.Expression.Lambda<Func<GroupPanel, BaseLayoutItem>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(DockLayoutManager.GetLayoutItem), arguments), parameters), out LayoutItemProperty, DockLayoutManager.LayoutItemProperty, null, (d, oldValue, newValue) => d.OnLayoutItemChanged(oldValue, newValue));
        }

        private bool CanSkipGroup(LayoutGroup group)
        {
            if (group == null)
            {
                return false;
            }
            if (group is FloatGroup)
            {
                return false;
            }
            DocumentGroup objA = group as DocumentGroup;
            return (((objA == null) || (objA.Parent == null)) ? (!group.HasNotCollapsedItems && ((group.GroupBorderStyle == GroupBorderStyle.NoBorder) && !group.GetIsDocumentHost())) : (!objA.HasNotCollapsedItems && !ReferenceEquals(objA, this.DocumentHost)));
        }

        private bool CanSkipItem(BaseLayoutItem item) => 
            (item.Visibility == Visibility.Collapsed) || this.CanSkipGroup(item as LayoutGroup);

        protected void ClearParameters()
        {
            base.RowDefinitions.Clear();
            base.ColumnDefinitions.Clear();
        }

        private bool CorrectDefectiveMeasure(Size constraint)
        {
            if (this.defectiveMeasureCount == 0.0)
            {
                return false;
            }
            this.defectiveMeasureCount = 0.0;
            double num = 0.0;
            double num2 = 0.0;
            foreach (UIElement element in base.InternalChildren)
            {
                BaseLayoutItem layoutItem = GetLayoutItem(element);
                if (layoutItem != null)
                {
                    DefinitionBase definition = DefinitionsHelper.GetDefinition(layoutItem);
                    if (definition != null)
                    {
                        GridLength length = definition.GetLength();
                        if (length.IsStar)
                        {
                            num += length.Value;
                            num2 += definition.GetActualLength();
                        }
                    }
                }
            }
            if (num2 == 0.0)
            {
                return false;
            }
            bool flag = false;
            foreach (UIElement element2 in base.InternalChildren)
            {
                BaseLayoutItem layoutItem = GetLayoutItem(element2);
                if (layoutItem != null)
                {
                    DefinitionBase definition = DefinitionsHelper.GetDefinition(layoutItem);
                    if ((definition != null) && definition.GetLength().IsStar)
                    {
                        double actualLength = definition.GetActualLength();
                        double num4 = this.IsHorizontal ? layoutItem.ActualMinSize.Width : layoutItem.ActualMinSize.Height;
                        GridLength objA = (GridLength) layoutItem.GetValue((this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? BaseLayoutItem.ItemWidthProperty : BaseLayoutItem.ItemHeightProperty);
                        if ((actualLength > num4) && objA.IsStar)
                        {
                            GridLength objB = new GridLength((actualLength * num) / num2, GridUnitType.Star);
                            if (!Equals(objA, objB) && (objB != DefinitionsHelper.ZeroStarLength))
                            {
                                layoutItem.SetValue((this.Orientation == System.Windows.Controls.Orientation.Horizontal) ? BaseLayoutItem.ItemWidthProperty : BaseLayoutItem.ItemHeightProperty, objB);
                                flag = true;
                            }
                        }
                    }
                }
            }
            return flag;
        }

        protected override UIElementCollection CreateUIElementCollection(FrameworkElement logicalParent) => 
            new UIElementCollection(this, logicalParent);

        private DocumentGroup GetDocumentHost(LayoutGroup ownerGroup)
        {
            DocumentGroup group = null;
            if (ownerGroup != null)
            {
                IEnumerable<DocumentGroup> source = ownerGroup.GetNestedItems().OfType<DocumentGroup>();
                Func<DocumentGroup, bool> predicate = <>c.<>9__29_0;
                if (<>c.<>9__29_0 == null)
                {
                    Func<DocumentGroup, bool> local1 = <>c.<>9__29_0;
                    predicate = <>c.<>9__29_0 = x => x.HasNotCollapsedItems;
                }
                group = source.FirstOrDefault<DocumentGroup>(predicate);
                if (group == null)
                {
                    Func<DocumentGroup, bool> func2 = <>c.<>9__29_1;
                    if (<>c.<>9__29_1 == null)
                    {
                        Func<DocumentGroup, bool> local2 = <>c.<>9__29_1;
                        func2 = <>c.<>9__29_1 = x => x.ShowWhenEmpty;
                    }
                    group = source.FirstOrDefault<DocumentGroup>(func2);
                    if (group == null)
                    {
                        group = source.FirstOrDefault<DocumentGroup>();
                    }
                }
            }
            return group;
        }

        private static BaseLayoutItem GetLayoutItem(DependencyObject element)
        {
            // Unresolved stack state at '00000055'
        }

        private double GetLength(Size s, bool isHorizontal) => 
            isHorizontal ? s.Width : s.Height;

        private LayoutGroup GetOwnerGroup() => 
            DockLayoutManager.GetLayoutItem(this) as LayoutGroup;

        internal void InvalidateLayout()
        {
            this.isLayoutDirty = true;
            base.InvalidateMeasure();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.isLayoutDirty)
            {
                this.RebuildLayout();
            }
            bool flag = true;
            if (this.defectiveMeasureCount > 0.0)
            {
                flag = this.CorrectDefectiveMeasure(constraint);
            }
            Size s = base.MeasureOverride(constraint);
            double length = this.GetLength(constraint, this.IsHorizontal);
            double num2 = this.GetLength(s, this.IsHorizontal);
            if (flag && (((num2 - length) > 1.0) && (length > 0.0)))
            {
                this.defectiveMeasureCount++;
                this.QueueInvlidateMeasure();
            }
            return s;
        }

        protected virtual void OnLastChildFillChanged(bool value)
        {
            this.InvalidateLayout();
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem oldValue, BaseLayoutItem newValue)
        {
            try
            {
                using (this.layoutItemLocker.Lock())
                {
                    if (newValue is LayoutGroup)
                    {
                        newValue.Forward(this, LastChildFillProperty, "LastChildFill", BindingMode.OneWay);
                        newValue.Forward(this, OrientationProperty, "Orientation", BindingMode.OneWay);
                    }
                    else
                    {
                        base.ClearValue(LastChildFillProperty);
                        base.ClearValue(OrientationProperty);
                    }
                }
            }
            finally
            {
                this.InvalidateLayout();
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (this.isLayoutDirty)
            {
                this.RebuildLayout();
            }
        }

        protected virtual void OnOrientatonChanged(System.Windows.Controls.Orientation orientation)
        {
            this.InvalidateLayout();
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            if (visualRemoved != null)
            {
                Splitter splitter1 = visualRemoved as Splitter;
                BaseLayoutItem target = (BaseLayoutItem) splitter1;
                if (splitter1 == null)
                {
                    Splitter local1 = splitter1;
                    BaseLayoutItem layoutItem = GetLayoutItem(visualRemoved);
                    target = layoutItem;
                    if (layoutItem == null)
                    {
                        BaseLayoutItem local2 = layoutItem;
                        target = (BaseLayoutItem) visualRemoved;
                    }
                }
                DefinitionsHelper.SetDefinition(target, null);
            }
        }

        private void QueueInvlidateMeasure()
        {
            base.Dispatcher.BeginInvoke(new Action(this.InvalidateMeasure), new object[0]);
        }

        protected internal virtual void RebuildLayout()
        {
            if (!this.rebuildLocker && !this.layoutItemLocker)
            {
                using (this.rebuildLocker.Lock())
                {
                    try
                    {
                        if (!this.SkipRebuildLayout())
                        {
                            this.ClearParameters();
                            int num = 0;
                            bool horizontal = this.Orientation == System.Windows.Controls.Orientation.Horizontal;
                            bool flag2 = false;
                            this.documentHost = this.GetDocumentHost(this.GetOwnerGroup());
                            using (IEnumerator enumerator = base.Children.GetEnumerator())
                            {
                                while (true)
                                {
                                    if (!enumerator.MoveNext())
                                    {
                                        break;
                                    }
                                    UIElement current = (UIElement) enumerator.Current;
                                    BaseLayoutItem source = GetLayoutItem(current) ?? (current as Splitter).With<Splitter, LayoutGroup>((<>c.<>9__32_0 ??= x => x.LayoutGroup));
                                    if (source != null)
                                    {
                                        if (current is Splitter)
                                        {
                                            GridLength length;
                                            BaseLayoutItem layoutItem = GetLayoutItem(base.Children[num + 1]);
                                            if (!(!this.CanSkipItem(layoutItem) & flag2))
                                            {
                                                current.Visibility = Visibility.Collapsed;
                                            }
                                            else
                                            {
                                                flag2 = false;
                                                current.Visibility = Visibility.Visible;
                                            }
                                            ((Splitter) current).InitSplitThumb(horizontal);
                                            DependencyProperty targetProperty = IntervalHelper.GetTargetProperty(GetLayoutItem(base.Children[num - 1]), GetLayoutItem(base.Children[num + 1]));
                                            DoubleToGridLengthConverter converter = new DoubleToGridLengthConverter();
                                            if (horizontal)
                                            {
                                                length = new GridLength();
                                                ColumnDefinition definition1 = new ColumnDefinition();
                                                definition1.Width = length;
                                                ColumnDefinition target = definition1;
                                                if (current.Visibility == Visibility.Visible)
                                                {
                                                    BindingHelper.SetBinding(target, ColumnDefinition.WidthProperty, source, targetProperty, converter);
                                                }
                                                DefinitionsHelper.SetDefinition(current, target);
                                                base.ColumnDefinitions.Add(target);
                                            }
                                            else
                                            {
                                                length = new GridLength();
                                                RowDefinition definition5 = new RowDefinition();
                                                definition5.Height = length;
                                                RowDefinition target = definition5;
                                                if (current.Visibility == Visibility.Visible)
                                                {
                                                    BindingHelper.SetBinding(target, RowDefinition.HeightProperty, source, targetProperty, converter);
                                                }
                                                DefinitionsHelper.SetDefinition(current, target);
                                                base.RowDefinitions.Add(target);
                                            }
                                        }
                                        else
                                        {
                                            if (source is LayoutControlItem)
                                            {
                                                source.ClearValue(LayoutControlItem.DesiredSizeInternalProperty);
                                            }
                                            bool flag3 = !this.CanSkipItem(source);
                                            if (flag3)
                                            {
                                                flag2 = true;
                                            }
                                            if (horizontal)
                                            {
                                                ColumnDefinition definition6 = new ColumnDefinition();
                                                definition6.Width = DefinitionsHelper.ZeroLength;
                                                ColumnDefinition target = definition6;
                                                if (flag3)
                                                {
                                                    BindingHelper.SetBinding(target, ColumnDefinition.WidthProperty, source, "ItemWidth");
                                                    target.SetMinSize(source.ActualMinSize);
                                                    target.SetMaxSize(source.ActualMaxSize);
                                                }
                                                DefinitionsHelper.SetDefinition(source, target);
                                                base.ColumnDefinitions.Add(target);
                                            }
                                            else
                                            {
                                                RowDefinition definition7 = new RowDefinition();
                                                definition7.Height = DefinitionsHelper.ZeroLength;
                                                RowDefinition target = definition7;
                                                if (flag3)
                                                {
                                                    BindingHelper.SetBinding(target, RowDefinition.HeightProperty, source, "ItemHeight");
                                                    target.SetMinSize(source.ActualMinSize);
                                                    target.SetMaxSize(source.ActualMaxSize);
                                                }
                                                DefinitionsHelper.SetDefinition(source, target);
                                                base.RowDefinitions.Add(target);
                                            }
                                        }
                                        current.SetValue(horizontal ? Grid.ColumnProperty : Grid.RowProperty, num++);
                                        continue;
                                    }
                                    return;
                                }
                            }
                            this.UpdateDefinitionsForSingleChild();
                            this.isLayoutDirty = false;
                        }
                    }
                    finally
                    {
                        this.documentHost = null;
                    }
                }
            }
        }

        protected virtual bool SkipRebuildLayout() => 
            base.IsDisposing || (base.RowDefinitions.IsReadOnly || base.ColumnDefinitions.IsReadOnly);

        private void UpdateDefinitionsForSingleChild()
        {
            if (this.LastChildFill)
            {
                if ((base.RowDefinitions.Count == 1) && !DefinitionsHelper.IsZero(base.RowDefinitions[0]))
                {
                    base.RowDefinitions[0].ClearValue(RowDefinition.HeightProperty);
                }
                if ((base.ColumnDefinitions.Count == 1) && !DefinitionsHelper.IsZero(base.ColumnDefinitions[0]))
                {
                    base.ColumnDefinitions[0].ClearValue(ColumnDefinition.WidthProperty);
                }
            }
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }

        public bool LastChildFill
        {
            get => 
                (bool) base.GetValue(LastChildFillProperty);
            set => 
                base.SetValue(LastChildFillProperty, value);
        }

        private bool IsHorizontal =>
            this.Orientation == System.Windows.Controls.Orientation.Horizontal;

        private DocumentGroup DocumentHost =>
            this.documentHost;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupPanel.<>c <>9 = new GroupPanel.<>c();
            public static Func<DocumentGroup, bool> <>9__29_0;
            public static Func<DocumentGroup, bool> <>9__29_1;
            public static Func<Splitter, LayoutGroup> <>9__32_0;
            public static Func<MultiTemplateControl, BaseLayoutItem> <>9__34_0;
            public static Func<BaseLayoutItem> <>9__34_1;

            internal void <.cctor>b__3_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((GroupPanel) dObj).OnOrientatonChanged((Orientation) e.NewValue);
            }

            internal void <.cctor>b__3_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((GroupPanel) dObj).OnLastChildFillChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__3_2(GroupPanel d, BaseLayoutItem oldValue, BaseLayoutItem newValue)
            {
                d.OnLayoutItemChanged(oldValue, newValue);
            }

            internal bool <GetDocumentHost>b__29_0(DocumentGroup x) => 
                x.HasNotCollapsedItems;

            internal bool <GetDocumentHost>b__29_1(DocumentGroup x) => 
                x.ShowWhenEmpty;

            internal BaseLayoutItem <GetLayoutItem>b__34_0(MultiTemplateControl x) => 
                x.LayoutItem;

            internal BaseLayoutItem <GetLayoutItem>b__34_1() => 
                null;

            internal LayoutGroup <RebuildLayout>b__32_0(Splitter x) => 
                x.LayoutGroup;
        }
    }
}

