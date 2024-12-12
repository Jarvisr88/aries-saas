namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.LayoutControl.Serialization;
    using DevExpress.Xpf.LayoutControl.UIAutomation;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Resources;
    using System.Xml;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), StyleTypedProperty(Property="CustomizationControlStyle", StyleTargetType=typeof(LayoutControlCustomizationControl)), StyleTypedProperty(Property="ItemCustomizationToolbarStyle", StyleTargetType=typeof(LayoutItemCustomizationToolbar)), StyleTypedProperty(Property="ItemInsertionPointIndicatorStyle", StyleTargetType=typeof(LayoutItemDragAndDropInsertionPointIndicator)), StyleTypedProperty(Property="ItemParentIndicatorStyle", StyleTargetType=typeof(LayoutItemParentIndicator)), StyleTypedProperty(Property="ItemSelectionIndicatorStyle", StyleTargetType=typeof(LayoutItemSelectionIndicator)), StyleTypedProperty(Property="ItemSizerStyle", StyleTargetType=typeof(ElementSizer)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class LayoutControl : LayoutGroup, ILayoutControl, ILayoutGroup, ILayoutControlBase, IScrollControl, IPanel, IControl, ILayoutModelBase, ILayoutGroupModel, ILiveCustomizationAreasProvider, ILayoutControlModel
    {
        public static readonly DependencyProperty ActualAllowAvailableItemsDuringCustomizationProperty = DependencyProperty.Register("ActualAllowAvailableItemsDuringCustomization", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), null);
        public static readonly DependencyProperty AllowAvailableItemsDuringCustomizationProperty;
        public static readonly DependencyProperty AllowItemMovingDuringCustomizationProperty;
        public static readonly DependencyProperty AllowItemRenamingDuringCustomizationProperty;
        public static readonly DependencyProperty AllowItemSizingProperty;
        public static readonly DependencyProperty AllowItemSizingDuringCustomizationProperty;
        public static readonly DependencyProperty AllowNewItemsDuringCustomizationProperty;
        public static readonly DependencyProperty AllowHorizontalSizingProperty;
        public static readonly DependencyProperty AllowVerticalSizingProperty;
        public static readonly DependencyProperty CustomizationControlStyleProperty;
        public static readonly DependencyProperty CustomizationLabelProperty;
        public static readonly DependencyProperty IsCustomizationProperty;
        private static readonly DependencyProperty IsUserDefinedProperty;
        public static readonly DependencyProperty ItemCustomizationToolbarStyleProperty;
        public static readonly DependencyProperty ItemInsertionPointIndicatorStyleProperty;
        public static readonly DependencyProperty ItemParentIndicatorStyleProperty;
        public static readonly DependencyProperty ItemSelectionIndicatorStyleProperty;
        public static readonly DependencyProperty ItemSizerStyleProperty;
        public static readonly DependencyProperty LayoutUriProperty;
        public static readonly DependencyProperty MovingItemPlaceHolderBrushProperty;
        public static readonly DependencyProperty StretchContentHorizontallyProperty;
        public static readonly DependencyProperty StretchContentVerticallyProperty;
        public static readonly DependencyProperty TabHeaderProperty;
        public static readonly DependencyProperty TabHeaderTemplateProperty;
        public static readonly DependencyProperty UseDesiredWidthAsMaxWidthProperty;
        public static readonly DependencyProperty UseDesiredHeightAsMaxHeightProperty;
        public static readonly DependencyProperty AddRestoredItemsToAvailableItemsProperty;
        public static readonly DependencyProperty UseContentMinSizeProperty;
        private static readonly DependencyProperty IDProperty;
        private static readonly DependencyProperty StringIDProperty;
        private int _LastID;
        private FrameworkElements _AvailableItems = new FrameworkElements();
        private bool _IsLayoutUriChanged;
        private const string AvailableItemsXMLNodeName = "AvailableItems";
        private readonly Locker updateIDLocker = new Locker();
        private ISerializationController serializationControllerCore;

        public event EventHandler<LayoutControlInitNewElementEventArgs> InitNewElement;

        public event EventHandler IsCustomizationChanged
        {
            add
            {
                this.Controller.IsCustomizationChanged += value;
            }
            remove
            {
                this.Controller.IsCustomizationChanged -= value;
            }
        }

        public event EventHandler<LayoutControlReadElementFromXMLEventArgs> ReadElementFromXML;

        public event EventHandler<LayoutControlWriteElementToXMLEventArgs> WriteElementToXML;

        static LayoutControl()
        {
            AllowAvailableItemsDuringCustomizationProperty = DependencyProperty.Register("AllowAvailableItemsDuringCustomization", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(true, (o, e) => ((DevExpress.Xpf.LayoutControl.LayoutControl) o).OnAllowAvailableItemsDuringCustomizationChanged()));
            AllowItemMovingDuringCustomizationProperty = DependencyProperty.Register("AllowItemMovingDuringCustomization", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(true, (o, e) => ((DevExpress.Xpf.LayoutControl.LayoutControl) o).OnAllowItemMovingDuringCustomizationChanged()));
            AllowItemRenamingDuringCustomizationProperty = DependencyProperty.Register("AllowItemRenamingDuringCustomization", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(true));
            AllowItemSizingProperty = DependencyProperty.Register("AllowItemSizing", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(true, (o, e) => ((DevExpress.Xpf.LayoutControl.LayoutControl) o).OnAllowItemSizingChanged()));
            AllowItemSizingDuringCustomizationProperty = DependencyProperty.Register("AllowItemSizingDuringCustomization", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(true));
            AllowNewItemsDuringCustomizationProperty = DependencyProperty.Register("AllowNewItemsDuringCustomization", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(true));
            AllowHorizontalSizingProperty = DependencyProperty.RegisterAttached("AllowHorizontalSizing", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            AllowVerticalSizingProperty = DependencyProperty.RegisterAttached("AllowVerticalSizing", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            CustomizationControlStyleProperty = DependencyProperty.Register("CustomizationControlStyle", typeof(Style), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata((o, e) => ((ILayoutControl) o).InitCustomizationController()));
            CustomizationLabelProperty = DependencyProperty.RegisterAttached("CustomizationLabel", typeof(object), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), null);
            IsCustomizationProperty = DependencyProperty.Register("IsCustomization", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(delegate (DependencyObject o, DependencyPropertyChangedEventArgs e) {
                if (!o.IsInDesignTool())
                {
                    ((DevExpress.Xpf.LayoutControl.LayoutControl) o).Controller.IsCustomization = (bool) e.NewValue;
                }
            }));
            IsUserDefinedProperty = DependencyProperty.RegisterAttached("IsUserDefined", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), null);
            ItemCustomizationToolbarStyleProperty = DependencyProperty.Register("ItemCustomizationToolbarStyle", typeof(Style), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata((o, e) => ((ILayoutControl) o).InitCustomizationController()));
            ItemInsertionPointIndicatorStyleProperty = DependencyProperty.Register("ItemInsertionPointIndicatorStyle", typeof(Style), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata((o, e) => ((DevExpress.Xpf.LayoutControl.LayoutControl) o).Controller.ItemInsertionPointIndicatorStyle = (Style) e.NewValue));
            ItemParentIndicatorStyleProperty = DependencyProperty.Register("ItemParentIndicatorStyle", typeof(Style), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata((o, e) => ((ILayoutControl) o).InitCustomizationController()));
            ItemSelectionIndicatorStyleProperty = DependencyProperty.Register("ItemSelectionIndicatorStyle", typeof(Style), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata((o, e) => ((ILayoutControl) o).InitCustomizationController()));
            ItemSizerStyleProperty = DependencyProperty.Register("ItemSizerStyle", typeof(Style), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata((o, e) => ((DevExpress.Xpf.LayoutControl.LayoutControl) o).SetItemSizerStyle((Style) e.NewValue)));
            LayoutUriProperty = DependencyProperty.Register("LayoutUri", typeof(Uri), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(delegate (DependencyObject o, DependencyPropertyChangedEventArgs e) {
                DevExpress.Xpf.LayoutControl.LayoutControl control = (DevExpress.Xpf.LayoutControl.LayoutControl) o;
                if (control.IsLoaded)
                {
                    control.OnLayoutUriChanged();
                }
                else
                {
                    control._IsLayoutUriChanged = true;
                }
            }));
            MovingItemPlaceHolderBrushProperty = DependencyProperty.Register("MovingItemPlaceHolderBrush", typeof(Brush), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(LayoutControlBase.DefaultMovingItemPlaceHolderBrush));
            StretchContentHorizontallyProperty = DependencyProperty.Register("StretchContentHorizontally", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(true, (o, e) => ((DevExpress.Xpf.LayoutControl.LayoutControl) o).OnStretchContentChanged()));
            StretchContentVerticallyProperty = DependencyProperty.Register("StretchContentVertically", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(true, (o, e) => ((DevExpress.Xpf.LayoutControl.LayoutControl) o).OnStretchContentChanged()));
            TabHeaderProperty = DependencyProperty.RegisterAttached("TabHeader", typeof(object), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            TabHeaderTemplateProperty = DependencyProperty.RegisterAttached("TabHeaderTemplate", typeof(DataTemplate), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            UseDesiredWidthAsMaxWidthProperty = DependencyProperty.RegisterAttached("UseDesiredWidthAsMaxWidth", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            UseDesiredHeightAsMaxHeightProperty = DependencyProperty.RegisterAttached("UseDesiredHeightAsMaxHeight", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(new PropertyChangedCallback(PanelBase.OnAttachedPropertyChanged)));
            AddRestoredItemsToAvailableItemsProperty = DependencyProperty.Register("AddRestoredItemsToAvailableItems", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(true));
            UseContentMinSizeProperty = DependencyProperty.Register("UseContentMinSize", typeof(bool), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(DevExpress.Xpf.LayoutControl.LayoutControl.UseContentMinSizePropertyChanged)));
            IDProperty = DependencyProperty.RegisterAttached("ID", typeof(int), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), null);
            StringIDProperty = DependencyProperty.RegisterAttached("StringID", typeof(string), typeof(DevExpress.Xpf.LayoutControl.LayoutControl), null);
            LayoutControlBase.PaddingProperty.OverrideMetadata(typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new PropertyMetadata(new Thickness(12.0)));
            DXSerializer.SerializationIDDefaultProperty.OverrideMetadata(typeof(DevExpress.Xpf.LayoutControl.LayoutControl), new UIPropertyMetadata("LayoutControl"));
            DependencyPropertyRegistrator<DevExpress.Xpf.LayoutControl.LayoutControl>.New().OverrideDefaultStyleKey();
        }

        public LayoutControl()
        {
            this.AvailableItems.CollectionChanged += (o, e) => this.OnAvailableItemsChanged(e);
            this.VisibleAvailableItems = new FrameworkElements();
            ItemsContainer container1 = new ItemsContainer();
            container1.Visibility = Visibility.Collapsed;
            this.AvailableItemsContainer = container1;
            this.AvailableItemsContainer.ChildVisibilityChanged = delegate (FrameworkElement child) {
                if (child.GetVisible())
                {
                    this.VisibleAvailableItems.Add(child);
                }
                else
                {
                    this.VisibleAvailableItems.Remove(child);
                }
            };
            this.UpdateActualAllowAvailableItemsDuringCustomization();
            this.serializationControllerCore = this.CreateSerializationController();
        }

        [CompilerGenerated, DebuggerHidden]
        private IEnumerable<UIElement> <>n__0() => 
            base.GetInternalElements();

        protected override void AddChildFromXML(IList children, FrameworkElement element, int index)
        {
            if (!ReferenceEquals(children, this.AvailableItems) || !children.Contains(element))
            {
                base.AddChildFromXML(children, element, index);
            }
        }

        protected override PanelControllerBase CreateController() => 
            new LayoutControlController(this);

        protected virtual ISerializationController CreateSerializationController() => 
            new DevExpress.Xpf.LayoutControl.Serialization.SerializationController(this);

        void ILayoutControl.ControlAdded(FrameworkElement control)
        {
            this.OnControlAdded(control);
        }

        void ILayoutControl.ControlRemoved(FrameworkElement control)
        {
            this.OnControlRemoved(control);
        }

        void ILayoutControl.ControlVisibilityChanged(FrameworkElement control)
        {
            this.Controller.OnControlVisibilityChanged(control);
        }

        void ILayoutControl.DeleteAvailableItem(FrameworkElement item)
        {
            if (item.IsLayoutGroup())
            {
                ((ILayoutGroup) item).MoveNonUserDefinedChildrenToAvailableItems();
            }
            this.AvailableItems.Remove(item);
        }

        FrameworkElement ILayoutControl.FindControl(string id) => 
            this.FindControl(id, null);

        string ILayoutControl.GetID(FrameworkElement control) => 
            this.GetID(control);

        Style ILayoutControl.GetPartStyle(LayoutGroupPartStyle style) => 
            base.GetPartStyle(style);

        void ILayoutControl.InitCustomizationController()
        {
            if (this.Controller.IsCustomization)
            {
                this.InitCustomizationController(this.Controller.CustomizationController);
            }
        }

        void ILayoutControl.InitNewElement(FrameworkElement element)
        {
            this.OnInitNewElement(element);
        }

        bool ILayoutControl.MakeControlVisible(FrameworkElement control)
        {
            if (!control.GetVisible())
            {
                return false;
            }
            LayoutItem item = control.GetLayoutItem(null, true);
            if (item != null)
            {
                control = item;
            }
            while (true)
            {
                ILayoutGroup parent = control.Parent as ILayoutGroup;
                if ((parent == null) || !parent.MakeChildVisible(control))
                {
                    return false;
                }
                if (parent.IsRoot)
                {
                    return true;
                }
                control = parent.Control;
            }
        }

        void ILayoutControl.ModelChanged(LayoutControlModelChangedEventArgs args)
        {
            this.Controller.OnModelChanged(args);
        }

        void ILayoutControl.ReadElementFromXML(XmlReader xml, FrameworkElement element)
        {
            this.OnReadElementFromXML(xml, element);
        }

        void ILayoutControl.SetID(FrameworkElement control)
        {
            this.SetID(control);
        }

        void ILayoutControl.SetID(FrameworkElement control, string id)
        {
            this.SetID(control, id);
        }

        void ILayoutControl.TabClicked(ILayoutGroup group, FrameworkElement selectedTabChild)
        {
            this.Controller.OnTabClicked(group, selectedTabChild);
        }

        void ILayoutControl.WriteElementToXML(XmlWriter xml, FrameworkElement element)
        {
            this.OnWriteElementToXML(xml, element);
        }

        private FrameworkElement FindControl(string id, FrameworkElement exception = null)
        {
            int num;
            if (!(this.AddRestoredItemsToAvailableItems ? int.TryParse(id, out num) : !string.IsNullOrEmpty(id)))
            {
                return this.FindControlByName(id);
            }
            FrameworkElement element = this.FindControl(this.AvailableItems, id, exception);
            return this.FindControl(this, id, exception);
        }

        private FrameworkElement FindControl(FrameworkElements controls, string id, FrameworkElement exception)
        {
            FrameworkElement element2;
            using (IEnumerator<FrameworkElement> enumerator = controls.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        FrameworkElement current = enumerator.Current;
                        if (!ReferenceEquals(current, exception) && (this.GetID(current) == id))
                        {
                            element2 = current;
                        }
                        else
                        {
                            if (!current.IsLayoutGroup())
                            {
                                continue;
                            }
                            FrameworkElement element3 = this.FindControl((ILayoutGroup) current, id, exception);
                            if (element3 == null)
                            {
                                continue;
                            }
                            element2 = element3;
                        }
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return element2;
        }

        private FrameworkElement FindControl(ILayoutGroup group, string id, FrameworkElement exception) => 
            this.FindControl(group.GetLogicalChildren(false), id, exception);

        protected virtual FrameworkElement FindControlByName(string id) => 
            (FrameworkElement) base.FindName(id);

        public static bool GetAllowHorizontalSizing(UIElement element) => 
            (bool) element.GetValue(AllowHorizontalSizingProperty);

        public static bool GetAllowVerticalSizing(UIElement element) => 
            (bool) element.GetValue(AllowVerticalSizingProperty);

        protected override FrameworkElement GetChildContainer(FrameworkElement child) => 
            child.GetLayoutItem(this, false) ?? base.GetChildContainer(child);

        public static string GetCustomizationDefaultLabel(UIElement element)
        {
            string content = null;
            switch (element)
            {
                case (ContentControl _):
                    content = ((ContentControl) element).Content as string;
                    break;

                case (ContentControlBase _):
                    content = ((ContentControlBase) element).Content as string;
                    break;

                case (LayoutGroup _):
                    content = ((LayoutGroup) element).Header as string;
                    break;

                case (LayoutItem _):
                    content = ((LayoutItem) element).Label as string;
                    if (!string.IsNullOrEmpty(content))
                    {
                        char[] trimChars = new char[] { ':', ' ' };
                        content = content.TrimEnd(trimChars);
                    }
                    break;
            }
            if (string.IsNullOrEmpty(content))
            {
                content = "[" + element.GetType().Name + "]";
            }
            return content;
        }

        public static object GetCustomizationLabel(UIElement element) => 
            element.GetValue(CustomizationLabelProperty);

        protected override Type GetGroupType() => 
            typeof(LayoutGroup);

        private string GetID(FrameworkElement control)
        {
            if (!string.IsNullOrEmpty(control.Name))
            {
                return control.Name;
            }
            string str = control.GetValue(IDProperty).ToString();
            return (!this.AddRestoredItemsToAvailableItems ? (control.IsPropertyAssigned(StringIDProperty) ? ((string) control.GetValue(StringIDProperty)) : str) : str);
        }

        [IteratorStateMachine(typeof(<GetInternalElements>d__141))]
        protected override IEnumerable<UIElement> GetInternalElements()
        {
            IEnumerator<UIElement> enumerator = this.<>n__0().GetEnumerator();
            if (enumerator.MoveNext())
            {
                UIElement current = enumerator.Current;
                yield return current;
                yield break;
            }
            else
            {
                enumerator = null;
                yield return this.AvailableItemsContainer;
                yield break;
            }
        }

        public static bool GetIsUserDefined(UIElement element) => 
            (bool) element.GetValue(IsUserDefinedProperty);

        public static object GetTabHeader(UIElement element) => 
            element.GetValue(TabHeaderProperty);

        public static DataTemplate GetTabHeaderTemplate(UIElement element) => 
            (DataTemplate) element.GetValue(TabHeaderTemplateProperty);

        public static bool GetUseDesiredHeightAsMaxHeight(UIElement element) => 
            (bool) element.GetValue(UseDesiredHeightAsMaxHeightProperty);

        public static bool GetUseDesiredWidthAsMaxWidth(UIElement element) => 
            (bool) element.GetValue(UseDesiredWidthAsMaxWidthProperty);

        protected virtual void InitCustomizationController(LayoutControlCustomizationController controller)
        {
            controller.CustomizationControlStyle = this.CustomizationControlStyle;
            controller.ItemCustomizationToolbarStyle = this.ItemCustomizationToolbarStyle;
            controller.ItemParentIndicatorStyle = this.ItemParentIndicatorStyle;
            controller.ItemSelectionIndicatorStyle = this.ItemSelectionIndicatorStyle;
        }

        protected virtual void OnAllowAvailableItemsDuringCustomizationChanged()
        {
            this.UpdateActualAllowAvailableItemsDuringCustomization();
        }

        protected virtual void OnAllowItemMovingDuringCustomizationChanged()
        {
            this.UpdateActualAllowAvailableItemsDuringCustomization();
        }

        protected override Size OnArrange(Rect bounds)
        {
            Size size = bounds.Size();
            if (!this.StretchContentHorizontally || (bounds.Width < base.OriginalDesiredSize.Width))
            {
                bounds.Width = base.OriginalDesiredSize.Width;
            }
            if (!this.StretchContentVertically || (bounds.Height < base.OriginalDesiredSize.Height))
            {
                bounds.Height = base.OriginalDesiredSize.Height;
            }
            base.OnArrange(bounds);
            return size;
        }

        protected virtual void OnAvailableItemsChanged(NotifyCollectionChangedEventArgs args)
        {
            IList list = ((args.Action == NotifyCollectionChangedAction.Add) || (args.Action == NotifyCollectionChangedAction.Replace)) ? args.NewItems : ((args.Action != NotifyCollectionChangedAction.Reset) ? null : this.AvailableItems);
            if (list != null)
            {
                foreach (FrameworkElement element in list)
                {
                    element.SetParent(null);
                    if (this.AddRestoredItemsToAvailableItems || !this.updateIDLocker)
                    {
                        this.UpdateID(element);
                    }
                }
            }
            Predicate<FrameworkElement> isItemAllowed = <>c.<>9__159_0;
            if (<>c.<>9__159_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__159_0;
                isItemAllowed = <>c.<>9__159_0 = item => item.GetVisible();
            }
            this.SynchronizeWithAvailableItems(this.VisibleAvailableItems, args, isItemAllowed);
            this.SynchronizeWithAvailableItems(this.AvailableItemsContainer.Children, args, null);
        }

        protected virtual void OnControlAdded(FrameworkElement control)
        {
            this.UpdateID(control);
            this.AvailableItems.Remove(control);
        }

        protected virtual void OnControlRemoved(FrameworkElement control)
        {
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new LayoutControlAutomationPeer(this);

        protected override void OnInitialized(EventArgs e)
        {
            base.Children.Add(this.AvailableItemsContainer);
            base.OnInitialized(e);
        }

        protected virtual void OnInitNewElement(FrameworkElement element)
        {
            SetIsUserDefined(element, true);
            if (this.InitNewElement != null)
            {
                this.InitNewElement(this, new LayoutControlInitNewElementEventArgs(element));
            }
        }

        protected override void OnItemLabelsAlignmentChanged()
        {
        }

        protected override void OnLayoutUpdated()
        {
            if (this.Controller.IsCustomization)
            {
                this.Controller.CustomizationController.CheckSelectedElementsAreInVisualTree();
            }
            base.OnLayoutUpdated();
        }

        protected virtual void OnLayoutUriChanged()
        {
            if (this.LayoutUri != null)
            {
                StreamResourceInfo resourceStream = Application.GetResourceStream(this.LayoutUri);
                if (resourceStream != null)
                {
                    this.ReadFromXML(XmlReader.Create(resourceStream.Stream));
                }
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (this._IsLayoutUriChanged)
            {
                this._IsLayoutUriChanged = false;
                this.OnLayoutUriChanged();
            }
        }

        protected override Size OnMeasure(Size availableSize)
        {
            if (!this.StretchContentHorizontally)
            {
                availableSize.Width = double.PositiveInfinity;
            }
            if (!this.StretchContentVertically)
            {
                availableSize.Height = double.PositiveInfinity;
            }
            return base.OnMeasure(availableSize);
        }

        protected override void OnPartStyleChanged(LayoutGroupPartStyle style)
        {
            ((ILayoutGroup) this).UpdatePartStyle(style);
        }

        protected virtual void OnReadElementFromXML(XmlReader xml, FrameworkElement element)
        {
            if (this.ReadElementFromXML != null)
            {
                this.ReadElementFromXML(this, new LayoutControlReadElementFromXMLEventArgs(xml, element));
            }
        }

        protected virtual void OnStretchContentChanged()
        {
            base.Changed();
        }

        protected virtual void OnWriteElementToXML(XmlWriter xml, FrameworkElement element)
        {
            if (this.WriteElementToXML != null)
            {
                this.WriteElementToXML(this, new LayoutControlWriteElementToXMLEventArgs(xml, element));
            }
        }

        protected override FrameworkElement ReadChildFromXML(XmlReader xml, IList children, int index)
        {
            FrameworkElement element;
            if (xml.Name != "AvailableItems")
            {
                return base.ReadChildFromXML(xml, children, index);
            }
            base.ReadChildrenFromXML(this.AvailableItems, xml, 0, out element);
            return null;
        }

        public override void ReadFromXML(XmlReader xml)
        {
            using (this.updateIDLocker.Lock())
            {
                base.ReadFromXML(xml);
                if (!this.AddRestoredItemsToAvailableItems)
                {
                    foreach (FrameworkElement element in this.AvailableItems)
                    {
                        this.UpdateID(element);
                    }
                }
            }
            base.OptimizeLayout(true);
        }

        private void RestoreLayoutCore(object path)
        {
            this.SerializationController.RestoreLayout(path);
        }

        private void RestoreLayoutFromStream(Stream stream)
        {
            this.RestoreLayoutCore(stream);
        }

        private void RestoreLayoutFromXml(string path)
        {
            this.RestoreLayoutCore(path);
        }

        private void SaveLayoutToStream(Stream stream)
        {
            this.SerializationController.SaveLayout(stream);
        }

        private void SaveLayoutToXml(string path)
        {
            this.SerializationController.SaveLayout(path);
        }

        public static void SetAllowHorizontalSizing(UIElement element, bool value)
        {
            element.SetValue(AllowHorizontalSizingProperty, value);
        }

        public static void SetAllowVerticalSizing(UIElement element, bool value)
        {
            element.SetValue(AllowVerticalSizingProperty, value);
        }

        public static void SetCustomizationLabel(UIElement element, object value)
        {
            element.SetValue(CustomizationLabelProperty, value);
        }

        private void SetID(FrameworkElement control)
        {
            this._LastID++;
            control.SetValue(IDProperty, this._LastID);
        }

        private void SetID(FrameworkElement control, string id)
        {
            int num;
            control.SetValue(StringIDProperty, id);
            if (int.TryParse(id, out num))
            {
                control.SetValue(IDProperty, num);
                if (num > this._LastID)
                {
                    this._LastID = num;
                }
            }
        }

        public static void SetIsUserDefined(UIElement element, bool value)
        {
            if (!element.IsInDesignTool())
            {
                element.SetValue(IsUserDefinedProperty, value);
            }
        }

        private void SetItemSizerStyle(Style value)
        {
            base.ItemSizerStyle = value;
        }

        public static void SetTabHeader(UIElement element, object value)
        {
            element.SetValue(TabHeaderProperty, value);
        }

        public static void SetTabHeaderTemplate(UIElement element, DataTemplate value)
        {
            element.SetValue(TabHeaderTemplateProperty, value);
        }

        public static void SetUseDesiredHeightAsMaxHeight(UIElement element, bool value)
        {
            element.SetValue(UseDesiredHeightAsMaxHeightProperty, value);
        }

        public static void SetUseDesiredWidthAsMaxWidth(UIElement element, bool value)
        {
            element.SetValue(UseDesiredWidthAsMaxWidthProperty, value);
        }

        protected void SynchronizeWithAvailableItems(IList items, NotifyCollectionChangedEventArgs args, Predicate<FrameworkElement> isItemAllowed = null)
        {
            Action<IList> action = delegate (IList newItems) {
                foreach (FrameworkElement element in newItems)
                {
                    if ((isItemAllowed == null) || isItemAllowed(element))
                    {
                        items.Add(element);
                    }
                }
            };
            Action<IList> action2 = delegate (IList oldItems) {
                foreach (FrameworkElement element in oldItems)
                {
                    items.Remove(element);
                }
            };
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    action(args.NewItems);
                    return;

                case NotifyCollectionChangedAction.Remove:
                    action2(args.OldItems);
                    return;

                case NotifyCollectionChangedAction.Replace:
                    action2(args.OldItems);
                    action(args.NewItems);
                    return;
            }
            items.Clear();
            action(this.AvailableItems);
        }

        private void UpdateActualAllowAvailableItemsDuringCustomization()
        {
            this.SetValue(ActualAllowAvailableItemsDuringCustomizationProperty, !this.AllowItemMovingDuringCustomization ? ((object) 0) : ((object) this.AllowAvailableItemsDuringCustomization));
        }

        private void UpdateID(FrameworkElement control)
        {
            if (!control.IsPropertyAssigned(IDProperty))
            {
                this.SetID(control);
            }
            else if (((int) control.GetValue(IDProperty)) > this._LastID)
            {
                this.SetID(control);
            }
            if (control.IsLayoutGroup())
            {
                foreach (FrameworkElement element in ((ILayoutGroup) control).GetLogicalChildren(false))
                {
                    this.UpdateID(element);
                }
            }
        }

        protected static void UseContentMinSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DevExpress.Xpf.LayoutControl.LayoutControl) d).InvalidateChildrenMeasure();
        }

        protected override void WriteToXMLCore(XmlWriter xml)
        {
            base.WriteToXMLCore(xml);
            xml.WriteStartElement("AvailableItems");
            base.WriteChildrenToXML(this.AvailableItems, xml);
            xml.WriteEndElement();
        }

        public bool AddRestoredItemsToAvailableItems
        {
            get => 
                (bool) base.GetValue(AddRestoredItemsToAvailableItemsProperty);
            set => 
                base.SetValue(AddRestoredItemsToAvailableItemsProperty, value);
        }

        [Description("Gets or sets whether the Available Items feature is enabled in Customization Mode at runtime.")]
        public bool AllowAvailableItemsDuringCustomization
        {
            get => 
                (bool) base.GetValue(AllowAvailableItemsDuringCustomizationProperty);
            set => 
                base.SetValue(AllowAvailableItemsDuringCustomizationProperty, value);
        }

        [Description("Gets or sets whether items can be dragged-and-droppped in customization mode.This is a dependency property.")]
        public bool AllowItemMovingDuringCustomization
        {
            get => 
                (bool) base.GetValue(AllowItemMovingDuringCustomizationProperty);
            set => 
                base.SetValue(AllowItemMovingDuringCustomizationProperty, value);
        }

        [Description("Gets or sets whether an end-user can rename items in Customization Mode.This is a dependency property.")]
        public bool AllowItemRenamingDuringCustomization
        {
            get => 
                (bool) base.GetValue(AllowItemRenamingDuringCustomizationProperty);
            set => 
                base.SetValue(AllowItemRenamingDuringCustomizationProperty, value);
        }

        [Description("Gets or sets whether item re-sizing is enabled within the LayoutControl.This is a dependency property.")]
        public bool AllowItemSizing
        {
            get => 
                (bool) base.GetValue(AllowItemSizingProperty);
            set => 
                base.SetValue(AllowItemSizingProperty, value);
        }

        [Description("Gets or sets whether item re-sizing is enabled in customization mode.This is a dependency property.")]
        public bool AllowItemSizingDuringCustomization
        {
            get => 
                (bool) base.GetValue(AllowItemSizingDuringCustomizationProperty);
            set => 
                base.SetValue(AllowItemSizingDuringCustomizationProperty, value);
        }

        [Description("Gets or sets whether an end-user can add new items (e.g. groups) in Customization Mode.")]
        public bool AllowNewItemsDuringCustomization
        {
            get => 
                (bool) base.GetValue(AllowNewItemsDuringCustomizationProperty);
            set => 
                base.SetValue(AllowNewItemsDuringCustomizationProperty, value);
        }

        [Description("Provides access to hidden items (items in the Available Items list).")]
        public FrameworkElements AvailableItems =>
            this._AvailableItems;

        [Description("Gets or sets the style applied to the Customization Control.")]
        public Style CustomizationControlStyle
        {
            get => 
                (Style) base.GetValue(CustomizationControlStyleProperty);
            set => 
                base.SetValue(CustomizationControlStyleProperty, value);
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public LayoutControlController Controller =>
            (LayoutControlController) base.Controller;

        [Description("Gets or sets whether customization mode is active.This is a dependency property.")]
        public bool IsCustomization
        {
            get => 
                (bool) base.GetValue(IsCustomizationProperty);
            set => 
                base.SetValue(IsCustomizationProperty, value);
        }

        [Description("Gets whether the current object is a root object for layout elements.")]
        public override bool IsRoot =>
            true;

        [Description("Gets or sets the style applied to an item's Customization Toolbar.This is a dependency property.")]
        public Style ItemCustomizationToolbarStyle
        {
            get => 
                (Style) base.GetValue(ItemCustomizationToolbarStyleProperty);
            set => 
                base.SetValue(ItemCustomizationToolbarStyleProperty, value);
        }

        [Description("Gets or sets the style used to render areas where an item being dragged in Customization Mode can be potentially dropped.This is a dependency property.")]
        public Style ItemInsertionPointIndicatorStyle
        {
            get => 
                (Style) base.GetValue(ItemInsertionPointIndicatorStyleProperty);
            set => 
                base.SetValue(ItemInsertionPointIndicatorStyleProperty, value);
        }

        [Description("Gets the alignment of child LayoutItems' content regions.")]
        public override LayoutItemLabelsAlignment ItemLabelsAlignment
        {
            get => 
                LayoutItemLabelsAlignment.Local;
            set
            {
            }
        }

        [Description("Gets or sets the style used to paint a rectangle, indicating an item's parent in Customization Mode.This is a dependency property.")]
        public Style ItemParentIndicatorStyle
        {
            get => 
                (Style) base.GetValue(ItemParentIndicatorStyleProperty);
            set => 
                base.SetValue(ItemParentIndicatorStyleProperty, value);
        }

        [Description("Gets or sets the style used to paint selected items in Customization Mode.This is a dependency property.")]
        public Style ItemSelectionIndicatorStyle
        {
            get => 
                (Style) base.GetValue(ItemSelectionIndicatorStyleProperty);
            set => 
                base.SetValue(ItemSelectionIndicatorStyleProperty, value);
        }

        [Description("Gets or sets the style applied to visual elements used to re-size the LayoutControl's items vertically or horizontally.This is a dependency property.")]
        public Style ItemSizerStyle
        {
            get => 
                (Style) base.GetValue(ItemSizerStyleProperty);
            set => 
                base.SetValue(ItemSizerStyleProperty, value);
        }

        [Description("Gets or sets a uniform resource identifier (URI) of the layout to be loaded.This is a dependency property.")]
        public Uri LayoutUri
        {
            get => 
                (Uri) base.GetValue(LayoutUriProperty);
            set => 
                base.SetValue(LayoutUriProperty, value);
        }

        [Description("Gets or sets the brush used to paint the placeholder of the item that is being dragged in Customization Mode.This is a dependency property.")]
        public Brush MovingItemPlaceHolderBrush
        {
            get => 
                (Brush) base.GetValue(MovingItemPlaceHolderBrushProperty);
            set => 
                base.SetValue(MovingItemPlaceHolderBrushProperty, value);
        }

        [Description("Gets or sets whether the control's immediate children are stretched horizontally to fit the control's width. This property is in effect for the items that have their HorizontalAlignment property set to Stretch.This is a dependency property.")]
        public bool StretchContentHorizontally
        {
            get => 
                (bool) base.GetValue(StretchContentHorizontallyProperty);
            set => 
                base.SetValue(StretchContentHorizontallyProperty, value);
        }

        [Description("Gets or sets whether the control's immediate children are stretched vertically to fit the control's height. This property is in effect for the items that have their VerticalAlignment property set to Stretch.This is a dependency property.")]
        public bool StretchContentVertically
        {
            get => 
                (bool) base.GetValue(StretchContentVerticallyProperty);
            set => 
                base.SetValue(StretchContentVerticallyProperty, value);
        }

        public bool UseContentMinSize
        {
            get => 
                (bool) base.GetValue(UseContentMinSizeProperty);
            set => 
                base.SetValue(UseContentMinSizeProperty, value);
        }

        protected override bool HasGroupBox =>
            false;

        protected override bool HasTabs =>
            false;

        protected override bool IsBorderless =>
            false;

        protected FrameworkElements VisibleAvailableItems { get; private set; }

        private ItemsContainer AvailableItemsContainer { get; set; }

        bool ILayoutControlBase.AllowItemMoving =>
            this.IsInDesignTool() || this.AllowItemMovingDuringCustomization;

        Rect ILayoutGroup.ChildAreaBounds =>
            base.ChildrenBounds;

        bool ILayoutGroup.IsCustomization
        {
            get => 
                base.IsCustomization;
            set
            {
                base.IsCustomization = value;
                if (!this.IsInDesignTool() && (this.IsCustomization != value))
                {
                    this.IsCustomization = value;
                }
            }
        }

        FrameworkElements ILayoutControl.VisibleAvailableItems =>
            this.VisibleAvailableItems;

        protected internal ISerializationController SerializationController =>
            this.serializationControllerCore;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, true, false)]
        public SerializableItemCollection Items
        {
            get => 
                this.SerializationController.Items;
            set => 
                this.SerializationController.Items = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.LayoutControl.LayoutControl.<>c <>9 = new DevExpress.Xpf.LayoutControl.LayoutControl.<>c();
            public static Predicate<FrameworkElement> <>9__159_0;

            internal void <.cctor>b__56_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.LayoutControl) o).OnAllowAvailableItemsDuringCustomizationChanged();
            }

            internal void <.cctor>b__56_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.LayoutControl) o).OnAllowItemMovingDuringCustomizationChanged();
            }

            internal void <.cctor>b__56_10(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                DevExpress.Xpf.LayoutControl.LayoutControl control = (DevExpress.Xpf.LayoutControl.LayoutControl) o;
                if (control.IsLoaded)
                {
                    control.OnLayoutUriChanged();
                }
                else
                {
                    control._IsLayoutUriChanged = true;
                }
            }

            internal void <.cctor>b__56_11(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.LayoutControl) o).OnStretchContentChanged();
            }

            internal void <.cctor>b__56_12(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.LayoutControl) o).OnStretchContentChanged();
            }

            internal void <.cctor>b__56_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.LayoutControl) o).OnAllowItemSizingChanged();
            }

            internal void <.cctor>b__56_3(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ILayoutControl) o).InitCustomizationController();
            }

            internal void <.cctor>b__56_4(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                if (!o.IsInDesignTool())
                {
                    ((DevExpress.Xpf.LayoutControl.LayoutControl) o).Controller.IsCustomization = (bool) e.NewValue;
                }
            }

            internal void <.cctor>b__56_5(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ILayoutControl) o).InitCustomizationController();
            }

            internal void <.cctor>b__56_6(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.LayoutControl) o).Controller.ItemInsertionPointIndicatorStyle = (Style) e.NewValue;
            }

            internal void <.cctor>b__56_7(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ILayoutControl) o).InitCustomizationController();
            }

            internal void <.cctor>b__56_8(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ILayoutControl) o).InitCustomizationController();
            }

            internal void <.cctor>b__56_9(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.LayoutControl.LayoutControl) o).SetItemSizerStyle((Style) e.NewValue);
            }

            internal bool <OnAvailableItemsChanged>b__159_0(FrameworkElement item) => 
                item.GetVisible();
        }


        internal class ItemsContainer : PanelBase
        {
            protected static DependencyProperty ChildVisibilityListener = RegisterChildPropertyListener("Visibility", typeof(DevExpress.Xpf.LayoutControl.LayoutControl.ItemsContainer));

            protected override void OnChildPropertyChanged(FrameworkElement child, DependencyProperty propertyListener, object oldValue, object newValue)
            {
                base.OnChildPropertyChanged(child, propertyListener, oldValue, newValue);
                if (ReferenceEquals(propertyListener, ChildVisibilityListener) && ((newValue != null) && !newValue.Equals(oldValue)))
                {
                    this.OnChildVisibilityChanged(child);
                }
            }

            protected virtual void OnChildVisibilityChanged(FrameworkElement child)
            {
                if (this.ChildVisibilityChanged != null)
                {
                    this.ChildVisibilityChanged(child);
                }
            }

            protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
            {
                base.OnVisualChildrenChanged(visualAdded, visualRemoved);
                if (visualAdded is FrameworkElement)
                {
                    AttachChildPropertyListener((FrameworkElement) visualAdded, "Visibility", ChildVisibilityListener);
                }
                if (visualRemoved is FrameworkElement)
                {
                    DetachChildPropertyListener((FrameworkElement) visualRemoved, ChildVisibilityListener);
                }
            }

            public Action<FrameworkElement> ChildVisibilityChanged { get; set; }
        }
    }
}

