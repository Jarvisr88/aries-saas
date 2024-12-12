namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.LayoutControl.UIAutomation;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Xml;

    [LicenseProvider(typeof(DX_WPF_ControlRequiredForReports_LicenseProvider)), StyleTypedProperty(Property="ItemSizerStyle", StyleTargetType=typeof(ElementSizer)), DXToolboxBrowsable]
    public class DockLayoutControl : LayoutControlBase, IDockLayoutControl, ILayoutControlBase, IScrollControl, IPanel, IControl, ILayoutModelBase, IDockLayoutModel
    {
        public static readonly DependencyProperty AllowItemSizingProperty;
        public static readonly DependencyProperty AllowHorizontalSizingProperty;
        public static readonly DependencyProperty AllowVerticalSizingProperty;
        public static readonly DependencyProperty DockProperty;
        public static readonly DependencyProperty ItemSizerStyleProperty;
        public static readonly DependencyProperty UseDesiredWidthAsMaxWidthProperty;
        public static readonly DependencyProperty UseDesiredHeightAsMaxHeightProperty;

        static DockLayoutControl()
        {
            AllowItemSizingProperty = DependencyProperty.Register("AllowItemSizing", typeof(bool), typeof(DockLayoutControl), new PropertyMetadata(true, (o, e) => ((DockLayoutControl) o).OnAllowItemSizingChanged()));
            AllowHorizontalSizingProperty = DependencyProperty.RegisterAttached("AllowHorizontalSizing", typeof(bool), typeof(DockLayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            AllowVerticalSizingProperty = DependencyProperty.RegisterAttached("AllowVerticalSizing", typeof(bool), typeof(DockLayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            DockProperty = DependencyProperty.RegisterAttached("Dock", typeof(Dock), typeof(DockLayoutControl), new PropertyMetadata(Dock.Left, new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            ItemSizerStyleProperty = DependencyProperty.Register("ItemSizerStyle", typeof(Style), typeof(DockLayoutControl), new PropertyMetadata((o, e) => ((DockLayoutControl) o).ItemSizers.ItemStyle = (Style) e.NewValue));
            UseDesiredWidthAsMaxWidthProperty = DependencyProperty.RegisterAttached("UseDesiredWidthAsMaxWidth", typeof(bool), typeof(DockLayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            UseDesiredHeightAsMaxHeightProperty = DependencyProperty.RegisterAttached("UseDesiredHeightAsMaxHeight", typeof(bool), typeof(DockLayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            LayoutControlBase.PaddingProperty.OverrideMetadata(typeof(DockLayoutControl), new PropertyMetadata(new Thickness(0.0)));
        }

        public DockLayoutControl()
        {
            About.CheckLicenseShowNagScreen(typeof(DockLayoutControl));
            this.ItemSizers = this.CreateItemSizers();
            this.ItemSizers.SizingAreaWidth = base.ItemSpace;
        }

        protected override PanelControllerBase CreateController() => 
            new DockLayoutController(this);

        protected virtual ElementSizers CreateItemSizers() => 
            new ElementSizers(this);

        protected override LayoutProviderBase CreateLayoutProvider() => 
            new DockLayoutProvider(this);

        protected override LayoutParametersBase CreateLayoutProviderParameters() => 
            new DockLayoutParameters(base.ItemSpace, this.AllowItemSizing ? this.ItemSizers : null);

        public static bool GetAllowHorizontalSizing(UIElement element) => 
            (bool) element.GetValue(AllowHorizontalSizingProperty);

        public static bool GetAllowVerticalSizing(UIElement element) => 
            (bool) element.GetValue(AllowVerticalSizingProperty);

        public static Dock GetDock(UIElement element) => 
            (Dock) element.GetValue(DockProperty);

        public static bool GetUseDesiredHeightAsMaxHeight(UIElement element) => 
            (bool) element.GetValue(UseDesiredHeightAsMaxHeightProperty);

        public static bool GetUseDesiredWidthAsMaxWidth(UIElement element) => 
            (bool) element.GetValue(UseDesiredWidthAsMaxWidthProperty);

        protected virtual void InitChildrenMaxSizeAsDesiredSize()
        {
            foreach (FrameworkElement element in base.GetLogicalChildren(true))
            {
                if (GetUseDesiredWidthAsMaxWidth(element) && ((element.DesiredSize.Width != 0.0) && double.IsInfinity(element.MaxWidth)))
                {
                    element.MaxWidth = element.DesiredSize.Width;
                }
                if (GetUseDesiredHeightAsMaxHeight(element) && ((element.DesiredSize.Height != 0.0) && double.IsInfinity(element.MaxHeight)))
                {
                    element.MaxHeight = element.DesiredSize.Height;
                }
            }
        }

        protected override bool IsTempChild(UIElement child) => 
            base.IsTempChild(child) || this.ItemSizers.IsItem(child);

        protected virtual void OnAllowHorizontalSizingChanged(FrameworkElement child)
        {
            base.InvalidateArrange();
        }

        protected virtual void OnAllowItemSizingChanged()
        {
            base.InvalidateArrange();
        }

        protected virtual void OnAllowVerticalSizingChanged(FrameworkElement child)
        {
            base.InvalidateArrange();
        }

        protected override Size OnArrange(Rect bounds)
        {
            this.ItemSizers.MarkItemsAsUnused();
            Size size = base.OnArrange(bounds);
            this.ItemSizers.DeleteUnusedItems();
            return size;
        }

        protected override void OnAttachedPropertyChanged(FrameworkElement child, DependencyProperty property, object oldValue, object newValue)
        {
            base.OnAttachedPropertyChanged(child, property, oldValue, newValue);
            if (ReferenceEquals(property, AllowHorizontalSizingProperty))
            {
                this.OnAllowHorizontalSizingChanged(child);
            }
            if (ReferenceEquals(property, AllowVerticalSizingProperty))
            {
                this.OnAllowVerticalSizingChanged(child);
            }
            if (ReferenceEquals(property, DockProperty))
            {
                this.OnDockChanged(child, (Dock) oldValue, (Dock) newValue);
            }
            if (ReferenceEquals(property, UseDesiredWidthAsMaxWidthProperty))
            {
                this.OnUseDesiredWidthAsMaxWidthChanged(child);
            }
            if (ReferenceEquals(property, UseDesiredHeightAsMaxHeightProperty))
            {
                this.OnUseDesiredHeightAsMaxHeightChanged(child);
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new DockLayoutControlAutomationPeer(this);

        protected virtual void OnDockChanged(FrameworkElement child, Dock oldValue, Dock newValue)
        {
            base.Changed();
        }

        protected override void OnItemSpaceChanged(double oldValue, double newValue)
        {
            base.OnItemSpaceChanged(oldValue, newValue);
            if (this.ItemSizers != null)
            {
                this.ItemSizers.SizingAreaWidth = base.ItemSpace;
            }
        }

        protected override Size OnMeasure(Size availableSize)
        {
            Size size = base.OnMeasure(availableSize);
            this.InitChildrenMaxSizeAsDesiredSize();
            return size;
        }

        protected virtual void OnUseDesiredHeightAsMaxHeightChanged(FrameworkElement child)
        {
            if (GetUseDesiredHeightAsMaxHeight(child) && (child.DesiredSize.Height != 0.0))
            {
                double height = child.Height;
                if (!double.IsNaN(height))
                {
                    child.Height = double.NaN;
                    base.UpdateLayout();
                }
                child.MaxHeight = child.DesiredSize.Height;
                child.Height = height;
            }
            if (!GetUseDesiredHeightAsMaxHeight(child))
            {
                child.MaxHeight = double.PositiveInfinity;
            }
        }

        protected virtual void OnUseDesiredWidthAsMaxWidthChanged(FrameworkElement child)
        {
            if (GetUseDesiredWidthAsMaxWidth(child) && (child.DesiredSize.Width != 0.0))
            {
                double width = child.Width;
                if (!double.IsNaN(width))
                {
                    child.Width = double.NaN;
                    base.UpdateLayout();
                }
                child.MaxWidth = child.DesiredSize.Width;
                child.Width = width;
            }
            if (!GetUseDesiredWidthAsMaxWidth(child))
            {
                child.MaxWidth = double.PositiveInfinity;
            }
        }

        protected override void ReadCustomizablePropertiesFromXML(FrameworkElement element, XmlReader xml)
        {
            base.ReadCustomizablePropertiesFromXML(element, xml);
            element.ReadPropertyFromXML(xml, FrameworkElement.WidthProperty, "Width", typeof(double));
            element.ReadPropertyFromXML(xml, FrameworkElement.HeightProperty, "Height", typeof(double));
        }

        public static void SetAllowHorizontalSizing(UIElement element, bool value)
        {
            element.SetValue(AllowHorizontalSizingProperty, value);
        }

        public static void SetAllowVerticalSizing(UIElement element, bool value)
        {
            element.SetValue(AllowVerticalSizingProperty, value);
        }

        public static void SetDock(UIElement element, Dock value)
        {
            element.SetValue(DockProperty, value);
        }

        public static void SetUseDesiredHeightAsMaxHeight(UIElement element, bool value)
        {
            element.SetValue(UseDesiredHeightAsMaxHeightProperty, value);
        }

        public static void SetUseDesiredWidthAsMaxWidth(UIElement element, bool value)
        {
            element.SetValue(UseDesiredWidthAsMaxWidthProperty, value);
        }

        protected override void WriteCustomizablePropertiesToXML(FrameworkElement element, XmlWriter xml)
        {
            base.WriteCustomizablePropertiesToXML(element, xml);
            element.WritePropertyToXML(xml, FrameworkElement.WidthProperty, "Width");
            element.WritePropertyToXML(xml, FrameworkElement.HeightProperty, "Height");
        }

        [Description("Gets or sets whether item re-sizing is enabled within the DockLayoutControl.This is a dependency property.")]
        public bool AllowItemSizing
        {
            get => 
                (bool) base.GetValue(AllowItemSizingProperty);
            set => 
                base.SetValue(AllowItemSizingProperty, value);
        }

        [Description("Gets or sets the style applied to visual elements used to re-size the DockLayoutControl's items vertically or horizontally.")]
        public Style ItemSizerStyle
        {
            get => 
                (Style) base.GetValue(ItemSizerStyleProperty);
            set => 
                base.SetValue(ItemSizerStyleProperty, value);
        }

        protected ElementSizers ItemSizers { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockLayoutControl.<>c <>9 = new DockLayoutControl.<>c();

            internal void <.cctor>b__17_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutControl) o).OnAllowItemSizingChanged();
            }

            internal void <.cctor>b__17_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DockLayoutControl) o).ItemSizers.ItemStyle = (Style) e.NewValue;
            }
        }
    }
}

