namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.LayoutControl.UIAutomation;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Xml;

    [TemplatePart(Name="BorderElement", Type=typeof(Border)), TemplatePart(Name="MaximizeElement", Type=typeof(GroupBoxButton)), TemplatePart(Name="MinimizeElement", Type=typeof(GroupBoxButton)), TemplateVisualState(Name="NormalLayout", GroupName="LayoutStates"), TemplateVisualState(Name="MinimizedLayout", GroupName="LayoutStates"), TemplateVisualState(Name="MaximizedLayout", GroupName="LayoutStates"), DXToolboxBrowsable(DXToolboxItemKind.Free), ToolboxTabName("DX.19.2: Navigation & Layout"), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class GroupBox : MaximizableHeaderedContentControlBase, IGroupBox, IControl, IMaximizableElement
    {
        private bool _IsChangingState;
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(System.Windows.CornerRadius), typeof(DevExpress.Xpf.LayoutControl.GroupBox), null);
        public static readonly DependencyProperty MaximizeElementVisibilityProperty;
        public static readonly DependencyProperty MinimizeElementVisibilityProperty;
        public static readonly DependencyProperty MinimizationDirectionProperty;
        public static readonly DependencyProperty SeparatorBrushProperty;
        public static readonly DependencyProperty ShadowOffsetProperty;
        public static readonly DependencyProperty ShadowVisibilityProperty;
        public static readonly DependencyProperty ShowShadowProperty;
        public static readonly DependencyProperty StateProperty;
        public static readonly DependencyProperty TitleBackgroundProperty;
        public static readonly DependencyProperty TitleForegroundProperty;
        public static readonly DependencyProperty TitleVisibilityProperty;
        public static readonly DependencyProperty NormalTemplateProperty;
        public static readonly DependencyProperty LightTemplateProperty;
        public static readonly DependencyProperty DisplayModeProperty;
        private object _StoredMinSizePropertyValue;
        private object _StoredSizePropertyValue;
        private static readonly Func<DependencyObject, bool> hasAutomationPeer;
        private const string BorderElementName = "BorderElement";
        private const string MaximizeElementName = "MaximizeElement";
        private const string MinimizeElementName = "MinimizeElement";
        private const string LayoutStates = "LayoutStates";
        private const string NormalLayoutState = "NormalLayout";
        private const string MinimizedLayoutState = "MinimizedLayout";
        private const string MaximizedLayoutState = "MaximizedLayout";

        public event ValueChangedEventHandler<GroupBoxState> StateChanged;

        static GroupBox()
        {
            MaximizeElementVisibilityProperty = DependencyProperty.Register("MaximizeElementVisibility", typeof(Visibility), typeof(DevExpress.Xpf.LayoutControl.GroupBox), new PropertyMetadata(Visibility.Collapsed, (d, e) => ((DevExpress.Xpf.LayoutControl.GroupBox) d).OnButtonVisibilityChanged()));
            MinimizeElementVisibilityProperty = DependencyProperty.Register("MinimizeElementVisibility", typeof(Visibility), typeof(DevExpress.Xpf.LayoutControl.GroupBox), new PropertyMetadata(Visibility.Collapsed, (d, e) => ((DevExpress.Xpf.LayoutControl.GroupBox) d).OnButtonVisibilityChanged()));
            MinimizationDirectionProperty = DependencyProperty.Register("MinimizationDirection", typeof(Orientation), typeof(DevExpress.Xpf.LayoutControl.GroupBox), null);
            SeparatorBrushProperty = DependencyProperty.Register("SeparatorBrush", typeof(Brush), typeof(DevExpress.Xpf.LayoutControl.GroupBox), null);
            ShadowOffsetProperty = DependencyProperty.Register("ShadowOffset", typeof(double), typeof(DevExpress.Xpf.LayoutControl.GroupBox), null);
            ShadowVisibilityProperty = DependencyProperty.Register("ShadowVisibility", typeof(Visibility), typeof(DevExpress.Xpf.LayoutControl.GroupBox), null);
            ShowShadowProperty = DependencyProperty.Register("ShowShadow", typeof(GroupBoxShadowVisibility), typeof(DevExpress.Xpf.LayoutControl.GroupBox), new PropertyMetadata((o, e) => ((DevExpress.Xpf.LayoutControl.GroupBox) o).UpdateShadowVisibility()));
            StateProperty = DependencyProperty.Register("State", typeof(GroupBoxState), typeof(DevExpress.Xpf.LayoutControl.GroupBox), new PropertyMetadata(delegate (DependencyObject o, DependencyPropertyChangedEventArgs e) {
                DevExpress.Xpf.LayoutControl.GroupBox box = (DevExpress.Xpf.LayoutControl.GroupBox) o;
                if (!box._IsChangingState)
                {
                    box._IsChangingState = true;
                    if (box.OnStateChanging((GroupBoxState) e.OldValue, (GroupBoxState) e.NewValue))
                    {
                        box.OnStateChanged((GroupBoxState) e.OldValue, (GroupBoxState) e.NewValue);
                    }
                    else
                    {
                        o.SetValue(e.Property, e.OldValue);
                    }
                    box._IsChangingState = false;
                }
            }));
            TitleBackgroundProperty = DependencyProperty.Register("TitleBackground", typeof(Brush), typeof(DevExpress.Xpf.LayoutControl.GroupBox), null);
            TitleForegroundProperty = DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(DevExpress.Xpf.LayoutControl.GroupBox), null);
            TitleVisibilityProperty = DependencyProperty.Register("TitleVisibility", typeof(Visibility), typeof(DevExpress.Xpf.LayoutControl.GroupBox), new PropertyMetadata(Visibility.Visible));
            NormalTemplateProperty = DependencyProperty.Register("NormalTemplate", typeof(ControlTemplate), typeof(DevExpress.Xpf.LayoutControl.GroupBox), new PropertyMetadata(null, (d, e) => ((DevExpress.Xpf.LayoutControl.GroupBox) d).UpdateCurrentTemplate()));
            LightTemplateProperty = DependencyProperty.Register("LightTemplate", typeof(ControlTemplate), typeof(DevExpress.Xpf.LayoutControl.GroupBox), new PropertyMetadata(null, (d, e) => ((DevExpress.Xpf.LayoutControl.GroupBox) d).UpdateCurrentTemplate()));
            DisplayModeProperty = DependencyProperty.Register("DisplayMode", typeof(GroupBoxDisplayMode), typeof(DevExpress.Xpf.LayoutControl.GroupBox), new PropertyMetadata(GroupBoxDisplayMode.Default, (d, e) => ((DevExpress.Xpf.LayoutControl.GroupBox) d).UpdateCurrentTemplate()));
            int? parametersCount = null;
            hasAutomationPeer = ReflectionHelper.CreateInstanceMethodHandler<FrameworkElement, Func<DependencyObject, bool>>(null, "get_HasAutomationPeer", BindingFlags.NonPublic | BindingFlags.Instance, parametersCount, null, true);
            DependencyPropertyRegistrator<DevExpress.Xpf.LayoutControl.GroupBox>.New().OverrideDefaultStyleKey();
        }

        public GroupBox()
        {
            this.UpdateShadowVisibility();
        }

        protected override ControlControllerBase CreateController() => 
            new GroupBoxController(this);

        bool IGroupBox.DesignTimeClick(DXMouseButtonEventArgs args) => 
            this.OnDesignTimeClick(args);

        void IGroupBox.UpdateShadowVisibility()
        {
            this.UpdateShadowVisibility();
        }

        void IMaximizableElement.AfterNormalization()
        {
            if (this.State == GroupBoxState.Maximized)
            {
                this.State = GroupBoxState.Normal;
            }
        }

        void IMaximizableElement.BeforeMaximization()
        {
            this.State = GroupBoxState.Maximized;
        }

        protected string GetLayoutState(GroupBoxState state) => 
            (state != GroupBoxState.Normal) ? ((state != GroupBoxState.Minimized) ? "MaximizedLayout" : "MinimizedLayout") : "NormalLayout";

        protected virtual Visibility GetShadowVisibility() => 
            ((this.ShowShadow == GroupBoxShadowVisibility.Always) || ((this.ShowShadow == GroupBoxShadowVisibility.WhenHasMouse) && base.Controller.IsMouseEntered)) ? Visibility.Visible : Visibility.Collapsed;

        public override void OnApplyTemplate()
        {
            if (this.MaximizeElement != null)
            {
                this.MaximizeElement.Click -= new RoutedEventHandler(this.OnButtonClick);
            }
            if (this.MinimizeElement != null)
            {
                this.MinimizeElement.Click -= new RoutedEventHandler(this.OnButtonClick);
            }
            base.OnApplyTemplate();
            this.BorderElement = base.GetTemplateChild("BorderElement") as Border;
            this.MaximizeElement = base.GetTemplateChild("MaximizeElement") as GroupBoxButton;
            this.MinimizeElement = base.GetTemplateChild("MinimizeElement") as GroupBoxButton;
            if (this.MaximizeElement != null)
            {
                this.MaximizeElement.Click += new RoutedEventHandler(this.OnButtonClick);
            }
            if (this.MinimizeElement != null)
            {
                this.MinimizeElement.Click += new RoutedEventHandler(this.OnButtonClick);
            }
            this.UpdateButtons();
        }

        protected virtual void OnButtonClick(GroupBoxButton button)
        {
            switch (button.Kind)
            {
                case GroupBoxButtonKind.Minimize:
                    this.State = GroupBoxState.Minimized;
                    return;

                case GroupBoxButtonKind.Unminimize:
                case GroupBoxButtonKind.Unmaximize:
                    this.State = GroupBoxState.Normal;
                    return;

                case GroupBoxButtonKind.Maximize:
                    this.State = GroupBoxState.Maximized;
                    return;
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            this.OnButtonClick((GroupBoxButton) sender);
        }

        private void OnButtonVisibilityChanged()
        {
            this.ResetChildrenPeerCache();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new DevExpress.Xpf.LayoutControl.UIAutomation.GroupBoxAutomationPeer(this);

        protected virtual bool OnDesignTimeClick(DXMouseButtonEventArgs args)
        {
            if ((this.MinimizeElementVisibility != Visibility.Visible) || ((this.MinimizeElement == null) || !this.MinimizeElement.Contains(args.GetPosition(null))))
            {
                return false;
            }
            this.OnButtonClick(this.MinimizeElement);
            return true;
        }

        protected virtual void OnStateChanged(GroupBoxState oldValue, GroupBoxState newValue)
        {
            if ((oldValue == GroupBoxState.Maximized) && ((base.Parent is IMaximizingContainer) && ReferenceEquals(((IMaximizingContainer) base.Parent).MaximizedElement, this)))
            {
                ((IMaximizingContainer) base.Parent).MaximizedElement = null;
            }
            this.UpdateButtons();
            this.UpdateState(true);
            this.UpdateSize(oldValue, newValue);
            base.IsMaximizedCore = newValue == GroupBoxState.Maximized;
            if (this.StateChanged != null)
            {
                this.StateChanged(this, new ValueChangedEventArgs<GroupBoxState>(oldValue, newValue));
            }
            if (newValue == GroupBoxState.Maximized)
            {
                ((IMaximizingContainer) base.Parent).MaximizedElement = this;
            }
            base.Dispatcher.BeginInvoke(new Action(this.ResetChildrenPeerCache), new object[0]);
        }

        protected virtual bool OnStateChanging(GroupBoxState oldValue, GroupBoxState newValue) => 
            (newValue != GroupBoxState.Maximized) || (base.Parent is IMaximizingContainer);

        protected internal virtual void ReadCustomizablePropertiesFromXML(XmlReader xml)
        {
            this.ReadPropertyFromXML(xml, StateProperty, "State", typeof(GroupBoxState));
        }

        private void ResetChildrenPeerCache()
        {
            if (this.HasAutomationPeer)
            {
                AutomationPeer peer1 = UIElementAutomationPeer.CreatePeerForElement(this);
                if (peer1 == null)
                {
                    AutomationPeer local1 = peer1;
                }
                else
                {
                    peer1.ResetChildrenCache();
                }
            }
        }

        protected virtual void UpdateButtons()
        {
            if (this.MinimizeElement != null)
            {
                this.MinimizeElement.Kind = (this.State == GroupBoxState.Minimized) ? GroupBoxButtonKind.Unminimize : GroupBoxButtonKind.Minimize;
            }
            if (this.MaximizeElement != null)
            {
                this.MaximizeElement.Kind = (this.State == GroupBoxState.Maximized) ? GroupBoxButtonKind.Unmaximize : GroupBoxButtonKind.Maximize;
            }
        }

        protected virtual void UpdateCurrentTemplate()
        {
            if ((this.NormalTemplate != null) && (this.LightTemplate != null))
            {
                ControlTemplate lightTemplate;
                switch (this.DisplayMode)
                {
                    case GroupBoxDisplayMode.Light:
                        lightTemplate = this.LightTemplate;
                        break;

                    default:
                        lightTemplate = this.NormalTemplate;
                        break;
                }
                if (System.Windows.DependencyPropertyHelper.GetValueSource(this, Control.TemplateProperty).BaseValueSource == BaseValueSource.Default)
                {
                    base.SetCurrentValue(Control.TemplateProperty, lightTemplate);
                }
            }
        }

        protected void UpdateLayoutState(bool useTransitions)
        {
            base.GoToState(this.GetLayoutState(this.State), useTransitions);
            UIElement borderElement = this.BorderElement;
            while (true)
            {
                if (borderElement != null)
                {
                    borderElement.InvalidateMeasure();
                    if (!ReferenceEquals(borderElement, this))
                    {
                        borderElement = (UIElement) VisualTreeHelper.GetParent(borderElement);
                        continue;
                    }
                }
                return;
            }
        }

        protected void UpdateShadowVisibility()
        {
            base.SetValue(ShadowVisibilityProperty, this.GetShadowVisibility());
        }

        protected virtual void UpdateSize(GroupBoxState oldState, GroupBoxState newState)
        {
            DependencyProperty dp = (this.MinimizationDirection == Orientation.Horizontal) ? FrameworkElement.WidthProperty : FrameworkElement.HeightProperty;
            DependencyProperty property2 = (this.MinimizationDirection == Orientation.Horizontal) ? FrameworkElement.MinWidthProperty : FrameworkElement.MinHeightProperty;
            if (newState == GroupBoxState.Minimized)
            {
                if (!double.IsNaN((double) base.GetValue(dp)))
                {
                    this._StoredSizePropertyValue = this.StorePropertyValue(dp);
                    base.SetValue(dp, (double) 1.0 / (double) 0.0);
                }
                if (((double) base.GetValue(property2)) != 0.0)
                {
                    this._StoredMinSizePropertyValue = this.StorePropertyValue(property2);
                    base.SetValue(property2, 0.0);
                }
            }
            if (oldState == GroupBoxState.Minimized)
            {
                if (this._StoredSizePropertyValue != null)
                {
                    this.RestorePropertyValue(dp, this._StoredSizePropertyValue);
                    this._StoredSizePropertyValue = null;
                }
                if (this._StoredMinSizePropertyValue != null)
                {
                    this.RestorePropertyValue(property2, this._StoredMinSizePropertyValue);
                    this._StoredMinSizePropertyValue = null;
                }
            }
        }

        protected override void UpdateState(bool useTransitions)
        {
            base.UpdateState(useTransitions);
            this.UpdateLayoutState(useTransitions);
        }

        protected internal virtual void WriteCustomizablePropertiesToXML(XmlWriter xml)
        {
            this.WritePropertyToXML(xml, StateProperty, "State");
        }

        [Description("Gets or sets the radius of the GroupBox's corners.This is a dependency property.")]
        public System.Windows.CornerRadius CornerRadius
        {
            get => 
                (System.Windows.CornerRadius) base.GetValue(CornerRadiusProperty);
            set => 
                base.SetValue(CornerRadiusProperty, value);
        }

        [Description("Gets or sets whether the Maximize Element is displayed within the GroupBox's header. The Maximize Element is in effect when the GroupBox is an item of a FlowLayoutControl. This is a dependency property.")]
        public Visibility MaximizeElementVisibility
        {
            get => 
                (Visibility) base.GetValue(MaximizeElementVisibilityProperty);
            set => 
                base.SetValue(MaximizeElementVisibilityProperty, value);
        }

        [Description("Gets or sets how the GroupBox is minimized.This is a dependency property.")]
        public Orientation MinimizationDirection
        {
            get => 
                (Orientation) base.GetValue(MinimizationDirectionProperty);
            set => 
                base.SetValue(MinimizationDirectionProperty, value);
        }

        [Description("Gets or sets whether the Minimize Element is displayed within the GroupBox's header. This is a dependency property.")]
        public Visibility MinimizeElementVisibility
        {
            get => 
                (Visibility) base.GetValue(MinimizeElementVisibilityProperty);
            set => 
                base.SetValue(MinimizeElementVisibilityProperty, value);
        }

        [Description("Gets or sets the brush used to paint a line that separates the GroupBox's title and content.This is a dependency property.")]
        public Brush SeparatorBrush
        {
            get => 
                (Brush) base.GetValue(SeparatorBrushProperty);
            set => 
                base.SetValue(SeparatorBrushProperty, value);
        }

        [Description("Gets or sets the size of the shadow, enabled if the GroupBox.ShowShadow option is enabled.This is a dependency property.")]
        public double ShadowOffset
        {
            get => 
                (double) base.GetValue(ShadowOffsetProperty);
            set => 
                base.SetValue(ShadowOffsetProperty, value);
        }

        [Description("Gets or sets when and if a shadow is visible for the GroupBox. This is a dependency property.")]
        public GroupBoxShadowVisibility ShowShadow
        {
            get => 
                (GroupBoxShadowVisibility) base.GetValue(ShowShadowProperty);
            set => 
                base.SetValue(ShowShadowProperty, value);
        }

        [Description("Gets or sets the GroupBox's state.")]
        public GroupBoxState State
        {
            get => 
                (GroupBoxState) base.GetValue(StateProperty);
            set => 
                base.SetValue(StateProperty, value);
        }

        [Description("Gets or sets the brush used to render the GroupBox's title.This is a dependency property.")]
        public Brush TitleBackground
        {
            get => 
                (Brush) base.GetValue(TitleBackgroundProperty);
            set => 
                base.SetValue(TitleBackgroundProperty, value);
        }

        [Description("Gets or sets the brush used to paint the title of the current GroupBox.")]
        public Brush TitleForeground
        {
            get => 
                (Brush) base.GetValue(TitleForegroundProperty);
            set => 
                base.SetValue(TitleForegroundProperty, value);
        }

        [Description("Gets or sets whether the GroupBox displays a title. This is a dependency property.")]
        public Visibility TitleVisibility
        {
            get => 
                (Visibility) base.GetValue(TitleVisibilityProperty);
            set => 
                base.SetValue(TitleVisibilityProperty, value);
        }

        public ControlTemplate NormalTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(NormalTemplateProperty);
            set => 
                base.SetValue(NormalTemplateProperty, value);
        }

        public ControlTemplate LightTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(LightTemplateProperty);
            set => 
                base.SetValue(LightTemplateProperty, value);
        }

        public GroupBoxDisplayMode DisplayMode
        {
            get => 
                (GroupBoxDisplayMode) base.GetValue(DisplayModeProperty);
            set => 
                base.SetValue(DisplayModeProperty, value);
        }

        private bool HasAutomationPeer =>
            (hasAutomationPeer == null) ? false : hasAutomationPeer(this);

        protected Border BorderElement { get; set; }

        protected GroupBoxButton MaximizeElement { get; set; }

        protected GroupBoxButton MinimizeElement { get; set; }

        Rect IGroupBox.MinimizeElementBounds =>
            ((this.MinimizeElement == null) || !this.MinimizeElement.IsInVisualTree()) ? Rect.Empty : this.MinimizeElement.GetBounds(this);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.LayoutControl.GroupBox.<>c <>9 = new DevExpress.Xpf.LayoutControl.GroupBox.<>c();

            internal void <.cctor>b__19_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.GroupBox) d).OnButtonVisibilityChanged();
            }

            internal void <.cctor>b__19_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.GroupBox) d).OnButtonVisibilityChanged();
            }

            internal void <.cctor>b__19_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.GroupBox) o).UpdateShadowVisibility();
            }

            internal void <.cctor>b__19_3(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                DevExpress.Xpf.LayoutControl.GroupBox box = (DevExpress.Xpf.LayoutControl.GroupBox) o;
                if (!box._IsChangingState)
                {
                    box._IsChangingState = true;
                    if (box.OnStateChanging((GroupBoxState) e.OldValue, (GroupBoxState) e.NewValue))
                    {
                        box.OnStateChanged((GroupBoxState) e.OldValue, (GroupBoxState) e.NewValue);
                    }
                    else
                    {
                        o.SetValue(e.Property, e.OldValue);
                    }
                    box._IsChangingState = false;
                }
            }

            internal void <.cctor>b__19_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.GroupBox) d).UpdateCurrentTemplate();
            }

            internal void <.cctor>b__19_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.GroupBox) d).UpdateCurrentTemplate();
            }

            internal void <.cctor>b__19_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.GroupBox) d).UpdateCurrentTemplate();
            }
        }
    }
}

