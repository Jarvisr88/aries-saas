namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    [TemplatePart(Name="PART_Header", Type=typeof(DockPaneHeaderPresenter)), TemplatePart(Name="PART_Content", Type=typeof(DockPaneContentPresenter))]
    public class DockPane : psvHeaderedContentControl, IMDIChildHost
    {
        public static readonly DependencyProperty HitTestTypeProperty;
        public static readonly DependencyProperty ControlHostTemplateProperty;
        public static readonly DependencyProperty LayoutHostTemplateProperty;
        public static readonly DependencyProperty DataHostTemplateProperty;
        public static readonly DependencyProperty CaptionActiveBackgroundProperty;
        public static readonly DependencyProperty CaptionNormalBackgroundProperty;
        public static readonly DependencyProperty CaptionActiveForegroundProperty;
        public static readonly DependencyProperty CaptionNormalForegroundProperty;
        public static readonly DependencyProperty ActualCaptionBackgroundProperty;
        public static readonly DependencyProperty ActualCaptionForegroundProperty;
        public static readonly DependencyProperty CaptionCornerRadiusProperty;
        public static readonly DependencyProperty FloatingCaptionCornerRadiusProperty;
        public static readonly DependencyProperty ActualCaptionCornerRadiusProperty;
        public static readonly DependencyProperty BorderMarginProperty;
        public static readonly DependencyProperty BorderPaddingProperty;
        public static readonly DependencyProperty BarContainerMarginProperty;
        public static readonly DependencyProperty ContentMarginProperty;
        public static readonly DependencyProperty ActualBorderThicknessProperty;
        public static readonly DependencyProperty ActualBorderMarginProperty;
        public static readonly DependencyProperty ActualBorderPaddingProperty;
        public static readonly DependencyProperty FloatingActiveBorderBrushProperty;
        public static readonly DependencyProperty ActualBorderBrushProperty;
        private static readonly DependencyPropertyKey ActualBorderBrushPropertyKey;
        public static readonly DependencyProperty ViewStyleProperty;
        private bool _IsChildMenuVisibleCore = true;
        protected WeakList<EventHandler> isChildMenuVisibleChangedHandlers = new WeakList<EventHandler>();

        event EventHandler IMDIChildHost.IsChildMenuVisibleChanged
        {
            add
            {
                this.isChildMenuVisibleChangedHandlers.Add(value);
            }
            remove
            {
                this.isChildMenuVisibleChangedHandlers.Remove(value);
            }
        }

        static DockPane()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<DockPane> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<DockPane>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DataTemplate>("ControlHostTemplate", ref ControlHostTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("LayoutHostTemplate", ref LayoutHostTemplateProperty, null, null, null);
            registrator.Register<DataTemplate>("DataHostTemplate", ref DataHostTemplateProperty, null, null, null);
            registrator.Register<Brush>("CaptionActiveBackground", ref CaptionActiveBackgroundProperty, null, null, null);
            registrator.Register<Brush>("CaptionNormalBackground", ref CaptionNormalBackgroundProperty, null, null, null);
            registrator.Register<Brush>("CaptionActiveForeground", ref CaptionActiveForegroundProperty, null, null, null);
            registrator.Register<Brush>("CaptionNormalForeground", ref CaptionNormalForegroundProperty, null, null, null);
            registrator.Register<Brush>("ActualCaptionBackground", ref ActualCaptionBackgroundProperty, null, null, null);
            registrator.Register<Brush>("ActualCaptionForeground", ref ActualCaptionForegroundProperty, null, null, null);
            registrator.Register<Brush>("FloatingActiveBorderBrush", ref FloatingActiveBorderBrushProperty, null, (d, e) => ((DockPane) d).OnBorderBrushChanged(), null);
            registrator.RegisterReadonly<Brush>("ActualBorderBrush", ref ActualBorderBrushPropertyKey, ref ActualBorderBrushProperty, null, null, null);
            registrator.Register<CornerRadius>("CaptionCornerRadius", ref CaptionCornerRadiusProperty, new CornerRadius(0.0), null, null);
            registrator.Register<CornerRadius>("FloatingCaptionCornerRadius", ref FloatingCaptionCornerRadiusProperty, new CornerRadius(0.0), null, null);
            registrator.Register<CornerRadius>("ActualCaptionCornerRadius", ref ActualCaptionCornerRadiusProperty, new CornerRadius(0.0), null, null);
            registrator.Register<Thickness>("BorderMargin", ref BorderMarginProperty, new Thickness(0.0), null, null);
            registrator.Register<Thickness>("BorderPadding", ref BorderPaddingProperty, new Thickness(0.0), null, null);
            registrator.Register<Thickness>("ActualBorderThickness", ref ActualBorderThicknessProperty, new Thickness(0.0), null, null);
            registrator.Register<Thickness>("ActualBorderMargin", ref ActualBorderMarginProperty, new Thickness(0.0), null, null);
            registrator.Register<Thickness>("ActualBorderPadding", ref ActualBorderPaddingProperty, new Thickness(0.0), null, null);
            registrator.Register<Thickness>("BarContainerMargin", ref BarContainerMarginProperty, new Thickness(0.0), null, null);
            registrator.Register<Thickness>("ContentMargin", ref ContentMarginProperty, new Thickness(0.0), null, null);
            registrator.RegisterAttachedInherited<HitTestType>("HitTestType", ref HitTestTypeProperty, HitTestType.Undefined, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DockPane), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DockPane>.New().OverrideMetadata(Control.BorderBrushProperty, d => d.OnBorderBrushChanged()).Register<DockingViewStyle>(System.Linq.Expressions.Expression.Lambda<Func<DockPane, DockingViewStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DockPane.get_ViewStyle)), parameters), out ViewStyleProperty, DockingViewStyle.Default, frameworkOptions);
        }

        public static HitTestType GetHitTestType(DependencyObject obj) => 
            (HitTestType) obj.GetValue(HitTestTypeProperty);

        private bool HasBorderCore()
        {
            DevExpress.Xpf.Docking.LayoutPanel layoutItem = base.LayoutItem as DevExpress.Xpf.Docking.LayoutPanel;
            if (layoutItem == null)
            {
                return false;
            }
            bool flag = (layoutItem.DockingViewStyle == DockingViewStyle.Light) && !LayoutItemsHelper.IsFloatingRootItem(base.LayoutItem);
            return (layoutItem.HasBorder && (!layoutItem.IsDockedAsDocument && !flag));
        }

        protected virtual void NotifyListeners()
        {
            foreach (EventHandler handler in this.isChildMenuVisibleChangedHandlers)
            {
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        protected override void OnActualSizeChanged(Size value)
        {
            base.OnActualSizeChanged(value);
            if (!base.IsDisposing && (base.LayoutItem != null))
            {
                base.LayoutItem.LayoutSize = value;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ((this.PartHeader != null) && !LayoutItemsHelper.IsTemplateChild<DockPane>(this.PartHeader, this))
            {
                this.PartHeader.Dispose();
            }
            this.PartHeader = base.GetTemplateChild("PART_Header") as DockPaneHeaderPresenter;
            if (this.PartHeader != null)
            {
                this.PartHeader.EnsureOwner(this);
                BindingHelper.SetBinding(this.PartHeader, DockPaneHeaderPresenter.IsCaptionVisibleProperty, base.LayoutItem, "IsCaptionVisible");
                BindingHelper.SetBinding(this.PartHeader, DockPaneHeaderPresenter.BackgroundProperty, this, "ActualCaptionBackground");
                BindingHelper.SetBinding(this.PartHeader, DockPaneHeaderPresenter.ForegroundProperty, this, "ActualCaptionForeground");
            }
            if ((this.PartContent != null) && !LayoutItemsHelper.IsTemplateChild<DockPane>(this.PartContent, this))
            {
                this.PartContent.Dispose();
            }
            this.PartContent = base.GetTemplateChild("PART_Content") as DockPaneContentPresenter;
            if (this.PartContent != null)
            {
                this.PartContent.EnsureOwner(this);
                BindingHelper.SetBinding(this.PartContent, DockItemContentPresenter<DockPane, DevExpress.Xpf.Docking.LayoutPanel>.IsControlItemsHostProperty, base.LayoutItem, "IsControlItemsHost");
                BindingHelper.SetBinding(this.PartContent, DockItemContentPresenter<DockPane, DevExpress.Xpf.Docking.LayoutPanel>.IsDataBoundProperty, base.LayoutItem, "IsDataBound");
            }
            DevExpress.Xpf.Docking.LayoutPanel layoutItem = base.LayoutItem as DevExpress.Xpf.Docking.LayoutPanel;
            if (layoutItem != null)
            {
                this.UpdateBrushes(layoutItem);
                this.UpdateGeometry(layoutItem);
            }
        }

        protected virtual void OnBorderBrushChanged()
        {
            DevExpress.Xpf.Docking.LayoutPanel layoutPanel = this.LayoutPanel;
            if (layoutPanel != null)
            {
                this.UpdateActualBorderBrush(layoutPanel);
            }
        }

        protected override void OnDispose()
        {
            if (this.PartContent != null)
            {
                this.PartContent.Dispose();
                this.PartContent = null;
            }
            if (this.PartHeader != null)
            {
                this.PartHeader.Dispose();
                this.PartHeader = null;
            }
            base.ClearValue(ControlHostTemplateProperty);
            base.ClearValue(LayoutHostTemplateProperty);
            base.ClearValue(DataHostTemplateProperty);
            this.isChildMenuVisibleChangedHandlers.Clear();
            base.OnDispose();
        }

        private void OnItemGeometryChanged(object sender, EventArgs e)
        {
            DevExpress.Xpf.Docking.LayoutPanel panel = sender as DevExpress.Xpf.Docking.LayoutPanel;
            if (panel != null)
            {
                this.UpdateGeometry(panel);
            }
        }

        private void OnItemVisualChanged(object sender, EventArgs e)
        {
            DevExpress.Xpf.Docking.LayoutPanel panel = sender as DevExpress.Xpf.Docking.LayoutPanel;
            if (panel != null)
            {
                this.UpdateBrushes(panel);
            }
        }

        protected override void OnLayoutItemChanged(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            base.OnLayoutItemChanged(item, oldItem);
            DevExpress.Xpf.Docking.LayoutPanel panel = item as DevExpress.Xpf.Docking.LayoutPanel;
            if (panel == null)
            {
                base.ClearValue(ViewStyleProperty);
            }
            else
            {
                if (this.PartHeader != null)
                {
                    BindingHelper.SetBinding(this.PartHeader, DockPaneHeaderPresenter.IsCaptionVisibleProperty, base.LayoutItem, "IsCaptionVisible");
                }
                if (this.PartContent != null)
                {
                    BindingHelper.SetBinding(this.PartContent, DockItemContentPresenter<DockPane, DevExpress.Xpf.Docking.LayoutPanel>.IsControlItemsHostProperty, base.LayoutItem, "IsControlItemsHost");
                    BindingHelper.SetBinding(this.PartContent, DockItemContentPresenter<DockPane, DevExpress.Xpf.Docking.LayoutPanel>.IsDataBoundProperty, base.LayoutItem, "IsDataBound");
                }
                this.UpdateBrushes(panel);
                this.UpdateGeometry(panel);
                base.LayoutItem.Forward(this, ViewStyleProperty, BaseLayoutItem.DockingViewStyleProperty, BindingMode.OneWay);
            }
        }

        public static void SetHitTestType(DependencyObject obj, HitTestType value)
        {
            obj.SetValue(HitTestTypeProperty, value);
        }

        protected override void Subscribe(BaseLayoutItem item)
        {
            base.Subscribe(item);
            if (item != null)
            {
                item.VisualChanged += new EventHandler(this.OnItemVisualChanged);
                item.GeometryChanged += new EventHandler(this.OnItemGeometryChanged);
            }
        }

        protected override void Unsubscribe(BaseLayoutItem item)
        {
            base.Unsubscribe(item);
            if (item != null)
            {
                item.VisualChanged -= new EventHandler(this.OnItemVisualChanged);
                item.GeometryChanged -= new EventHandler(this.OnItemGeometryChanged);
            }
        }

        private void UpdateActualBorderBrush(DevExpress.Xpf.Docking.LayoutPanel panel)
        {
            bool isActive = panel.IsActive;
            this.ActualBorderBrush = (isActive & panel.IsFloatingRootItem) ? this.FloatingActiveBorderBrush : base.BorderBrush;
        }

        private void UpdateActualBorderMargin(DevExpress.Xpf.Docking.LayoutPanel panel)
        {
            Thickness borderMargin = this.BorderMargin;
            if (panel.IsTabPage && !panel.IsDockedAsDocument)
            {
                borderMargin = new Thickness(borderMargin.Left, borderMargin.Top, borderMargin.Right, 0.0);
            }
            else
            {
                borderMargin = new Thickness(this.HasBorderCore() ? borderMargin.Left : 0.0);
            }
            this.ActualBorderMargin = borderMargin;
        }

        private void UpdateActualBorderPadding(DevExpress.Xpf.Docking.LayoutPanel panel)
        {
            Thickness borderPadding = this.BorderPadding;
            if (panel.IsTabPage && (!panel.IsDockedAsDocument && (panel.DockingViewStyle != DockingViewStyle.Light)))
            {
                borderPadding = new Thickness(0.0, 0.0, 0.0, borderPadding.Bottom);
            }
            else
            {
                borderPadding = new Thickness();
            }
            this.ActualBorderPadding = borderPadding;
        }

        private void UpdateActualBorderThickness(DevExpress.Xpf.Docking.LayoutPanel panel)
        {
            Thickness borderThickness = base.BorderThickness;
            if (panel.IsTabPage)
            {
                borderThickness = new Thickness(borderThickness.Left, borderThickness.Top, borderThickness.Right, 0.0);
            }
            if (!this.HasBorderCore())
            {
                borderThickness = new Thickness(0.0);
            }
            this.ActualBorderThickness = borderThickness;
        }

        private void UpdateActualCaptionBackground(DevExpress.Xpf.Docking.LayoutPanel panel)
        {
            Brush background = panel.IsActive ? this.CaptionActiveBackground : this.CaptionNormalBackground;
            if ((panel.ActualAppearanceObject != null) && (panel.ActualAppearanceObject.Background != null))
            {
                background = panel.ActualAppearanceObject.Background;
            }
            this.ActualCaptionBackground = background;
        }

        private void UpdateActualCaptionForeground(DevExpress.Xpf.Docking.LayoutPanel panel)
        {
            Brush foreground = panel.IsActive ? this.CaptionActiveForeground : this.CaptionNormalForeground;
            if ((panel.ActualAppearanceObject != null) && (panel.ActualAppearanceObject.Foreground != null))
            {
                foreground = panel.ActualAppearanceObject.Foreground;
            }
            this.ActualCaptionForeground = foreground;
        }

        protected void UpdateBrushes(DevExpress.Xpf.Docking.LayoutPanel panel)
        {
            this.UpdateActualCaptionBackground(panel);
            this.UpdateActualCaptionForeground(panel);
            this.UpdateActualBorderBrush(panel);
        }

        private void UpdateCaptionCornerRadius(DevExpress.Xpf.Docking.LayoutPanel panel)
        {
            CornerRadius radius = panel.IsFloatingRootItem ? this.FloatingCaptionCornerRadius : this.CaptionCornerRadius;
            if (!panel.ShowCaption)
            {
                radius = new CornerRadius(0.0, 0.0, radius.BottomRight, radius.BottomLeft);
            }
            if (!this.HasBorderCore())
            {
                radius = new CornerRadius();
            }
            this.ActualCaptionCornerRadius = radius;
        }

        protected void UpdateGeometry(DevExpress.Xpf.Docking.LayoutPanel panel)
        {
            this.UpdateActualBorderThickness(panel);
            this.UpdateActualBorderMargin(panel);
            this.UpdateActualBorderPadding(panel);
            this.UpdateCaptionCornerRadius(panel);
            this.UpdatePartContentMargin(panel);
            base.InvalidateMeasure();
        }

        private void UpdatePartContentMargin(DevExpress.Xpf.Docking.LayoutPanel panel)
        {
            if (this.PartContent != null)
            {
                bool flag = this.HasBorderCore();
                this.PartContent.BarContainerMargin = flag ? this.BarContainerMargin : new Thickness(0.0);
                this.PartContent.ContentMargin = flag ? this.ContentMargin : new Thickness(0.0);
            }
        }

        public Brush FloatingActiveBorderBrush
        {
            get => 
                (Brush) base.GetValue(FloatingActiveBorderBrushProperty);
            set => 
                base.SetValue(FloatingActiveBorderBrushProperty, value);
        }

        public Brush ActualBorderBrush
        {
            get => 
                (Brush) base.GetValue(ActualBorderBrushProperty);
            private set => 
                base.SetValue(ActualBorderBrushPropertyKey, value);
        }

        public Thickness ActualBorderMargin
        {
            get => 
                (Thickness) base.GetValue(ActualBorderMarginProperty);
            set => 
                base.SetValue(ActualBorderMarginProperty, value);
        }

        public Thickness ActualBorderPadding
        {
            get => 
                (Thickness) base.GetValue(ActualBorderPaddingProperty);
            set => 
                base.SetValue(ActualBorderPaddingProperty, value);
        }

        public Thickness ActualBorderThickness
        {
            get => 
                (Thickness) base.GetValue(ActualBorderThicknessProperty);
            set => 
                base.SetValue(ActualBorderThicknessProperty, value);
        }

        public Brush ActualCaptionBackground
        {
            get => 
                (Brush) base.GetValue(ActualCaptionBackgroundProperty);
            set => 
                base.SetValue(ActualCaptionBackgroundProperty, value);
        }

        public CornerRadius ActualCaptionCornerRadius
        {
            get => 
                (CornerRadius) base.GetValue(ActualCaptionCornerRadiusProperty);
            set => 
                base.SetValue(ActualCaptionCornerRadiusProperty, value);
        }

        public Brush ActualCaptionForeground
        {
            get => 
                (Brush) base.GetValue(ActualCaptionForegroundProperty);
            set => 
                base.SetValue(ActualCaptionForegroundProperty, value);
        }

        public Thickness BarContainerMargin
        {
            get => 
                (Thickness) base.GetValue(BarContainerMarginProperty);
            set => 
                base.SetValue(BarContainerMarginProperty, value);
        }

        public Thickness BorderMargin
        {
            get => 
                (Thickness) base.GetValue(BorderMarginProperty);
            set => 
                base.SetValue(BorderMarginProperty, value);
        }

        public Thickness BorderPadding
        {
            get => 
                (Thickness) base.GetValue(BorderPaddingProperty);
            set => 
                base.SetValue(BorderPaddingProperty, value);
        }

        public Brush CaptionActiveBackground
        {
            get => 
                (Brush) base.GetValue(CaptionActiveBackgroundProperty);
            set => 
                base.SetValue(CaptionActiveBackgroundProperty, value);
        }

        public Brush CaptionActiveForeground
        {
            get => 
                (Brush) base.GetValue(CaptionActiveForegroundProperty);
            set => 
                base.SetValue(CaptionActiveForegroundProperty, value);
        }

        public CornerRadius CaptionCornerRadius
        {
            get => 
                (CornerRadius) base.GetValue(CaptionCornerRadiusProperty);
            set => 
                base.SetValue(CaptionCornerRadiusProperty, value);
        }

        public Brush CaptionNormalBackground
        {
            get => 
                (Brush) base.GetValue(CaptionNormalBackgroundProperty);
            set => 
                base.SetValue(CaptionNormalBackgroundProperty, value);
        }

        public Brush CaptionNormalForeground
        {
            get => 
                (Brush) base.GetValue(CaptionNormalForegroundProperty);
            set => 
                base.SetValue(CaptionNormalForegroundProperty, value);
        }

        public Thickness ContentMargin
        {
            get => 
                (Thickness) base.GetValue(ContentMarginProperty);
            set => 
                base.SetValue(ContentMarginProperty, value);
        }

        public DataTemplate ControlHostTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ControlHostTemplateProperty);
            set => 
                base.SetValue(ControlHostTemplateProperty, value);
        }

        public DataTemplate DataHostTemplate
        {
            get => 
                (DataTemplate) base.GetValue(DataHostTemplateProperty);
            set => 
                base.SetValue(DataHostTemplateProperty, value);
        }

        public CornerRadius FloatingCaptionCornerRadius
        {
            get => 
                (CornerRadius) base.GetValue(FloatingCaptionCornerRadiusProperty);
            set => 
                base.SetValue(FloatingCaptionCornerRadiusProperty, value);
        }

        public DataTemplate LayoutHostTemplate
        {
            get => 
                (DataTemplate) base.GetValue(LayoutHostTemplateProperty);
            set => 
                base.SetValue(LayoutHostTemplateProperty, value);
        }

        public DockPaneContentPresenter PartContent { get; private set; }

        public DockPaneHeaderPresenter PartHeader { get; private set; }

        public DockingViewStyle ViewStyle
        {
            get => 
                (DockingViewStyle) base.GetValue(ViewStyleProperty);
            set => 
                base.SetValue(ViewStyleProperty, value);
        }

        private DevExpress.Xpf.Docking.LayoutPanel LayoutPanel =>
            base.LayoutItem as DevExpress.Xpf.Docking.LayoutPanel;

        protected bool IsChildMenuVisibleCore
        {
            get => 
                this._IsChildMenuVisibleCore;
            set
            {
                if (this._IsChildMenuVisibleCore != value)
                {
                    this._IsChildMenuVisibleCore = value;
                    this.NotifyListeners();
                }
            }
        }

        bool IMDIChildHost.IsChildMenuVisible =>
            this._IsChildMenuVisibleCore;

        bool IMDIChildHost.CanResize =>
            LayoutItemsHelper.IsFloatingRootItem(base.LayoutItem) && base.LayoutItem.AllowSizing;

        private FloatGroup FloatingRoot =>
            base.LayoutItem.GetRoot() as FloatGroup;

        Size IMDIChildHost.Size
        {
            get => 
                this.FloatingRoot.FloatSize;
            set => 
                this.FloatingRoot.FloatSize = value;
        }

        Size IMDIChildHost.MinSize =>
            LayoutItemsHelper.GetResizingMinSize(this.FloatingRoot);

        Size IMDIChildHost.MaxSize =>
            LayoutItemsHelper.GetResizingMaxSize(this.FloatingRoot);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockPane.<>c <>9 = new DockPane.<>c();

            internal void <.cctor>b__24_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DockPane) d).OnBorderBrushChanged();
            }

            internal void <.cctor>b__24_1(DockPane d)
            {
                d.OnBorderBrushChanged();
            }
        }
    }
}

