namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.ModuleInjection;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.LayoutControl.UIAutomation;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Xml;

    [StyleTypedProperty(Property="GroupHeaderStyle", StyleTargetType=typeof(TileGroupHeader)), LicenseProvider(typeof(DX_WPF_LicenseProvider)), DXToolboxBrowsable]
    public class TileLayoutControl : FlowLayoutControl, ITileLayoutControl, IFlowLayoutControl, ILayoutControlBase, IScrollControl, IPanel, IControl, ILayoutModelBase, IFlowLayoutModel, ITileLayoutModel
    {
        public static readonly Brush DefaultBackground = new SolidColorBrush(Colors.Transparent);
        public const double DefaultGroupHeaderSpace = 11.0;
        public const double DefaultItemSpace = 10.0;
        public const double DefaultLayerSpace = 70.0;
        public static readonly Thickness DefaultPadding = new Thickness(120.0, 110.0, 120.0, 110.0);
        public static readonly DependencyProperty AllowGroupHeaderEditingProperty;
        public static readonly DependencyProperty GroupHeaderProperty;
        public static readonly DependencyProperty GroupHeaderSpaceProperty;
        public static readonly DependencyProperty GroupHeaderStyleProperty;
        public static readonly DependencyProperty GroupHeaderTemplateProperty;
        public static readonly DependencyProperty TileClickCommandProperty;
        public static readonly DependencyProperty ShowGroupHeadersProperty;

        public event EventHandler<TileClickEventArgs> TileClick;

        static TileLayoutControl()
        {
            AllowGroupHeaderEditingProperty = DependencyProperty.Register("AllowGroupHeaderEditing", typeof(bool), typeof(TileLayoutControl), new PropertyMetadata((o, e) => ((TileLayoutControl) o).OnAllowGroupHeaderEditingChanged()));
            GroupHeaderProperty = DependencyProperty.RegisterAttached("GroupHeader", typeof(object), typeof(TileLayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            GroupHeaderSpaceProperty = DependencyProperty.Register("GroupHeaderSpace", typeof(double), typeof(TileLayoutControl), new PropertyMetadata(11.0, (o, e) => ((TileLayoutControl) o).OnGroupHeaderSpaceChanged()));
            GroupHeaderStyleProperty = DependencyProperty.Register("GroupHeaderStyle", typeof(Style), typeof(TileLayoutControl), new PropertyMetadata((o, e) => ((TileLayoutControl) o).GroupHeaders.ItemStyle = (Style) e.NewValue));
            GroupHeaderTemplateProperty = DependencyProperty.Register("GroupHeaderTemplate", typeof(DataTemplate), typeof(TileLayoutControl), new PropertyMetadata((o, e) => ((TileLayoutControl) o).GroupHeaders.ItemContentTemplate = (DataTemplate) e.NewValue));
            TileClickCommandProperty = DependencyProperty.Register("TileClickCommand", typeof(ICommand), typeof(TileLayoutControl), null);
            ShowGroupHeadersProperty = DependencyProperty.Register("ShowGroupHeaders", typeof(bool), typeof(TileLayoutControl), new PropertyMetadata(true, (o, e) => ((TileLayoutControl) o).OnShowGroupHeadersChanged()));
            LayoutControlStrategyRegistrator.RegisterTileLayoutControlStrategy();
            FlowLayoutControl.AllowAddFlowBreaksDuringItemMovingProperty.OverrideMetadata(typeof(TileLayoutControl), new PropertyMetadata(true));
            FlowLayoutControl.AllowItemMovingProperty.OverrideMetadata(typeof(TileLayoutControl), new PropertyMetadata(true));
            FlowLayoutControl.AnimateItemMovingProperty.OverrideMetadata(typeof(TileLayoutControl), new PropertyMetadata(true));
            Panel.BackgroundProperty.OverrideMetadata(typeof(TileLayoutControl), new FrameworkPropertyMetadata(DefaultBackground));
            LayoutControlBase.ItemSpaceProperty.OverrideMetadata(typeof(TileLayoutControl), new PropertyMetadata(10.0));
            FlowLayoutControl.LayerSpaceProperty.OverrideMetadata(typeof(TileLayoutControl), new PropertyMetadata(70.0));
            LayoutControlBase.PaddingProperty.OverrideMetadata(typeof(TileLayoutControl), new PropertyMetadata(DefaultPadding));
        }

        public TileLayoutControl()
        {
            this.GroupHeaders = new TileGroupHeaders(this);
        }

        protected override PanelControllerBase CreateController() => 
            new TileLayoutController(this);

        protected override FrameworkElement CreateItem() => 
            new Tile();

        protected override LayoutProviderBase CreateLayoutProvider() => 
            new TileLayoutProvider(this);

        protected override LayoutParametersBase CreateLayoutProviderParameters() => 
            new TileLayoutParameters(base.ItemSpace, base.LayerSpace, this.GroupHeaderSpace, base.BreakFlowToFit, this.GroupHeaders);

        void ITileLayoutControl.OnTileSizeChanged(ITile tile)
        {
            base.InvalidateMeasure();
        }

        void ITileLayoutControl.StopGroupHeaderEditing()
        {
            this.GroupHeaders.StopEditing();
        }

        void ITileLayoutControl.TileClick(ITile tile)
        {
            if (tile is Tile)
            {
                this.OnTileClick((Tile) tile);
            }
        }

        protected override FrameworkElement FindByXMLID(string id) => 
            base.FindByXMLID(id) ?? base.GetLogicalChildren(false).OfType<FrameworkElement>().FirstOrDefault<FrameworkElement>(x => (x.Name == id));

        public static object GetGroupHeader(UIElement element) => 
            element.GetValue(GroupHeaderProperty);

        protected override void InitItem(FrameworkElement item)
        {
            item.SetBinding(ContentControlBase.ContentProperty, new Binding());
        }

        protected override bool IsTempChild(UIElement child) => 
            base.IsTempChild(child) || this.GroupHeaders.IsItem(child);

        protected virtual void OnAllowGroupHeaderEditingChanged()
        {
            this.GroupHeaders.AreEditable = this.AllowGroupHeaderEditing;
        }

        protected override Size OnArrange(Rect bounds)
        {
            this.GroupHeaders.MarkItemsAsUnused();
            Size size = base.OnArrange(bounds);
            this.GroupHeaders.DeleteUnusedItems();
            return size;
        }

        protected override void OnAttachedPropertyChanged(FrameworkElement child, DependencyProperty property, object oldValue, object newValue)
        {
            base.OnAttachedPropertyChanged(child, property, oldValue, newValue);
            if (ReferenceEquals(property, GroupHeaderProperty))
            {
                this.OnGroupHeaderChanged(child);
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new TileLayoutControlAutomationPeer(this);

        protected virtual void OnGroupHeaderChanged(FrameworkElement child)
        {
            base.InvalidateArrange();
        }

        protected virtual void OnGroupHeaderSpaceChanged()
        {
            base.InvalidateArrange();
        }

        protected virtual void OnShowGroupHeadersChanged()
        {
            base.InvalidateArrange();
        }

        protected virtual void OnTileClick(Tile tile)
        {
            if (this.TileClick != null)
            {
                this.TileClick(this, new TileClickEventArgs(tile));
            }
            if ((this.TileClickCommand != null) && this.TileClickCommand.CanExecute(tile))
            {
                this.TileClickCommand.Execute(tile);
            }
        }

        protected override void ReadCustomizablePropertiesFromXML(FrameworkElement element, XmlReader xml)
        {
            base.ReadCustomizablePropertiesFromXML(element, xml);
            element.ReadPropertyFromXML(xml, GroupHeaderProperty, "GroupHeader", typeof(object));
            (element as Tile).Do<Tile>(x => x.ReadCustomizablePropertiesFromXML(xml));
        }

        public static void SetGroupHeader(UIElement element, object value)
        {
            element.SetValue(GroupHeaderProperty, value);
        }

        protected override void WriteCustomizablePropertiesToXML(FrameworkElement element, XmlWriter xml)
        {
            base.WriteCustomizablePropertiesToXML(element, xml);
            element.WritePropertyToXML(xml, GroupHeaderProperty, "GroupHeader");
            (element as Tile).Do<Tile>(x => x.WriteCustomizablePropertiesToXML(xml));
        }

        [Description("Gets or sets whether end-users are allowed to edit group headers. This is a dependency property.")]
        public bool AllowGroupHeaderEditing
        {
            get => 
                (bool) base.GetValue(AllowGroupHeaderEditingProperty);
            set => 
                base.SetValue(AllowGroupHeaderEditingProperty, value);
        }

        [Description("Gets or sets the vertical distance between tile group headers and tiles.This is a dependency property.")]
        public double GroupHeaderSpace
        {
            get => 
                (double) base.GetValue(GroupHeaderSpaceProperty);
            set => 
                base.SetValue(GroupHeaderSpaceProperty, value);
        }

        [Description("Gets or sets a style applied to tile group headers.This is a dependency property.")]
        public Style GroupHeaderStyle
        {
            get => 
                (Style) base.GetValue(GroupHeaderStyleProperty);
            set => 
                base.SetValue(GroupHeaderStyleProperty, value);
        }

        [Description("Gets or sets a data template used to display tile group headers.This is a dependency property.")]
        public DataTemplate GroupHeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(GroupHeaderTemplateProperty);
            set => 
                base.SetValue(GroupHeaderTemplateProperty, value);
        }

        [Description("Gets or sets the command to invoke when a tile is clicked. This is a dependency property.")]
        public ICommand TileClickCommand
        {
            get => 
                (ICommand) base.GetValue(TileClickCommandProperty);
            set => 
                base.SetValue(TileClickCommandProperty, value);
        }

        public bool ShowGroupHeaders
        {
            get => 
                (bool) base.GetValue(ShowGroupHeadersProperty);
            set => 
                base.SetValue(ShowGroupHeadersProperty, value);
        }

        protected TileGroupHeaders GroupHeaders { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TileLayoutControl.<>c <>9 = new TileLayoutControl.<>c();

            internal void <.cctor>b__14_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((TileLayoutControl) o).OnAllowGroupHeaderEditingChanged();
            }

            internal void <.cctor>b__14_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((TileLayoutControl) o).OnGroupHeaderSpaceChanged();
            }

            internal void <.cctor>b__14_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((TileLayoutControl) o).GroupHeaders.ItemStyle = (Style) e.NewValue;
            }

            internal void <.cctor>b__14_3(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((TileLayoutControl) o).GroupHeaders.ItemContentTemplate = (DataTemplate) e.NewValue;
            }

            internal void <.cctor>b__14_4(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((TileLayoutControl) o).OnShowGroupHeadersChanged();
            }
        }
    }
}

