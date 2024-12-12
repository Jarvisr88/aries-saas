namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    [DXToolboxBrowsable(false), TemplatePart(Name="PART_DocumentSelectorPreview", Type=typeof(DocumentSelectorPreview)), TemplatePart(Name="PART_PanelsListBox", Type=typeof(DocumentSelectorListBox)), TemplatePart(Name="PART_DocumentsListBox", Type=typeof(DocumentSelectorListBox)), TemplatePart(Name="PART_PanelsListBoxCaption", Type=typeof(UIElement)), TemplatePart(Name="PART_DocumentsListBoxCaption", Type=typeof(UIElement))]
    public class DocumentSelector : psvControl, IControlHost
    {
        public static readonly DependencyProperty PanelIndexProperty;
        public static readonly DependencyProperty DocumentIndexProperty;
        public static readonly DependencyProperty PanelsCaptionProperty;
        public static readonly DependencyProperty DocumentsCaptionProperty;
        private static readonly DependencyPropertyKey SelectedItemPropertyKey;
        public static readonly DependencyProperty SelectedItemProperty;
        public static readonly DependencyProperty BindableDocumentIndexProperty;
        public static readonly DependencyProperty BindablePanelIndexProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsActiveProperty;
        private ObservableCollection<LayoutPanel> selectedItemsCore;
        private int selectedItemLock;
        private bool keyLeftCtrl;
        private bool keyRightCtrl;
        private readonly Locker closeLocker = new Locker();
        private int lockDocumentIndex;
        private int lockPanelIndex;

        static DocumentSelector()
        {
            DependencyPropertyRegistrator<DocumentSelector> registrator = new DependencyPropertyRegistrator<DocumentSelector>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<string>("PanelsCaption", ref PanelsCaptionProperty, string.Empty, null, null);
            registrator.Register<string>("DocumentsCaption", ref DocumentsCaptionProperty, string.Empty, null, null);
            registrator.Register<int>("PanelIndex", ref PanelIndexProperty, -1, (dObj, e) => ((DocumentSelector) dObj).OnPanelIndexChanged(), (dObj, value) => ((DocumentSelector) dObj).CoercePanelIndex(value));
            registrator.Register<int>("DocumentIndex", ref DocumentIndexProperty, -1, (dObj, e) => ((DocumentSelector) dObj).OnDocumentIndexChanged(), (dObj, value) => ((DocumentSelector) dObj).CoerceDocumentIndex(value));
            registrator.RegisterReadonly<LayoutPanel>("SelectedItem", ref SelectedItemPropertyKey, ref SelectedItemProperty, null, (dObj, e) => ((DocumentSelector) dObj).OnSelectedItemChanged((LayoutPanel) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((DocumentSelector) dObj).CoerceSelectedItem((LayoutPanel) value)));
            registrator.Register<int>("BindableDocumentIndex", ref BindableDocumentIndexProperty, -1, (dObj, e) => ((DocumentSelector) dObj).OnBindableDocumentIndexChanged((int) e.OldValue, (int) e.NewValue), null);
            registrator.Register<int>("BindablePanelIndex", ref BindablePanelIndexProperty, -1, (dObj, e) => ((DocumentSelector) dObj).OnBindablePanelIndexChanged((int) e.OldValue, (int) e.NewValue), null);
            registrator.Register<bool>("IsActive", ref IsActiveProperty, false, (dObj, e) => ((DocumentSelector) dObj).OnIsActiveChanged((bool) e.OldValue, (bool) e.NewValue), null);
        }

        public DocumentSelector()
        {
            this.Panels = new ObservableCollection<LayoutPanel>();
            this.Documents = new ObservableCollection<LayoutPanel>();
            this.Documents.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnDocumentsCollectionChanged);
            this.Panels.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnPanelsCollectionChanged);
            this.SelectedItems = this.Documents;
        }

        protected void AdjustDocumentIndex(int index)
        {
            if (this.Documents.Count != 0)
            {
                this.DocumentIndex = index;
            }
        }

        protected void AdjustIndex(bool isPanel, int index)
        {
            if (isPanel)
            {
                this.AdjustDocumentIndex(index);
            }
            else
            {
                this.AdjustPanelIndex(index);
            }
        }

        protected void AdjustPanelIndex(int index)
        {
            if (this.Panels.Count != 0)
            {
                this.PanelIndex = index;
            }
        }

        protected int CalcIndex(bool isPanel, int baseValue)
        {
            int num = isPanel ? this.Panels.Count : this.Documents.Count;
            return ((baseValue < num) ? ((baseValue >= 0) ? baseValue : this.CalcIndexLess(isPanel)) : this.CalcIndexGreaterOrEqual(isPanel));
        }

        protected int CalcIndexGreaterOrEqual(bool isPanelIndex)
        {
            int num = this.IsCollectionChanging ? 0 : -1;
            if (num == -1)
            {
                this.AdjustIndex(isPanelIndex, 0);
            }
            return num;
        }

        protected int CalcIndexLess(bool isPanel)
        {
            int num = isPanel ? (this.Panels.Count - 1) : (this.Documents.Count - 1);
            int num2 = this.IsCollectionChanging ? num : -1;
            if (num2 == -1)
            {
                this.AdjustIndex(isPanel, isPanel ? (this.Documents.Count - 1) : (this.Panels.Count - 1));
            }
            return num2;
        }

        protected bool CalcIsCollectionChanging(bool isChanging) => 
            (isChanging || !this.NotActiveItemsAreEmpty()) ? isChanging : true;

        public void Close()
        {
            this.CloseContainer();
        }

        private void CloseContainer()
        {
            if (!this.closeLocker)
            {
                using (this.closeLocker.Lock())
                {
                    BaseLayoutItem selected = this.SelectedItem;
                    base.ClearValue(DocumentIndexProperty);
                    base.ClearValue(PanelIndexProperty);
                    if (this.PartDocumentSelectorPreview != null)
                    {
                        this.PartDocumentSelectorPreview.OnClosing();
                    }
                    if (this.FloatingContainer != null)
                    {
                        this.FloatingContainer.IsOpen = false;
                        base.Container.RemoveFromLogicalTree(this.FloatingContainer, this);
                    }
                    this.Documents.Clear();
                    this.Panels.Clear();
                    if (!this.IsSelectedItemLocked)
                    {
                        base.Dispatcher.BeginInvoke(delegate {
                            this.Container.Activate(selected);
                            this.Container.Update();
                        }, new object[0]);
                    }
                    base.ClearValue(IsActiveProperty);
                }
            }
        }

        protected object CoerceDocumentIndex(object baseValue) => 
            this.CoerceIndexCore(false, (int) baseValue);

        protected int CoerceIndexCore(bool isPanel, int baseValue) => 
            ((this.PanelIndex == -1) || (this.DocumentIndex == -1)) ? this.CalcIndex(isPanel, baseValue) : -1;

        protected object CoercePanelIndex(object baseValue) => 
            this.CoerceIndexCore(true, (int) baseValue);

        protected virtual LayoutPanel CoerceSelectedItem(LayoutPanel value) => 
            ((this.SelectedItems == null) || ((this.SelectedItems.Count == 0) || (this.SelectedIndex == -1))) ? null : this.SelectedItems[this.SelectedIndex];

        protected void CollectItemsByType(BaseLayoutItem[] items)
        {
            foreach (BaseLayoutItem item in items)
            {
                LayoutPanel panel = item as LayoutPanel;
                if ((panel != null) && (!panel.IsClosed && (panel.IsVisibleCore && panel.ShowInDocumentSelector)))
                {
                    if (panel.ItemType == LayoutItemType.Document)
                    {
                        this.Documents.Add(panel);
                    }
                    else if (panel.ItemType == LayoutItemType.Panel)
                    {
                        this.Panels.Add(panel);
                    }
                }
            }
        }

        public FrameworkElement[] GetChildren() => 
            new FrameworkElement[] { this.PartPanelsListBox, this.PartDocumentsListBox, this.PartDocumentSelectorPreview };

        private static Visibility GetVisibility(ICollection collection) => 
            (collection.Count > 0) ? Visibility.Visible : Visibility.Collapsed;

        protected internal void InitializeItems(DockLayoutManager manager)
        {
            this.InitializeItemsCore(manager);
        }

        private void InitializeItemsCore(DockLayoutManager manager)
        {
            this.Panels.Clear();
            this.Documents.Clear();
            this.CollectItemsByType(manager.GetItems());
        }

        protected bool NotActiveDocumentsAreEmpty() => 
            ReferenceEquals(this.SelectedItems, this.Panels) && (this.Documents.Count == 0);

        protected bool NotActiveItemsAreEmpty() => 
            this.NotActivePanelsAreEmpty() || this.NotActiveDocumentsAreEmpty();

        protected bool NotActivePanelsAreEmpty() => 
            ReferenceEquals(this.SelectedItems, this.Documents) && (this.Panels.Count == 0);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.keyLeftCtrl = Keyboard.IsKeyDown(Key.LeftCtrl);
            this.keyRightCtrl = Keyboard.IsKeyDown(Key.RightCtrl);
            this.FloatingContainer = DevExpress.Xpf.Core.FloatingContainer.GetFloatingContainer(this);
            this.PartDocumentSelectorPreview = base.GetTemplateChild("PART_DocumentSelectorPreview") as DocumentSelectorPreview;
            this.PartPanelsListBox = base.GetTemplateChild("PART_PanelsListBox") as DocumentSelectorListBox;
            if (this.PartPanelsListBox != null)
            {
                this.PartPanelsListBox.MouseLeftButtonUp += new MouseButtonEventHandler(this.SelectorListBox_MouseLeftButtonUp);
            }
            this.PartDocumentsListBox = base.GetTemplateChild("PART_DocumentsListBox") as DocumentSelectorListBox;
            if (this.PartDocumentsListBox != null)
            {
                this.PartDocumentsListBox.MouseLeftButtonUp += new MouseButtonEventHandler(this.SelectorListBox_MouseLeftButtonUp);
            }
            this.PartPanelsListBoxCaption = base.GetTemplateChild("PART_PanelsListBoxCaption") as UIElement;
            this.PartDocumentsListBoxCaption = base.GetTemplateChild("PART_DocumentsListBoxCaption") as UIElement;
            this.UpdateDocumentsVisibility();
            this.UpdatePanelsVisibility();
            this.SelectContent();
        }

        protected virtual void OnBindableDocumentIndexChanged(int oldValue, int newValue)
        {
            if (!this.IsDocumentIndexLocked && (newValue != -1))
            {
                this.DocumentIndex = newValue;
            }
        }

        protected virtual void OnBindablePanelIndexChanged(int oldValue, int newValue)
        {
            if (!this.IsPanelIndexLocked && (newValue != -1))
            {
                this.PanelIndex = newValue;
            }
        }

        protected override void OnDispose()
        {
            this.Documents.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnDocumentsCollectionChanged);
            this.Panels.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnPanelsCollectionChanged);
            if (this.PartPanelsListBox != null)
            {
                this.PartPanelsListBox.MouseLeftButtonUp -= new MouseButtonEventHandler(this.SelectorListBox_MouseLeftButtonUp);
            }
            if (this.PartDocumentsListBox != null)
            {
                this.PartDocumentsListBox.MouseLeftButtonUp -= new MouseButtonEventHandler(this.SelectorListBox_MouseLeftButtonUp);
            }
            base.OnDispose();
        }

        protected void OnDocumentIndexChanged()
        {
            this.OnIndexPropertyChangedCore(false);
            try
            {
                this.lockDocumentIndex++;
                this.BindableDocumentIndex = this.DocumentIndex;
            }
            finally
            {
                this.lockDocumentIndex--;
            }
        }

        private void OnDocumentsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateDocumentsVisibility();
        }

        protected void OnIndexPropertyChangedCore(bool isPanelIndex)
        {
            DependencyProperty dp = null;
            if ((this.PanelIndex != -1) || (this.DocumentIndex != -1))
            {
                if (isPanelIndex)
                {
                    this.SelectedItems = (this.PanelIndex == -1) ? this.Documents : this.Panels;
                    dp = DocumentIndexProperty;
                }
                else
                {
                    this.SelectedItems = (this.DocumentIndex == -1) ? this.Panels : this.Documents;
                    dp = PanelIndexProperty;
                }
            }
            if ((this.PanelIndex != -1) && ((this.DocumentIndex != -1) && (dp != null)))
            {
                base.CoerceValue(dp);
            }
            base.CoerceValue(SelectedItemProperty);
            this.SelectContent();
        }

        protected virtual void OnIsActiveChanged(bool oldValue, bool newValue)
        {
            try
            {
                this.selectedItemLock++;
                if (!newValue)
                {
                    this.CloseContainer();
                }
            }
            finally
            {
                this.selectedItemLock--;
            }
        }

        protected override void OnLoaded()
        {
            FrameworkElement visualContainer = this.VisualContainer as FrameworkElement;
            if (visualContainer != null)
            {
                visualContainer.PreviewKeyDown -= new KeyEventHandler(this.OnVisualContainerPreviewKeyDown);
                visualContainer.PreviewKeyDown -= new KeyEventHandler(this.OnVisualContainerPreviewKeyUp);
            }
            base.ClearValue(IsActiveProperty);
            this.VisualContainer = LayoutHelper.FindRoot(this, false);
            if (this.VisualContainer != null)
            {
                DockLayoutManager.SetLayoutItem(this.VisualContainer, this.SelectedItem);
                visualContainer = this.VisualContainer as FrameworkElement;
                if (visualContainer != null)
                {
                    visualContainer.PreviewKeyDown += new KeyEventHandler(this.OnVisualContainerPreviewKeyDown);
                    visualContainer.PreviewKeyUp += new KeyEventHandler(this.OnVisualContainerPreviewKeyUp);
                }
                Binding binding = new Binding();
                binding.Path = new PropertyPath(DevExpress.Xpf.Core.FloatingContainer.IsActiveProperty);
                binding.Source = this.VisualContainer;
                binding.Mode = BindingMode.OneWay;
                base.SetBinding(IsActiveProperty, binding);
            }
            DockLayoutManager.SetLayoutItem(this, this.SelectedItem);
        }

        protected void OnPanelIndexChanged()
        {
            this.OnIndexPropertyChangedCore(true);
            try
            {
                this.lockPanelIndex++;
                this.BindablePanelIndex = this.PanelIndex;
            }
            finally
            {
                this.lockPanelIndex--;
            }
        }

        private void OnPanelsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdatePanelsVisibility();
        }

        private void OnPreviewKeyDownCore(KeyEventArgs e)
        {
            this.keyLeftCtrl = Keyboard.IsKeyDown(Key.LeftCtrl);
            this.keyRightCtrl = Keyboard.IsKeyDown(Key.RightCtrl);
            if ((e.Key == Key.Tab) && ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) == (ModifierKeys.Shift | ModifierKeys.Control)))
            {
                this.SelectPrevItemWrap();
            }
            else if ((e.Key == Key.Tab) && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
            {
                this.SelectNextItemWrap();
            }
            else if (e.Key == Key.Up)
            {
                this.SelectPrevItem();
            }
            else if (e.Key == Key.Down)
            {
                this.SelectNextItem();
            }
            else if ((e.Key == Key.Left) || (e.Key == Key.Right))
            {
                this.ToggleActiveItems();
            }
        }

        private void OnPreviewKeyUpCore(KeyEventArgs e)
        {
            if (((e.Key == Key.LeftCtrl) && this.keyLeftCtrl) || ((e.Key == Key.RightCtrl) && this.keyRightCtrl))
            {
                this.CloseContainer();
            }
        }

        protected virtual void OnSelectedItemChanged(LayoutPanel value)
        {
            if (this.VisualContainer != null)
            {
                DockLayoutManager.SetLayoutItem(this.VisualContainer, value);
            }
            DockLayoutManager.SetLayoutItem(this, value);
        }

        private void OnVisualContainerPreviewKeyDown(object sender, KeyEventArgs e)
        {
            this.OnPreviewKeyDownCore(e);
        }

        private void OnVisualContainerPreviewKeyUp(object sender, KeyEventArgs e)
        {
            this.OnPreviewKeyUpCore(e);
        }

        private void SelectActiveItem()
        {
            DockLayoutManager manager = base.Container ?? DockLayoutManager.Ensure(this, false);
            if ((this.Panels.Count != 0) || (this.Documents.Count != 0))
            {
                if (this.Documents.Count > 0)
                {
                    DocumentPanel activeDockItem = manager.ActiveDockItem as DocumentPanel;
                    this.SelectedIndex = (activeDockItem != null) ? this.Documents.IndexOf(activeDockItem) : 0;
                }
                else
                {
                    if (this.Panels.Count > 0)
                    {
                        LayoutPanel activeDockItem = manager.ActiveDockItem as LayoutPanel;
                        if (activeDockItem != null)
                        {
                            this.SelectedIndex = this.Panels.IndexOf(activeDockItem);
                            return;
                        }
                    }
                    this.SelectedIndex = 0;
                }
            }
        }

        protected void SelectActiveItems(ObservableCollection<LayoutPanel> activeItems)
        {
            if (!ReferenceEquals(this.SelectedItems, activeItems) && (activeItems.Count != 0))
            {
                this.SelectActiveItemsIndex(activeItems);
                this.SelectedItems = activeItems;
            }
        }

        protected void SelectActiveItemsIndex(ObservableCollection<LayoutPanel> activeItems)
        {
            if (ReferenceEquals(activeItems, this.Documents))
            {
                if (this.PanelIndex >= this.Documents.Count)
                {
                    this.PanelIndex = this.Documents.Count - 1;
                }
                this.DocumentIndex = this.PanelIndex;
            }
            else
            {
                if (this.DocumentIndex >= this.Panels.Count)
                {
                    this.DocumentIndex = this.Panels.Count - 1;
                }
                this.PanelIndex = this.DocumentIndex;
            }
        }

        protected void SelectContent()
        {
            if (this.PartDocumentSelectorPreview != null)
            {
                this.PartDocumentSelectorPreview.Target = this.SelectedItem;
            }
        }

        protected internal void SelectItemCore(bool changeCollection, bool isNext)
        {
            if (this.SelectedItems.Count == 0)
            {
                this.SelectedIndex = -1;
            }
            else
            {
                this.IsCollectionChanging = this.CalcIsCollectionChanging(changeCollection);
                this.SelectedIndex += isNext ? 1 : -1;
            }
        }

        public void SelectNextItem()
        {
            this.SelectItemCore(false, true);
        }

        public void SelectNextItemWrap()
        {
            this.SelectItemCore(true, true);
        }

        private void SelectorListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.CloseContainer();
        }

        public void SelectPrevItem()
        {
            this.SelectItemCore(false, false);
        }

        public void SelectPrevItemWrap()
        {
            this.SelectItemCore(true, false);
        }

        protected internal void SetSelectedItem()
        {
            this.SelectedItems = (this.Documents.Count > 0) ? this.Documents : this.Panels;
            this.SelectActiveItem();
            if (KeyHelper.IsShiftPressed)
            {
                this.SelectPrevItemWrap();
            }
            else
            {
                this.SelectNextItemWrap();
            }
        }

        protected void ToggleActiveCollectionCore()
        {
            this.IsCollectionChanging = false;
            this.SelectActiveItems(ReferenceEquals(this.SelectedItems, this.Panels) ? this.Documents : this.Panels);
            this.SelectContent();
        }

        public void ToggleActiveItems()
        {
            this.ToggleActiveCollectionCore();
        }

        protected void UpdateDocumentsVisibility()
        {
            if (this.PartDocumentsListBox != null)
            {
                this.PartDocumentsListBox.Visibility = GetVisibility(this.Documents);
            }
            if (this.PartDocumentsListBoxCaption != null)
            {
                this.PartDocumentsListBoxCaption.Visibility = GetVisibility(this.Documents);
            }
        }

        private void UpdatePanelsVisibility()
        {
            if (this.PartPanelsListBoxCaption != null)
            {
                this.PartPanelsListBoxCaption.Visibility = GetVisibility(this.Panels);
            }
            if (this.PartPanelsListBox != null)
            {
                this.PartPanelsListBox.Visibility = GetVisibility(this.Panels);
            }
        }

        public int SelectedIndex
        {
            get => 
                ReferenceEquals(this.SelectedItems, this.Panels) ? this.PanelIndex : this.DocumentIndex;
            internal set
            {
                if (ReferenceEquals(this.SelectedItems, this.Panels))
                {
                    this.PanelIndex = value;
                }
                else
                {
                    this.DocumentIndex = value;
                }
            }
        }

        public int BindableDocumentIndex
        {
            get => 
                (int) base.GetValue(BindableDocumentIndexProperty);
            set => 
                base.SetValue(BindableDocumentIndexProperty, value);
        }

        public int BindablePanelIndex
        {
            get => 
                (int) base.GetValue(BindablePanelIndexProperty);
            set => 
                base.SetValue(BindablePanelIndexProperty, value);
        }

        public int PanelIndex
        {
            get => 
                (int) base.GetValue(PanelIndexProperty);
            set => 
                base.SetValue(PanelIndexProperty, value);
        }

        public int DocumentIndex
        {
            get => 
                (int) base.GetValue(DocumentIndexProperty);
            set => 
                base.SetValue(DocumentIndexProperty, value);
        }

        public string PanelsCaption
        {
            get => 
                (string) base.GetValue(PanelsCaptionProperty);
            set => 
                base.SetValue(PanelsCaptionProperty, value);
        }

        public string DocumentsCaption
        {
            get => 
                (string) base.GetValue(DocumentsCaptionProperty);
            set => 
                base.SetValue(DocumentsCaptionProperty, value);
        }

        public LayoutPanel SelectedItem =>
            (LayoutPanel) base.GetValue(SelectedItemProperty);

        public bool HasItemsToShow =>
            (this.Panels.Count + this.Documents.Count) > 1;

        public ObservableCollection<LayoutPanel> SelectedItems
        {
            get => 
                this.selectedItemsCore;
            internal set => 
                this.selectedItemsCore = value;
        }

        public ObservableCollection<LayoutPanel> Panels { get; internal set; }

        public ObservableCollection<LayoutPanel> Documents { get; internal set; }

        public DocumentSelectorPreview PartDocumentSelectorPreview { get; private set; }

        public DocumentSelectorListBox PartPanelsListBox { get; private set; }

        public DocumentSelectorListBox PartDocumentsListBox { get; private set; }

        public UIElement PartPanelsListBoxCaption { get; private set; }

        public UIElement PartDocumentsListBoxCaption { get; private set; }

        protected internal bool IsCollectionChanging { get; set; }

        protected DevExpress.Xpf.Core.FloatingContainer FloatingContainer { get; set; }

        protected DependencyObject VisualContainer { get; set; }

        private bool IsSelectedItemLocked =>
            this.selectedItemLock > 0;

        private bool IsDocumentIndexLocked =>
            this.lockDocumentIndex > 0;

        private bool IsPanelIndexLocked =>
            this.lockPanelIndex > 0;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentSelector.<>c <>9 = new DocumentSelector.<>c();

            internal void <.cctor>b__9_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentSelector) dObj).OnPanelIndexChanged();
            }

            internal object <.cctor>b__9_1(DependencyObject dObj, object value) => 
                ((DocumentSelector) dObj).CoercePanelIndex(value);

            internal void <.cctor>b__9_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentSelector) dObj).OnDocumentIndexChanged();
            }

            internal object <.cctor>b__9_3(DependencyObject dObj, object value) => 
                ((DocumentSelector) dObj).CoerceDocumentIndex(value);

            internal void <.cctor>b__9_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentSelector) dObj).OnSelectedItemChanged((LayoutPanel) e.NewValue);
            }

            internal object <.cctor>b__9_5(DependencyObject dObj, object value) => 
                ((DocumentSelector) dObj).CoerceSelectedItem((LayoutPanel) value);

            internal void <.cctor>b__9_6(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentSelector) dObj).OnBindableDocumentIndexChanged((int) e.OldValue, (int) e.NewValue);
            }

            internal void <.cctor>b__9_7(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentSelector) dObj).OnBindablePanelIndexChanged((int) e.OldValue, (int) e.NewValue);
            }

            internal void <.cctor>b__9_8(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((DocumentSelector) dObj).OnIsActiveChanged((bool) e.OldValue, (bool) e.NewValue);
            }
        }
    }
}

