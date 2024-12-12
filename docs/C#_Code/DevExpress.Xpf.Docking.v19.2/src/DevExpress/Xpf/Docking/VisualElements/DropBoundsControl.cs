namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Customization;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    [TemplatePart(Name="PART_Content", Type=typeof(FrameworkElement)), TemplatePart(Name="PART_Bounds", Type=typeof(FrameworkElement))]
    public class DropBoundsControl : ContentControl
    {
        public static readonly DependencyProperty DropTypeProperty;
        public static readonly DependencyProperty IsDragSourceProperty;
        public static readonly DependencyProperty LayoutItemProperty;
        public static readonly DependencyProperty UseOptimizedTemplateProperty;
        public static readonly DependencyProperty CenterElementTemplateProperty;
        public static readonly DependencyProperty SideElementTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsCenterElementVisibleProperty;
        protected BaseLayoutItem itemCore;
        private DockLayoutManager Container;
        private int lockChanged;
        private Splitter nextSplitter;
        private Splitter prevSplitter;

        static DropBoundsControl()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<DropBoundsControl> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<DropBoundsControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DevExpress.Xpf.Layout.Core.DropType>("DropType", ref DropTypeProperty, DevExpress.Xpf.Layout.Core.DropType.None, (dObj, e) => ((DropBoundsControl) dObj).OnDropTypeChanged((DevExpress.Xpf.Layout.Core.DropType) e.NewValue), null);
            registrator.Register<bool>("IsDragSource", ref IsDragSourceProperty, false, (dObj, e) => ((DropBoundsControl) dObj).OnIsDragSourceChanged((bool) e.NewValue), null);
            registrator.Register<BaseLayoutItem>("LayoutItem", ref LayoutItemProperty, null, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DropBoundsControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DropBoundsControl> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DropBoundsControl>.New().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DropBoundsControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DropBoundsControl.get_UseOptimizedTemplate)), parameters), out UseOptimizedTemplateProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DropBoundsControl), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DropBoundsControl> registrator2 = registrator1.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<DropBoundsControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DropBoundsControl.get_CenterElementTemplate)), expressionArray2), out CenterElementTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DropBoundsControl), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DropBoundsControl> registrator3 = registrator2.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<DropBoundsControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DropBoundsControl.get_SideElementTemplate)), expressionArray3), out SideElementTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DropBoundsControl), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator3.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DropBoundsControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DropBoundsControl.get_IsCenterElementVisible)), expressionArray4), out IsCenterElementVisibleProperty, false, frameworkOptions);
        }

        public DropBoundsControl()
        {
            base.Focusable = false;
            base.Loaded += new RoutedEventHandler(this.DropBoundsControl_Loaded);
            base.Unloaded += new RoutedEventHandler(this.DropBoundsControl_Unloaded);
        }

        private void ChangeSplitterSelection(DevExpress.Xpf.Layout.Core.DropType type)
        {
            if ((type != DevExpress.Xpf.Layout.Core.DropType.None) && (type != DevExpress.Xpf.Layout.Core.DropType.Center))
            {
                Splitter splitter = DropBoundsHelper.ChooseSplitter(type, this.prevSplitter, this.nextSplitter);
                if (splitter != null)
                {
                    splitter.IsDragDropOver = type.ToOrientation() == this.Item.Parent.Orientation;
                }
            }
            else
            {
                if (this.nextSplitter != null)
                {
                    this.nextSplitter.IsDragDropOver = false;
                }
                if (this.prevSplitter != null)
                {
                    this.prevSplitter.IsDragDropOver = false;
                }
            }
        }

        private FrameworkElement CreateElement(bool center)
        {
            Rectangle rectangle1 = new Rectangle();
            rectangle1.Opacity = 0.15;
            rectangle1.IsHitTestVisible = false;
            Rectangle target = rectangle1;
            if (center)
            {
                target.RadiusX = 2.0;
                target.RadiusY = 2.0;
                target.VerticalAlignment = VerticalAlignment.Center;
                target.HorizontalAlignment = HorizontalAlignment.Center;
                target.Visibility = Visibility.Collapsed;
            }
            BindingHelper.SetBinding(target, Shape.FillProperty, this, Control.BackgroundProperty, BindingMode.OneWay);
            return target;
        }

        private FrameworkElement CreateElement(DataTemplate fromTemplate, int row, int column, bool center = false)
        {
            FrameworkElement element = null;
            if (fromTemplate != null)
            {
                element = fromTemplate.LoadContent() as FrameworkElement;
            }
            element ??= this.CreateElement(center);
            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);
            return element;
        }

        private void DropBoundsControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Container = DockLayoutManager.FindManager(this);
            if (this.Container != null)
            {
                this.SubscribeEvents(this.Container);
            }
        }

        private void DropBoundsControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.Container != null)
            {
                this.UnsubscribeEvents(this.Container);
                this.Container = null;
            }
        }

        private void EnsureCollapsedWithAnimation()
        {
            Storyboard storyboard = new Storyboard {
                Children = { 
                    DropBoundsHelper.CreateGridLengthAnimation(this.Col0, ColumnDefinition.WidthProperty, this.Col0.Width, GridLengthAnimation.Zero),
                    DropBoundsHelper.CreateGridLengthAnimation(this.Col2, ColumnDefinition.WidthProperty, this.Col2.Width, GridLengthAnimation.Zero),
                    DropBoundsHelper.CreateGridLengthAnimation(this.Row0, RowDefinition.HeightProperty, this.Row0.Height, GridLengthAnimation.Zero),
                    DropBoundsHelper.CreateGridLengthAnimation(this.Row2, RowDefinition.HeightProperty, this.Row2.Height, GridLengthAnimation.Zero)
                }
            };
            storyboard.Completed += new EventHandler(this.OnCollapseAnimationCompleted);
            this.PartBounds.BeginStoryboard(storyboard);
            this.PartCenter.ClearValue(FrameworkElement.WidthProperty);
            this.PartCenter.ClearValue(FrameworkElement.HeightProperty);
        }

        private void EnsureSplitters()
        {
            this.nextSplitter = null;
            this.prevSplitter = null;
            if (this.Item.Parent != null)
            {
                ObservableCollection<object> itemsInternal = this.Item.Parent.ItemsInternal;
                int index = itemsInternal.IndexOf(this.Item);
                if ((index - 1) > 0)
                {
                    this.prevSplitter = itemsInternal[index - 1] as Splitter;
                }
                if ((index + 1) < (itemsInternal.Count - 1))
                {
                    this.nextSplitter = itemsInternal[index + 1] as Splitter;
                }
            }
        }

        private void EnsureVisualTree()
        {
            Grid partBounds = this.PartBounds as Grid;
            if ((partBounds != null) && (this.Row0 == null))
            {
                partBounds.RowDefinitions.Clear();
                partBounds.ColumnDefinitions.Clear();
                RowDefinition definition1 = new RowDefinition();
                definition1.Height = DefinitionsHelper.ZeroLength;
                this.Row0 = definition1;
                RowDefinition definition2 = new RowDefinition();
                definition2.Height = new GridLength(1.0, GridUnitType.Star);
                this.Row1 = definition2;
                RowDefinition definition3 = new RowDefinition();
                definition3.Height = DefinitionsHelper.ZeroLength;
                this.Row2 = definition3;
                ColumnDefinition definition4 = new ColumnDefinition();
                definition4.Width = DefinitionsHelper.ZeroLength;
                this.Col0 = definition4;
                ColumnDefinition definition5 = new ColumnDefinition();
                definition5.Width = new GridLength(1.0, GridUnitType.Star);
                this.Col1 = definition5;
                ColumnDefinition definition6 = new ColumnDefinition();
                definition6.Width = DefinitionsHelper.ZeroLength;
                this.Col2 = definition6;
                partBounds.RowDefinitions.Add(this.Row0);
                partBounds.RowDefinitions.Add(this.Row1);
                partBounds.RowDefinitions.Add(this.Row2);
                partBounds.ColumnDefinitions.Add(this.Col0);
                partBounds.ColumnDefinitions.Add(this.Col1);
                partBounds.ColumnDefinitions.Add(this.Col2);
                this.PartCenter = this.CreateElement(this.CenterElementTemplate, 1, 1, true);
                BindingHelper.SetBinding(this.PartCenter, UIElement.VisibilityProperty, this, IsCenterElementVisibleProperty, new BooleanToVisibilityConverter());
                partBounds.Children.Add(this.PartCenter);
                partBounds.Children.Add(this.CreateElement(this.SideElementTemplate, 0, 1, false));
                partBounds.Children.Add(this.CreateElement(this.SideElementTemplate, 2, 1, false));
                partBounds.Children.Add(this.CreateElement(this.SideElementTemplate, 1, 0, false));
                partBounds.Children.Add(this.CreateElement(this.SideElementTemplate, 1, 2, false));
            }
        }

        private void ExpandWithAnimation(DependencyObject element, DependencyProperty property, GridLength from, GridLength to)
        {
            Storyboard storyboard = new Storyboard {
                Children = { DropBoundsHelper.CreateGridLengthAnimation(element, property, from, to) }
            };
            storyboard.Completed += new EventHandler(this.OnExpandAnimationCompleted);
            this.PartBounds.BeginStoryboard(storyboard);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DockLayoutManager.Ensure(this, false);
            this.itemCore = base.DataContext as BaseLayoutItem;
            this.PartCenter = base.GetTemplateChild("PART_Center") as FrameworkElement;
            this.PartBounds = base.GetTemplateChild("PART_Bounds") as FrameworkElement;
            this.Row0 = base.GetTemplateChild("PART_Row0") as RowDefinition;
            this.Row1 = base.GetTemplateChild("PART_Row1") as RowDefinition;
            this.Row2 = base.GetTemplateChild("PART_Row2") as RowDefinition;
            this.Col0 = base.GetTemplateChild("PART_Col0") as ColumnDefinition;
            this.Col1 = base.GetTemplateChild("PART_Col1") as ColumnDefinition;
            this.Col2 = base.GetTemplateChild("PART_Col2") as ColumnDefinition;
        }

        private void OnCollapseAnimationCompleted(object sender, EventArgs e)
        {
            this.ChangeSplitterSelection(this.DropType);
        }

        private void OnDragInfoChanged(object sender, DragInfoChangedEventArgs e)
        {
            this.EnsureVisualTree();
            if (e.Info == null)
            {
                base.ClearValue(DropTypeProperty);
                base.ClearValue(IsDragSourceProperty);
            }
            else
            {
                this.DropType = DropBoundsHelper.CalcDropType(this.Item, e.Info);
                this.IsDragSource = ReferenceEquals(e.Info.Item, this.Item);
            }
            this.IsCenterElementVisible = this.DropType == DevExpress.Xpf.Layout.Core.DropType.Center;
        }

        protected virtual void OnDropTypeChanged(DevExpress.Xpf.Layout.Core.DropType type)
        {
            if (this.lockChanged <= 0)
            {
                this.lockChanged++;
                this.UpdateVisualState();
                this.EnsureSplitters();
                this.EnsureCollapsedWithAnimation();
                if (type != DevExpress.Xpf.Layout.Core.DropType.None)
                {
                    double num = 0.0;
                    if (this.Item.Parent != null)
                    {
                        Splitter splitter = DropBoundsHelper.ChooseSplitter(type, this.prevSplitter, this.nextSplitter);
                        bool flag = this.Item.Parent.Orientation == Orientation.Horizontal;
                        num = ((splitter == null) || (this.Item.Parent.Orientation != type.ToOrientation())) ? 0.0 : (flag ? splitter.ActualWidth : splitter.ActualHeight);
                        this.SelectSplitter(type, splitter);
                    }
                    double num2 = ((type.ToOrientation() != Orientation.Horizontal) || (this.Item.ItemType != LayoutItemType.ControlItem)) ? 14.0 : 32.0;
                    num2 = (num > num2) ? 4.0 : (num2 - num);
                    GridLength to = new GridLength(Math.Min(num2, Math.Max((double) 4.0, (double) (this.PartBounds.ActualWidth * 0.15))), GridUnitType.Pixel);
                    GridLength length2 = new GridLength(Math.Min(num2, Math.Max((double) 4.0, (double) (this.PartBounds.ActualHeight * 0.15))), GridUnitType.Pixel);
                    switch (type)
                    {
                        case DevExpress.Xpf.Layout.Core.DropType.Center:
                            if (this.CanShowCenterZone)
                            {
                                this.ShowCenterZoneWithAnimation(this.PartBounds.ActualWidth * 0.6, this.PartBounds.ActualHeight * 0.6);
                            }
                            break;

                        case DevExpress.Xpf.Layout.Core.DropType.Top:
                            this.ExpandWithAnimation(this.Row0, RowDefinition.HeightProperty, GridLengthAnimation.Zero, length2);
                            break;

                        case DevExpress.Xpf.Layout.Core.DropType.Bottom:
                            this.ExpandWithAnimation(this.Row2, RowDefinition.HeightProperty, GridLengthAnimation.Zero, length2);
                            break;

                        case DevExpress.Xpf.Layout.Core.DropType.Left:
                            this.ExpandWithAnimation(this.Col0, ColumnDefinition.WidthProperty, GridLengthAnimation.Zero, to);
                            break;

                        case DevExpress.Xpf.Layout.Core.DropType.Right:
                            this.ExpandWithAnimation(this.Col2, ColumnDefinition.WidthProperty, GridLengthAnimation.Zero, to);
                            break;

                        default:
                            break;
                    }
                }
                this.lockChanged--;
            }
        }

        private void OnExpandAnimationCompleted(object sender, EventArgs e)
        {
            this.ChangeSplitterSelection(this.DropType);
        }

        protected virtual void OnIsDragSourceChanged(bool newValue)
        {
            this.UpdateVisualState();
        }

        private void SelectSplitter(DevExpress.Xpf.Layout.Core.DropType type, Splitter splitter)
        {
            if ((splitter != null) && ((type != DevExpress.Xpf.Layout.Core.DropType.None) && ((type != DevExpress.Xpf.Layout.Core.DropType.Center) && (type.ToOrientation() == this.Item.Parent.Orientation))))
            {
                splitter.IsDragDropOver = true;
            }
        }

        private void ShowCenterZoneWithAnimation(double width, double height)
        {
            Storyboard storyboard = new Storyboard {
                Children = { 
                    DropBoundsHelper.CreateDoubleAnimation(this.PartCenter, FrameworkElement.WidthProperty, 0.0, width),
                    DropBoundsHelper.CreateDoubleAnimation(this.PartCenter, FrameworkElement.HeightProperty, 0.0, height)
                }
            };
            this.PartCenter.BeginStoryboard(storyboard);
        }

        protected virtual void SubscribeEvents(DockLayoutManager manager)
        {
            if (!manager.IsDisposing && (manager.CustomizationController != null))
            {
                manager.CustomizationController.DragInfoChanged += new DragInfoChangedEventHandler(this.OnDragInfoChanged);
            }
        }

        protected virtual void UnsubscribeEvents(DockLayoutManager manager)
        {
            if (!manager.IsDisposing && (manager.CustomizationController != null))
            {
                manager.CustomizationController.DragInfoChanged -= new DragInfoChangedEventHandler(this.OnDragInfoChanged);
            }
        }

        protected virtual void UpdateVisualState()
        {
            if (this.DropType == DevExpress.Xpf.Layout.Core.DropType.Center)
            {
                VisualStateManager.GoToState(this, "DropCenterState", false);
            }
            else
            {
                VisualStateManager.GoToState(this, this.IsDragSource ? "DragSourceState" : "EmptyDraggingState", false);
            }
        }

        public DataTemplate CenterElementTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CenterElementTemplateProperty);
            set => 
                base.SetValue(CenterElementTemplateProperty, value);
        }

        public DevExpress.Xpf.Layout.Core.DropType DropType
        {
            get => 
                (DevExpress.Xpf.Layout.Core.DropType) base.GetValue(DropTypeProperty);
            set => 
                base.SetValue(DropTypeProperty, value);
        }

        public bool IsDragSource
        {
            get => 
                (bool) base.GetValue(IsDragSourceProperty);
            set => 
                base.SetValue(IsDragSourceProperty, value);
        }

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            set => 
                base.SetValue(LayoutItemProperty, value);
        }

        public DataTemplate SideElementTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SideElementTemplateProperty);
            set => 
                base.SetValue(SideElementTemplateProperty, value);
        }

        public bool UseOptimizedTemplate
        {
            get => 
                (bool) base.GetValue(UseOptimizedTemplateProperty);
            set => 
                base.SetValue(UseOptimizedTemplateProperty, value);
        }

        protected ColumnDefinition Col0 { get; private set; }

        protected ColumnDefinition Col1 { get; private set; }

        protected ColumnDefinition Col2 { get; private set; }

        protected BaseLayoutItem Item =>
            this.LayoutItem ?? this.itemCore;

        protected FrameworkElement PartBounds { get; private set; }

        protected FrameworkElement PartCenter { get; private set; }

        protected RowDefinition Row0 { get; private set; }

        protected RowDefinition Row1 { get; private set; }

        protected RowDefinition Row2 { get; private set; }

        private bool CanShowCenterZone =>
            (this.Item is LayoutGroup) && (((LayoutGroup) this.Item).GroupBorderStyle != GroupBorderStyle.Tabbed);

        private bool IsCenterElementVisible
        {
            get => 
                (bool) base.GetValue(IsCenterElementVisibleProperty);
            set => 
                base.SetValue(IsCenterElementVisibleProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DropBoundsControl.<>c <>9 = new DropBoundsControl.<>c();

            internal void <.cctor>b__7_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DropBoundsControl) dObj).OnDropTypeChanged((DropType) e.NewValue);
            }

            internal void <.cctor>b__7_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DropBoundsControl) dObj).OnIsDragSourceChanged((bool) e.NewValue);
            }
        }
    }
}

