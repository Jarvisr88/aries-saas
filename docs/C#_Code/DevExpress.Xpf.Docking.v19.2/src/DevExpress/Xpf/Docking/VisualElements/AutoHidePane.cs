namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    [TemplatePart(Name="PART_PaneContent", Type=typeof(FrameworkElement)), TemplatePart(Name="PART_Sizer", Type=typeof(ElementSizer))]
    public class AutoHidePane : psvContentControl, IUIElement
    {
        public static readonly DependencyProperty PanelSizeProperty;
        public static readonly DependencyProperty DockTypeProperty;
        private static readonly DependencyPropertyKey DockTypePropertyKey;
        public static readonly DependencyProperty AutoHideTrayProperty;
        private static readonly DependencyPropertyKey IsCollapsedPropertyKey;
        public static readonly DependencyProperty IsCollapsedProperty;
        private static readonly DependencyPropertyKey IsSizerVisiblePropertyKey;
        public static readonly DependencyProperty IsSizerVisibleProperty;
        public static readonly DependencyProperty DisplayModeProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty AutoHideSizeProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty SizeToContentProperty;
        public static readonly DependencyProperty ViewStyleProperty;
        private readonly Locker containerSizeLocker = new Locker();
        private readonly WeakList<EventHandler> layoutItemChangedHandlers = new WeakList<EventHandler>();
        private AnimationContext _Context;
        private AutoHideGroup animatedGroup;
        private System.Windows.Size arrangeSize;
        private System.Windows.Size borderSize;
        private bool firstMeasure = true;
        private int isClosing;
        private int isSizing;
        private UIChildren uiChildren = new UIChildren();

        internal event EventHandler LayoutItemChanged
        {
            add
            {
                this.layoutItemChangedHandlers.Add(value);
            }
            remove
            {
                this.layoutItemChangedHandlers.Remove(value);
            }
        }

        static AutoHidePane()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHidePane> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHidePane>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<double>("PanelSize", ref PanelSizeProperty, 0.0, FrameworkPropertyMetadataOptions.AffectsMeasure, null, (dObj, value) => ((AutoHidePane) dObj).CoercePanelSize((double) value));
            registrator.RegisterReadonly<Dock>("DockType", ref DockTypePropertyKey, ref DockTypeProperty, Dock.Left, null, null);
            registrator.Register<DevExpress.Xpf.Docking.VisualElements.AutoHideTray>("AutoHideTray", ref AutoHideTrayProperty, null, (dObj, e) => ((AutoHidePane) dObj).OnAutoHideTrayChanged(e.OldValue as DevExpress.Xpf.Docking.VisualElements.AutoHideTray, e.NewValue as DevExpress.Xpf.Docking.VisualElements.AutoHideTray), null);
            registrator.RegisterReadonly<bool>("IsCollapsed", ref IsCollapsedPropertyKey, ref IsCollapsedProperty, true, (dObj, e) => ((AutoHidePane) dObj).OnIsCollapsedChanged((bool) e.OldValue, (bool) e.NewValue), null);
            registrator.RegisterReadonly<bool>("IsSizerVisible", ref IsSizerVisiblePropertyKey, ref IsSizerVisibleProperty, false, null, null);
            registrator.Register<AutoHideMode>("DisplayMode", ref DisplayModeProperty, AutoHideMode.Default, (dObj, e) => ((AutoHidePane) dObj).OnDisplayModeChanged((AutoHideMode) e.OldValue, (AutoHideMode) e.NewValue), null);
            System.Windows.Size defValue = new System.Windows.Size();
            registrator.Register<System.Windows.Size>("AutoHideSize", ref AutoHideSizeProperty, defValue, (d, e) => ((AutoHidePane) d).OnAutoHideSizeChanged((System.Windows.Size) e.OldValue, (System.Windows.Size) e.NewValue), null);
            registrator.Register<System.Windows.SizeToContent>("SizeToContent", ref SizeToContentProperty, System.Windows.SizeToContent.Manual, (d, e) => ((AutoHidePane) d).OnSizeToContentChanged((System.Windows.SizeToContent) e.OldValue, (System.Windows.SizeToContent) e.NewValue), null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(AutoHidePane), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<AutoHidePane>.New().Register<DockingViewStyle>(System.Linq.Expressions.Expression.Lambda<Func<AutoHidePane, DockingViewStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AutoHidePane.get_ViewStyle)), parameters), out ViewStyleProperty, DockingViewStyle.Default, frameworkOptions);
        }

        public AutoHidePane()
        {
            ContentControlHelper.SetContentIsNotLogical(this, true);
        }

        private void ApplyPanelSize(double size = (double) 1.0 / (double) 0.0)
        {
            double num = double.IsNaN(size) ? this.Size : size;
            this.PanelSize = this.Size = num;
            base.BeginAnimation(PanelSizeProperty, null);
            if (this.GetSizeToContent(this.IsHorz))
            {
                this.PanelSize = double.NaN;
            }
            this.AutoHideTray.IsAnimated = false;
            AutoHideGroup autoHideGroup = this.GetAutoHideGroup();
            if (autoHideGroup != null)
            {
                autoHideGroup.SelectedTabIndex = autoHideGroup.Items.IndexOf(base.LayoutItem);
                autoHideGroup.IsAnimated = false;
                autoHideGroup.IsExpanded = true;
            }
            this.animatedGroup = null;
        }

        protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeBounds)
        {
            this.arrangeSize = base.ArrangeOverride(arrangeBounds);
            return this.arrangeSize;
        }

        private void BindBorderCursor()
        {
            Cursor cursor = this.DockType.ToCursor();
            ConditionalCursorConverter converter = new ConditionalCursorConverter();
            converter.Cursor = cursor;
            BindingHelper.SetBinding(this.PartPaneBorder, FrameworkElement.CursorProperty, base.LayoutItem, BaseLayoutItem.AllowSizingProperty, converter);
        }

        private void BindPanelSizeToContentSize()
        {
            bool flag = DevExpress.Xpf.Docking.VisualElements.AutoHideTray.GetOrientation(this) == System.Windows.Controls.Orientation.Vertical;
            BindingHelper.SetBinding(this.PartPaneContentPresenter, flag ? FrameworkElement.WidthProperty : FrameworkElement.HeightProperty, this, "PanelSize");
        }

        private double CalcPanelSize(PanelSizeAction action, AutoHideGroup aGroup, BaseLayoutItem layoutItem, double desiredSize = (double) 1.0 / (double) 0.0)
        {
            bool isHorz = this.IsHorz;
            Func<DevExpress.Xpf.Docking.LayoutPanel, bool> evaluator = <>c.<>9__115_0;
            if (<>c.<>9__115_0 == null)
            {
                Func<DevExpress.Xpf.Docking.LayoutPanel, bool> local1 = <>c.<>9__115_0;
                evaluator = <>c.<>9__115_0 = x => x.AutoHideExpandState == AutoHideExpandState.Expanded;
            }
            if (this.LayoutPanel.Return<DevExpress.Xpf.Docking.LayoutPanel, bool>(evaluator, <>c.<>9__115_1 ??= () => false))
            {
                action = PanelSizeAction.Maximize;
            }
            double availableAutoHideSize = this.GetAvailableAutoHideSize(isHorz);
            bool flag3 = !double.IsNaN(availableAutoHideSize);
            double num2 = 0.0;
            switch (action)
            {
                case PanelSizeAction.Expand:
                {
                    System.Windows.Size actualAutoHideSize = aGroup.GetActualAutoHideSize(layoutItem);
                    num2 = MathHelper.MeasureDimension(isHorz ? aGroup.ActualMinSize.Width : aGroup.ActualMinSize.Height, isHorz ? aGroup.ActualMaxSize.Width : aGroup.ActualMaxSize.Height, isHorz ? actualAutoHideSize.Width : actualAutoHideSize.Height);
                    if (flag3)
                    {
                        num2 = Math.Min(num2, availableAutoHideSize);
                    }
                    break;
                }
                case PanelSizeAction.Maximize:
                    if (flag3)
                    {
                        num2 = availableAutoHideSize;
                    }
                    num2 = MathHelper.MeasureDimension(isHorz ? aGroup.ActualMinSize.Width : aGroup.ActualMinSize.Height, isHorz ? aGroup.ActualMaxSize.Width : aGroup.ActualMaxSize.Height, num2);
                    break;

                case PanelSizeAction.Resize:
                {
                    double num3 = isHorz ? (this.GetActualSize(isHorz) - layoutItem.ActualWidth) : (this.GetActualSize(isHorz) - layoutItem.ActualHeight);
                    num2 = MathHelper.MeasureDimension(isHorz ? (layoutItem.ActualMinSize.Width + num3) : (layoutItem.ActualMinSize.Height + num3), isHorz ? layoutItem.ActualMaxSize.Width : layoutItem.ActualMaxSize.Height, desiredSize);
                    num2 = MathHelper.MeasureDimension(isHorz ? aGroup.ActualMinSize.Width : aGroup.ActualMinSize.Height, isHorz ? aGroup.ActualMaxSize.Width : aGroup.ActualMaxSize.Height, num2);
                    if (flag3)
                    {
                        num2 = Math.Min(num2, availableAutoHideSize);
                    }
                    break;
                }
                default:
                    break;
            }
            return num2;
        }

        private void CancelAnimation()
        {
            base.BeginAnimation(PanelSizeProperty, null);
            if ((this.ExpandAnimation != null) || (this.CollapseAnimation != null))
            {
                if (this.ExpandAnimation != null)
                {
                    this.ExpandAnimation.Completed -= new EventHandler(this.OnExpandAnimationCompleted);
                    this.ExpandAnimation = null;
                }
                if (this.CollapseAnimation != null)
                {
                    this.CollapseAnimation.Completed -= new EventHandler(this.OnCollapseAnimationCompleted);
                    this.CollapseAnimation = null;
                }
            }
        }

        protected virtual bool CanCollapseCore()
        {
            BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(this);
            return (!((layoutItem != null) && layoutItem.IsActive) && (!KeyboardFocusHelper.IsKeyboardFocused(this) && !KeyboardFocusHelper.IsKeyboardFocusWithin(this)));
        }

        protected virtual bool CanHideCurrentItemCore()
        {
            if (KeyboardFocusHelper.IsKeyboardFocusWithin(this))
            {
                DependencyObject focusedElement = KeyboardFocusHelper.FocusedElement;
                if (focusedElement != null)
                {
                    DependencyObject obj3 = LayoutHelper.FindRoot(focusedElement, false);
                    return ((obj3 != null) && (!DockLayoutManagerHelper.IsPopupRoot(obj3) && !DragManager.GetIsDragging(obj3)));
                }
            }
            return true;
        }

        private void ClosePaneCore(BaseLayoutItem item = null)
        {
            AutoHideGroup group = this.GetAutoHideGroup() ?? item.Return<BaseLayoutItem, AutoHideGroup>((<>c.<>9__117_0 ??= x => (x.Parent as AutoHideGroup)), (<>c.<>9__117_1 ??= ((Func<AutoHideGroup>) (() => null))));
            if (group != null)
            {
                group.IsAnimated = false;
                group.IsExpanded = false;
                if (!this.AutoHideTray.IsExpandingLocked)
                {
                    group.ClearValue(LayoutGroup.SelectedTabIndexProperty);
                }
            }
        }

        protected virtual object CoercePanelSize(double size)
        {
            if (this.isClosing > 0)
            {
                size = 0.0;
            }
            if (this.IsSizing)
            {
                size = this.Size;
            }
            return size;
        }

        private void CompletePreviousAnimation(bool isCollapsed)
        {
            bool flag = this.CollapseAnimation != null;
            bool flag2 = this.ExpandAnimation != null;
            this.CancelAnimation();
            AutoHideGroup animatedGroup = this.animatedGroup;
            if (animatedGroup != null)
            {
                if (flag2)
                {
                    animatedGroup.SelectedTabIndex = animatedGroup.Items.IndexOf(base.LayoutItem);
                    animatedGroup.IsAnimated = false;
                    animatedGroup.IsExpanded = true;
                }
                if (flag)
                {
                    animatedGroup.IsAnimated = false;
                    animatedGroup.IsExpanded = false;
                    if (isCollapsed)
                    {
                        animatedGroup.ClearValue(LayoutGroup.SelectedTabIndexProperty);
                    }
                }
            }
        }

        private void EnsureBorderSize()
        {
            if (this.PartPaneContentPresenter == null)
            {
                this.EnsurePaneContentPresenter();
            }
            if ((this.PartPaneContentPresenter != null) && (this.PartPaneContent != null))
            {
                if (base.LayoutItem == null)
                {
                    this.borderSize = this.PartPaneContent.DesiredSize;
                }
                else
                {
                    this.borderSize = new System.Windows.Size(this.PartPaneContent.DesiredSize.Width - this.PartPaneContentPresenter.DesiredSize.Width, this.PartPaneContent.DesiredSize.Width - this.PartPaneContentPresenter.DesiredSize.Width);
                }
            }
        }

        private void EnsurePaneContentPresenter()
        {
            FrameworkElement partPaneContent = this.PartPaneContent;
            FrameworkElement element = partPaneContent;
            if (partPaneContent == null)
            {
                FrameworkElement local1 = partPaneContent;
                element = this;
            }
            this.PartPaneContentPresenter = LayoutItemsHelper.GetTemplateChild<ContentPresenter>(element);
            if (this.PartPaneContentPresenter != null)
            {
                this.BindPanelSizeToContentSize();
            }
        }

        private void ExpandWithAnimation(double to, double from = 0.0)
        {
            AutoHideGroup autoHideGroup = this.GetAutoHideGroup();
            autoHideGroup.IsAnimated = true;
            if ((!this.Context.IsAnimationEnabled || (autoHideGroup.AutoHideSpeed <= 0)) || (to == from))
            {
                this.ApplyPanelSize(to);
            }
            else
            {
                DoubleAnimation animation1 = new DoubleAnimation();
                animation1.From = new double?(from);
                animation1.To = new double?(to);
                animation1.Duration = new Duration(TimeSpan.FromMilliseconds((double) autoHideGroup.AutoHideSpeed));
                this.ExpandAnimation = animation1;
                this.ExpandAnimation.Completed += new EventHandler(this.OnExpandAnimationCompleted);
                this.AutoHideTray.IsAnimated = true;
                base.BeginAnimation(PanelSizeProperty, this.ExpandAnimation);
            }
        }

        private void ExpandWithAnimation(AutoHideGroup aGroup, PanelSizeAction action, bool isAnimationAllowed = true)
        {
            base.Visibility = Visibility.Visible;
            this.Context.IsAnimationEnabled = isAnimationAllowed;
            bool isHorz = this.IsHorz;
            if ((this.ExpandAnimation != null) || (this.CollapseAnimation != null))
            {
                this.Size = this.GetActualSize(isHorz);
            }
            this.CompletePreviousAnimation(false);
            this.animatedGroup = aGroup;
            if (this.GetSizeToContent(isHorz))
            {
                base.InvalidateMeasure();
                base.LayoutUpdated += new EventHandler(this.OnLayoutUpdatedOnAutoSize);
            }
            else
            {
                double size = this.Size;
                this.Size = this.CalcPanelSize(action, aGroup, base.LayoutItem, double.NaN);
                this.ExpandWithAnimation(this.Size, size);
            }
            Func<DevExpress.Xpf.Docking.LayoutPanel, Locker> evaluator = <>c.<>9__121_0;
            if (<>c.<>9__121_0 == null)
            {
                Func<DevExpress.Xpf.Docking.LayoutPanel, Locker> local1 = <>c.<>9__121_0;
                evaluator = <>c.<>9__121_0 = x => x.ExpandAnimationLocker;
            }
            Action<Locker> action1 = <>c.<>9__121_1;
            if (<>c.<>9__121_1 == null)
            {
                Action<Locker> local2 = <>c.<>9__121_1;
                action1 = <>c.<>9__121_1 = x => x.Lock();
            }
            this.LayoutPanel.With<DevExpress.Xpf.Docking.LayoutPanel, Locker>(evaluator).Do<Locker>(action1);
            Func<DevExpress.Xpf.Docking.LayoutPanel, bool> func2 = <>c.<>9__121_2;
            if (<>c.<>9__121_2 == null)
            {
                Func<DevExpress.Xpf.Docking.LayoutPanel, bool> local3 = <>c.<>9__121_2;
                func2 = <>c.<>9__121_2 = x => x.AutoHideExpandState == AutoHideExpandState.Hidden;
            }
            this.LayoutPanel.If<DevExpress.Xpf.Docking.LayoutPanel>(func2).Do<DevExpress.Xpf.Docking.LayoutPanel>(x => x.SetCurrentValue(DevExpress.Xpf.Docking.LayoutPanel.AutoHideExpandStateProperty, (action == PanelSizeAction.Maximize) ? AutoHideExpandState.Expanded : AutoHideExpandState.Visible));
            Func<DevExpress.Xpf.Docking.LayoutPanel, Locker> func3 = <>c.<>9__121_4;
            if (<>c.<>9__121_4 == null)
            {
                Func<DevExpress.Xpf.Docking.LayoutPanel, Locker> local4 = <>c.<>9__121_4;
                func3 = <>c.<>9__121_4 = x => x.ExpandAnimationLocker;
            }
            Action<Locker> action2 = <>c.<>9__121_5;
            if (<>c.<>9__121_5 == null)
            {
                Action<Locker> local5 = <>c.<>9__121_5;
                action2 = <>c.<>9__121_5 = x => x.Unlock();
            }
            this.LayoutPanel.With<DevExpress.Xpf.Docking.LayoutPanel, Locker>(func3).Do<Locker>(action2);
            this.IsCollapsed = false;
        }

        private double GetActualSize(bool isHorz) => 
            (this.PartPaneContentPresenter != null) ? (isHorz ? this.PartPaneContentPresenter.ActualWidth : this.PartPaneContentPresenter.ActualHeight) : (isHorz ? base.ActualWidth : base.ActualHeight);

        private AutoHideGroup GetAutoHideGroup()
        {
            Func<BaseLayoutItem, LayoutGroup> evaluator = <>c.<>9__123_0;
            if (<>c.<>9__123_0 == null)
            {
                Func<BaseLayoutItem, LayoutGroup> local1 = <>c.<>9__123_0;
                evaluator = <>c.<>9__123_0 = x => x.Parent;
            }
            return (DockLayoutManager.GetLayoutItem(this).With<BaseLayoutItem, LayoutGroup>(evaluator) as AutoHideGroup);
        }

        private double GetAvailableAutoHideSize(bool isHorz)
        {
            double local4;
            DockLayoutManager container = base.Container ?? DockLayoutManager.GetDockLayoutManager(this);
            if (container == null)
            {
                return 0.0;
            }
            double availableAutoHideSize = container.GetAvailableAutoHideSize(isHorz);
            if (double.IsNaN(availableAutoHideSize))
            {
                return availableAutoHideSize;
            }
            if (this.DisplayMode != AutoHideMode.Inline)
            {
                local4 = 0.0;
            }
            else
            {
                Func<ElementSizer, double> evaluator = <>c.<>9__124_0;
                if (<>c.<>9__124_0 == null)
                {
                    Func<ElementSizer, double> local2 = <>c.<>9__124_0;
                    evaluator = <>c.<>9__124_0 = x => x.Thickness;
                }
                local4 = this.PartSizer.Return<ElementSizer, double>(evaluator, <>c.<>9__124_1 ??= () => 0.0);
            }
            double num2 = local4;
            double num3 = isHorz ? this.borderSize.Width : this.borderSize.Height;
            return Math.Max((double) 0.0, (double) ((availableAutoHideSize - num2) - num3));
        }

        private bool GetSizeToContent(bool fhorz)
        {
            if (base.LayoutItem == null)
            {
                return false;
            }
            System.Windows.SizeToContent sizeToContent = AutoHideGroup.GetSizeToContent(base.LayoutItem);
            if (!((sizeToContent == System.Windows.SizeToContent.WidthAndHeight) || (fhorz ? (sizeToContent == System.Windows.SizeToContent.Width) : (sizeToContent == System.Windows.SizeToContent.Height))))
            {
                return false;
            }
            Func<DevExpress.Xpf.Docking.LayoutPanel, bool> evaluator = <>c.<>9__125_0;
            if (<>c.<>9__125_0 == null)
            {
                Func<DevExpress.Xpf.Docking.LayoutPanel, bool> local1 = <>c.<>9__125_0;
                evaluator = <>c.<>9__125_0 = x => x.AutoHideExpandState != AutoHideExpandState.Expanded;
            }
            return (base.LayoutItem as DevExpress.Xpf.Docking.LayoutPanel).Return<DevExpress.Xpf.Docking.LayoutPanel, bool>(evaluator, (<>c.<>9__125_1 ??= () => false));
        }

        private void LockOnContainerSizeChanged()
        {
            this.containerSizeLocker.LockOnce();
            base.Dispatcher.BeginInvoke(delegate {
                if (this.ExpandAnimation == null)
                {
                    this.containerSizeLocker.Unlock();
                }
                else
                {
                    this.LockOnContainerSizeChanged();
                }
            }, DispatcherPriority.Input, new object[0]);
        }

        protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        {
            System.Windows.Size size = constraint;
            bool fhorz = this.Orientation == System.Windows.Controls.Orientation.Vertical;
            bool sizeToContent = this.GetSizeToContent(fhorz);
            if (sizeToContent)
            {
                this.PanelSize = double.NaN;
                size = fhorz ? new System.Windows.Size(double.PositiveInfinity, size.Height) : new System.Windows.Size(size.Width, double.PositiveInfinity);
            }
            if (this.firstMeasure && (base.LayoutItem == null))
            {
                this.IsSizerVisible = true;
            }
            System.Windows.Size size2 = base.MeasureOverride(size);
            if ((sizeToContent & fhorz) && ((this.LayoutPanel != null) && !DockLayoutManagerParameters.AutoHidePanelsAutoSizeDependsOnCaption))
            {
                DockPaneContentPresenter presenter = LayoutTreeHelper.GetVisualChildren(this.LayoutPanel).OfType<DockPaneContentPresenter>().FirstOrDefault<DockPaneContentPresenter>();
                DockPaneHeaderPresenter presenter2 = LayoutTreeHelper.GetVisualChildren(this.LayoutPanel).OfType<DockPaneHeaderPresenter>().FirstOrDefault<DockPaneHeaderPresenter>();
                if ((presenter != null) && (presenter2 != null))
                {
                    double num = Math.Max((double) 0.0, (double) (presenter2.DesiredSize.Width - presenter.DesiredSize.Width));
                    if (num > 0.0)
                    {
                        size2 = base.MeasureOverride(new System.Windows.Size(size2.Width - num, size.Height));
                    }
                }
            }
            if (this.firstMeasure)
            {
                if (base.LayoutItem == null)
                {
                    base.Visibility = Visibility.Collapsed;
                }
                this.EnsureBorderSize();
                this.UpdateIsSizerVisible();
                this.firstMeasure = false;
            }
            return new System.Windows.Size(Math.Min(size2.Width, constraint.Width), Math.Min(size2.Height, constraint.Height));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartAutoHidePresenter = base.GetTemplateChild("PART_Presenter") as AutoHidePanePresenter;
            this.PartPaneContent = base.GetTemplateChild("PART_PaneContent") as FrameworkElement;
            this.PartPaneBorder = base.GetTemplateChild("PART_PaneBorder") as Border;
            this.PartSizer = base.GetTemplateChild("PART_Sizer") as ElementSizer;
            if (this.PartPaneBorder != null)
            {
                this.UpdatePaneBorder();
            }
            if (base.Container != null)
            {
                base.Container.SizeChanged += new SizeChangedEventHandler(this.OnContainerSizeChanged);
                base.Container.AutoHideLayer.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.SizeChanged += new SizeChangedEventHandler(this.OnContainerSizeChanged);
                });
            }
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        private void OnAutoHideSizeChanged(System.Windows.Size oldValue, System.Windows.Size newValue)
        {
            if (!((this.IsHorz ? (this.SizeToContent == System.Windows.SizeToContent.Width) : (this.SizeToContent == System.Windows.SizeToContent.Height)) || (this.SizeToContent == System.Windows.SizeToContent.WidthAndHeight)) && !this.IsSizing)
            {
                this.RecalculatePanelSize();
            }
        }

        protected virtual void OnAutoHideTrayChanged(DevExpress.Xpf.Docking.VisualElements.AutoHideTray oldTray, DevExpress.Xpf.Docking.VisualElements.AutoHideTray newTray)
        {
            this.UnSubscribe(oldTray);
            this.Subscribe(newTray);
        }

        private void OnCollapseAnimationCompleted(object sender, EventArgs e)
        {
            this.CollapseAnimation.Completed -= new EventHandler(this.OnCollapseAnimationCompleted);
            this.CollapseAnimation = null;
            this.OnCollapseCompleted();
        }

        private void OnCollapseCompleted()
        {
            this.AutoHideTray.IsAnimated = false;
            this.ClosePaneCore(null);
            this.animatedGroup = null;
            this.IsCollapsed = true;
            this.Size = 0.0;
            base.Visibility = Visibility.Collapsed;
        }

        protected virtual void OnCollapsed(object sender, RoutedEventArgs e)
        {
            AutoHideGroup autoHideGroup = this.GetAutoHideGroup();
            if (autoHideGroup != null)
            {
                this.animatedGroup = autoHideGroup;
                double panelSize = this.PanelSize;
                this.CompletePreviousAnimation(true);
                autoHideGroup.IsAnimated = true;
                bool isHorz = this.Orientation == System.Windows.Controls.Orientation.Vertical;
                double actualSize = this.GetActualSize(isHorz);
                double num3 = !double.IsNaN(panelSize) ? Math.Min(panelSize, actualSize) : actualSize;
                if (autoHideGroup.AutoHideSpeed <= 0)
                {
                    this.OnCollapseCompleted();
                }
                else
                {
                    DoubleAnimation animation1 = new DoubleAnimation();
                    animation1.From = new double?(num3);
                    animation1.To = 0.0;
                    animation1.Duration = new Duration(TimeSpan.FromMilliseconds((double) autoHideGroup.AutoHideSpeed));
                    this.CollapseAnimation = animation1;
                    this.CollapseAnimation.Completed += new EventHandler(this.OnCollapseAnimationCompleted);
                    this.AutoHideTray.IsAnimated = true;
                    base.BeginAnimation(PanelSizeProperty, this.CollapseAnimation);
                }
            }
        }

        private void OnContainerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((base.Container != null) && (this.AutoHideTray != null)) && (DockLayoutManagerParameters.AutoHidePanelsFitToContainer && ((this.CollapseAnimation == null) && (!this.IsSizing && !this.containerSizeLocker))))
            {
                if (this.ExpandAnimation != null)
                {
                    this.LockOnContainerSizeChanged();
                }
                else if ((this.IsHorz ? (e.NewSize.Width - e.PreviousSize.Width) : (e.NewSize.Height - e.PreviousSize.Height)) != 0.0)
                {
                    base.Container.InvalidateView(base.Container.LayoutRoot);
                    if (this.AutoHideTray.IsExpanded)
                    {
                        this.LockOnContainerSizeChanged();
                    }
                    this.RecalculatePanelSize();
                }
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new AutoHidePaneAutomationPeer(this);

        private void OnDisplayModeChanged(AutoHideMode oldValue, AutoHideMode newValue)
        {
            this.UpdateIsSizerVisible();
        }

        protected override void OnDispose()
        {
            this.CancelAnimation();
            this.UnSubscribe(this.AutoHideTray);
            if (this.PartPaneContentPresenter != null)
            {
                this.PartPaneContentPresenter.ClearValue(FrameworkElement.WidthProperty);
                this.PartPaneContentPresenter.ClearValue(FrameworkElement.HeightProperty);
            }
            if (this.PartAutoHidePresenter != null)
            {
                this.PartAutoHidePresenter.Dispose();
                this.PartAutoHidePresenter = null;
            }
            if (base.Container != null)
            {
                base.Container.SizeChanged -= new SizeChangedEventHandler(this.OnContainerSizeChanged);
                base.Container.AutoHideLayer.Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.SizeChanged -= new SizeChangedEventHandler(this.OnContainerSizeChanged);
                });
            }
            base.OnDispose();
        }

        private void OnExpandAnimationCompleted(object sender, EventArgs e)
        {
            double naN = double.NaN;
            if (this.ExpandAnimation != null)
            {
                double? to = this.ExpandAnimation.To;
                naN = (to != null) ? to.GetValueOrDefault() : double.NaN;
                this.ExpandAnimation.Completed -= new EventHandler(this.OnExpandAnimationCompleted);
                this.ExpandAnimation = null;
            }
            this.ApplyPanelSize(naN);
        }

        protected virtual void OnExpanded(object sender, RoutedEventArgs e)
        {
            AutoHideGroup autoHideGroup = this.GetAutoHideGroup();
            if (autoHideGroup != null)
            {
                this.ExpandWithAnimation(autoHideGroup, PanelSizeAction.Expand, true);
            }
        }

        protected virtual void OnHotItemChanged(object sender, HotItemChangedEventArgs e)
        {
            base.SetValue(DockLayoutManager.LayoutItemProperty, e.Hot);
            if (e.Hot != null)
            {
                e.Hot.SelectTemplateIfNeeded();
            }
            base.SetValue(ContentControl.ContentProperty, e.Hot);
            if (this.ExpandAnimation != null)
            {
                this.RecalculatePanelSize();
            }
        }

        private void OnIsCollapsedChanged(bool oldValue, bool newValue)
        {
            this.UpdateIsSizerVisible();
        }

        protected override void OnLayoutItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            base.OnLayoutItemChanged(item, oldItem);
            if (oldItem != null)
            {
                oldItem.ClearTemplate();
                base.ClearValue(AutoHideSizeProperty);
                base.ClearValue(SizeToContentProperty);
            }
            if (item == null)
            {
                this.ClosePaneCore(oldItem);
                this.IsCollapsed = true;
                base.Visibility = Visibility.Collapsed;
                base.ClearValue(ViewStyleProperty);
            }
            else
            {
                this.BindBorderCursor();
                item.SelectTemplateIfNeeded();
                DockLayoutManager.SetUIScope(item, this);
                Binding binding = new Binding();
                binding.Path = new PropertyPath(AutoHideGroup.AutoHideSizeProperty);
                binding.Source = item;
                base.SetBinding(AutoHideSizeProperty, binding);
                Binding binding2 = new Binding();
                binding2.Path = new PropertyPath(AutoHideGroup.SizeToContentProperty);
                binding2.Source = item;
                base.SetBinding(SizeToContentProperty, binding2);
                BindingHelper.SetBinding(this, ViewStyleProperty, base.LayoutItem, BaseLayoutItem.DockingViewStyleProperty, BindingMode.OneWay);
            }
            foreach (EventHandler handler in this.layoutItemChangedHandlers)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            this.EnsurePaneContentPresenter();
        }

        private void OnLayoutUpdatedOnAutoSize(object sender, EventArgs e)
        {
            double local3;
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdatedOnAutoSize);
            if (double.IsNaN(this.Size))
            {
                this.Size = 0.0;
            }
            if (this.DisplayMode != AutoHideMode.Inline)
            {
                local3 = 0.0;
            }
            else
            {
                Func<ElementSizer, double> evaluator = <>c.<>9__135_0;
                if (<>c.<>9__135_0 == null)
                {
                    Func<ElementSizer, double> local1 = <>c.<>9__135_0;
                    evaluator = <>c.<>9__135_0 = x => x.Thickness;
                }
                local3 = this.PartSizer.Return<ElementSizer, double>(evaluator, <>c.<>9__135_1 ??= () => 0.0);
            }
            double num = local3;
            this.ExpandWithAnimation(this.IsHorz ? ((this.arrangeSize.Width - this.borderSize.Width) - num) : ((this.arrangeSize.Height - this.borderSize.Height) - num), this.Size);
        }

        protected virtual void OnPanelClosed(object sender, RoutedEventArgs e)
        {
            this.isClosing++;
            base.CoerceValue(PanelSizeProperty);
            base.ClearValue(ContentControl.ContentProperty);
            this.CancelAnimation();
            this.Size = 0.0;
            this.ClosePaneCore(null);
            this.isClosing--;
        }

        private void OnPanelMaximized(object sender, RoutedEventArgs e)
        {
            AutoHideGroup autoHideGroup = this.GetAutoHideGroup();
            if ((base.LayoutItem != null) && (autoHideGroup != null))
            {
                this.ExpandWithAnimation(autoHideGroup, PanelSizeAction.Maximize, true);
            }
        }

        protected virtual void OnPanelResizing(object sender, PanelResizingEventArgs e)
        {
            AutoHideGroup autoHideGroup = this.GetAutoHideGroup();
            if ((base.LayoutItem != null) && (autoHideGroup != null))
            {
                this.isSizing++;
                base.LayoutItem.ResizeLockHelper.Lock();
                bool isHorz = this.IsHorz;
                double num = (isHorz ? base.ActualWidth : base.ActualHeight) - e.Size;
                double desiredSize = (isHorz ? this.PartPaneContentPresenter.ActualWidth : this.PartPaneContentPresenter.ActualHeight) - num;
                this.Size = this.CalcPanelSize(PanelSizeAction.Resize, autoHideGroup, base.LayoutItem, desiredSize);
                System.Windows.Size autoHideSize = AutoHideGroup.GetAutoHideSize(base.LayoutItem);
                AutoHideGroup.SetAutoHideSize(base.LayoutItem, isHorz ? new System.Windows.Size(this.Size, autoHideSize.Height) : new System.Windows.Size(autoHideSize.Width, this.Size));
                AutoHideGroup.SetSizeToContent(base.LayoutItem, System.Windows.SizeToContent.Manual);
                DevExpress.Xpf.Docking.LayoutPanel layoutItem = base.LayoutItem as DevExpress.Xpf.Docking.LayoutPanel;
                if (<>c.<>9__111_0 == null)
                {
                    DevExpress.Xpf.Docking.LayoutPanel local1 = base.LayoutItem as DevExpress.Xpf.Docking.LayoutPanel;
                    layoutItem = (DevExpress.Xpf.Docking.LayoutPanel) (<>c.<>9__111_0 = x => x.AutoHideExpandState == AutoHideExpandState.Expanded);
                }
                Action<DevExpress.Xpf.Docking.LayoutPanel> action = <>c.<>9__111_1;
                if (<>c.<>9__111_1 == null)
                {
                    Action<DevExpress.Xpf.Docking.LayoutPanel> local2 = <>c.<>9__111_1;
                    action = <>c.<>9__111_1 = x => x.SetCurrentValue(DevExpress.Xpf.Docking.LayoutPanel.AutoHideExpandStateProperty, AutoHideExpandState.Visible);
                }
                ((DevExpress.Xpf.Docking.LayoutPanel) <>c.<>9__111_0).If<DevExpress.Xpf.Docking.LayoutPanel>(((Func<DevExpress.Xpf.Docking.LayoutPanel, bool>) layoutItem)).Do<DevExpress.Xpf.Docking.LayoutPanel>(action);
                base.CoerceValue(PanelSizeProperty);
                this.isSizing--;
            }
        }

        private void OnPanelRestored(object sender, RoutedEventArgs e)
        {
            AutoHideGroup autoHideGroup = this.GetAutoHideGroup();
            if ((base.LayoutItem != null) && ((autoHideGroup != null) && !this.IsSizing))
            {
                this.ExpandWithAnimation(autoHideGroup, PanelSizeAction.Expand, true);
            }
        }

        private void OnSizeToContentChanged(System.Windows.SizeToContent oldValue, System.Windows.SizeToContent newValue)
        {
            bool isHorz = this.IsHorz;
            bool flag2 = (isHorz ? (oldValue == System.Windows.SizeToContent.Width) : (oldValue == System.Windows.SizeToContent.Height)) || (oldValue == System.Windows.SizeToContent.WidthAndHeight);
            if (flag2 != ((isHorz ? (newValue == System.Windows.SizeToContent.Width) : (newValue == System.Windows.SizeToContent.Height)) || (newValue == System.Windows.SizeToContent.WidthAndHeight)))
            {
                base.InvalidateMeasure();
                this.RecalculatePanelSize();
            }
        }

        private void RecalculatePanelSize()
        {
            AutoHideGroup autoHideGroup = this.GetAutoHideGroup();
            if ((autoHideGroup != null) && (autoHideGroup.IsExpanded && this.AutoHideTray.IsExpanded))
            {
                this.ExpandWithAnimation(autoHideGroup, PanelSizeAction.Expand, false);
            }
        }

        private void Subscribe(DevExpress.Xpf.Docking.VisualElements.AutoHideTray tray)
        {
            if (tray != null)
            {
                ((IUIElement) tray).Children.Add(this);
                tray.Collapsed += new RoutedEventHandler(this.OnCollapsed);
                tray.Expanded += new RoutedEventHandler(this.OnExpanded);
                tray.PanelClosed += new RoutedEventHandler(this.OnPanelClosed);
                tray.HotItemChanged += new HotItemChangedEventHandler(this.OnHotItemChanged);
                tray.PanelResizing += new PanelResizingEventHandler(this.OnPanelResizing);
                tray.PanelMaximized += new RoutedEventHandler(this.OnPanelMaximized);
                tray.PanelRestored += new RoutedEventHandler(this.OnPanelRestored);
            }
        }

        private void UnSubscribe(DevExpress.Xpf.Docking.VisualElements.AutoHideTray tray)
        {
            if (tray != null)
            {
                ((IUIElement) tray).Children.Remove(this);
                tray.Collapsed -= new RoutedEventHandler(this.OnCollapsed);
                tray.Expanded -= new RoutedEventHandler(this.OnExpanded);
                tray.PanelClosed -= new RoutedEventHandler(this.OnPanelClosed);
                tray.HotItemChanged -= new HotItemChangedEventHandler(this.OnHotItemChanged);
                tray.PanelResizing -= new PanelResizingEventHandler(this.OnPanelResizing);
                tray.PanelMaximized -= new RoutedEventHandler(this.OnPanelMaximized);
                tray.PanelRestored -= new RoutedEventHandler(this.OnPanelRestored);
            }
        }

        private void UpdateIsSizerVisible()
        {
            this.IsSizerVisible = ((this.DisplayMode == AutoHideMode.Inline) && !this.IsCollapsed) && (base.LayoutItem != null);
        }

        private void UpdatePaneBorder()
        {
            if (base.LayoutItem != null)
            {
                this.BindBorderCursor();
            }
            this.PartPaneBorder.HorizontalAlignment = DevExpress.Xpf.Docking.DockExtensions.ToHorizontalAlignment(this.DockType, true);
            this.PartPaneBorder.VerticalAlignment = DevExpress.Xpf.Docking.DockExtensions.ToVerticalAlignment(this.DockType, true);
        }

        public DockingViewStyle ViewStyle
        {
            get => 
                (DockingViewStyle) base.GetValue(ViewStyleProperty);
            set => 
                base.SetValue(ViewStyleProperty, value);
        }

        public DevExpress.Xpf.Docking.VisualElements.AutoHideTray AutoHideTray
        {
            get => 
                (DevExpress.Xpf.Docking.VisualElements.AutoHideTray) base.GetValue(AutoHideTrayProperty);
            set => 
                base.SetValue(AutoHideTrayProperty, value);
        }

        public bool CanCollapse =>
            this.CanCollapseCore();

        public bool CanHideCurrentItem =>
            this.CanHideCurrentItemCore();

        public AutoHideMode DisplayMode
        {
            get => 
                (AutoHideMode) base.GetValue(DisplayModeProperty);
            set => 
                base.SetValue(DisplayModeProperty, value);
        }

        public Dock DockType
        {
            get => 
                (Dock) base.GetValue(DockTypeProperty);
            internal set => 
                base.SetValue(DockTypePropertyKey, value);
        }

        public bool IsCollapsed
        {
            get => 
                (bool) base.GetValue(IsCollapsedProperty);
            internal set => 
                base.SetValue(IsCollapsedPropertyKey, value);
        }

        public bool IsSizerVisible
        {
            get => 
                (bool) base.GetValue(IsSizerVisibleProperty);
            private set => 
                base.SetValue(IsSizerVisiblePropertyKey, value);
        }

        public double PanelSize
        {
            get => 
                (double) base.GetValue(PanelSizeProperty);
            set => 
                base.SetValue(PanelSizeProperty, value);
        }

        public AutoHidePanePresenter PartAutoHidePresenter { get; private set; }

        public Border PartPaneBorder { get; private set; }

        public FrameworkElement PartPaneContent { get; private set; }

        public FrameworkElement PartPaneContentPresenter { get; private set; }

        internal ElementSizer PartSizer { get; private set; }

        protected internal double Size { get; private set; }

        protected System.Windows.Controls.Orientation Orientation =>
            DevExpress.Xpf.Docking.VisualElements.AutoHideTray.GetOrientation(this);

        private DoubleAnimation CollapseAnimation { get; set; }

        private AnimationContext Context
        {
            get
            {
                AnimationContext context2 = this._Context;
                if (this._Context == null)
                {
                    AnimationContext local1 = this._Context;
                    context2 = this._Context = new AnimationContext();
                }
                return context2;
            }
        }

        private DoubleAnimation ExpandAnimation { get; set; }

        private bool IsHorz =>
            this.Orientation == System.Windows.Controls.Orientation.Vertical;

        private bool IsSizing =>
            this.isSizing > 0;

        private DevExpress.Xpf.Docking.LayoutPanel LayoutPanel =>
            base.LayoutItem as DevExpress.Xpf.Docking.LayoutPanel;

        private System.Windows.SizeToContent SizeToContent
        {
            get => 
                (System.Windows.SizeToContent) base.GetValue(SizeToContentProperty);
            set => 
                base.SetValue(SizeToContentProperty, value);
        }

        IUIElement IUIElement.Scope =>
            this.AutoHideTray;

        UIChildren IUIElement.Children
        {
            get
            {
                UIChildren uiChildren = this.uiChildren;
                if (this.uiChildren == null)
                {
                    UIChildren local1 = this.uiChildren;
                    uiChildren = this.uiChildren = new UIChildren();
                }
                return uiChildren;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoHidePane.<>c <>9 = new AutoHidePane.<>c();
            public static Func<LayoutPanel, bool> <>9__111_0;
            public static Action<LayoutPanel> <>9__111_1;
            public static Func<LayoutPanel, bool> <>9__115_0;
            public static Func<bool> <>9__115_1;
            public static Func<BaseLayoutItem, AutoHideGroup> <>9__117_0;
            public static Func<AutoHideGroup> <>9__117_1;
            public static Func<LayoutPanel, Locker> <>9__121_0;
            public static Action<Locker> <>9__121_1;
            public static Func<LayoutPanel, bool> <>9__121_2;
            public static Func<LayoutPanel, Locker> <>9__121_4;
            public static Action<Locker> <>9__121_5;
            public static Func<BaseLayoutItem, LayoutGroup> <>9__123_0;
            public static Func<ElementSizer, double> <>9__124_0;
            public static Func<double> <>9__124_1;
            public static Func<LayoutPanel, bool> <>9__125_0;
            public static Func<bool> <>9__125_1;
            public static Func<ElementSizer, double> <>9__135_0;
            public static Func<double> <>9__135_1;

            internal object <.cctor>b__12_0(DependencyObject dObj, object value) => 
                ((AutoHidePane) dObj).CoercePanelSize((double) value);

            internal void <.cctor>b__12_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHidePane) dObj).OnAutoHideTrayChanged(e.OldValue as AutoHideTray, e.NewValue as AutoHideTray);
            }

            internal void <.cctor>b__12_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHidePane) dObj).OnIsCollapsedChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__12_3(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHidePane) dObj).OnDisplayModeChanged((AutoHideMode) e.OldValue, (AutoHideMode) e.NewValue);
            }

            internal void <.cctor>b__12_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHidePane) d).OnAutoHideSizeChanged((Size) e.OldValue, (Size) e.NewValue);
            }

            internal void <.cctor>b__12_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHidePane) d).OnSizeToContentChanged((SizeToContent) e.OldValue, (SizeToContent) e.NewValue);
            }

            internal bool <CalcPanelSize>b__115_0(LayoutPanel x) => 
                x.AutoHideExpandState == AutoHideExpandState.Expanded;

            internal bool <CalcPanelSize>b__115_1() => 
                false;

            internal AutoHideGroup <ClosePaneCore>b__117_0(BaseLayoutItem x) => 
                x.Parent as AutoHideGroup;

            internal AutoHideGroup <ClosePaneCore>b__117_1() => 
                null;

            internal Locker <ExpandWithAnimation>b__121_0(LayoutPanel x) => 
                x.ExpandAnimationLocker;

            internal void <ExpandWithAnimation>b__121_1(Locker x)
            {
                x.Lock();
            }

            internal bool <ExpandWithAnimation>b__121_2(LayoutPanel x) => 
                x.AutoHideExpandState == AutoHideExpandState.Hidden;

            internal Locker <ExpandWithAnimation>b__121_4(LayoutPanel x) => 
                x.ExpandAnimationLocker;

            internal void <ExpandWithAnimation>b__121_5(Locker x)
            {
                x.Unlock();
            }

            internal LayoutGroup <GetAutoHideGroup>b__123_0(BaseLayoutItem x) => 
                x.Parent;

            internal double <GetAvailableAutoHideSize>b__124_0(ElementSizer x) => 
                x.Thickness;

            internal double <GetAvailableAutoHideSize>b__124_1() => 
                0.0;

            internal bool <GetSizeToContent>b__125_0(LayoutPanel x) => 
                x.AutoHideExpandState != AutoHideExpandState.Expanded;

            internal bool <GetSizeToContent>b__125_1() => 
                false;

            internal double <OnLayoutUpdatedOnAutoSize>b__135_0(ElementSizer x) => 
                x.Thickness;

            internal double <OnLayoutUpdatedOnAutoSize>b__135_1() => 
                0.0;

            internal bool <OnPanelResizing>b__111_0(LayoutPanel x) => 
                x.AutoHideExpandState == AutoHideExpandState.Expanded;

            internal void <OnPanelResizing>b__111_1(LayoutPanel x)
            {
                x.SetCurrentValue(LayoutPanel.AutoHideExpandStateProperty, AutoHideExpandState.Visible);
            }
        }

        private class AnimationContext
        {
            public bool IsAnimationEnabled { get; set; }
        }

        private enum PanelSizeAction
        {
            Expand,
            Maximize,
            Resize
        }
    }
}

