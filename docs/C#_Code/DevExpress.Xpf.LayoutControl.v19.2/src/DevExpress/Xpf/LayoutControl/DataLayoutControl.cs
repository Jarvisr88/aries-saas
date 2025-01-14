﻿namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class DataLayoutControl : DevExpress.Xpf.LayoutControl.LayoutControl
    {
        public const string GeneratedItemNamePrefix = "li";
        public static readonly DependencyProperty AddColonToItemLabelsProperty = DependencyProperty.Register("AddColonToItemLabels", typeof(bool), typeof(DataLayoutControl), new PropertyMetadata(true));
        public static readonly DependencyProperty AutoGeneratedItemsLocationProperty;
        public static readonly DependencyProperty CurrentItemProperty;
        public static readonly DependencyProperty IsReadOnlyProperty;
        public static readonly DependencyProperty AutoGenerateItemsProperty;
        public static readonly DependencyProperty ItemUpdateSourceTriggerProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ValueProviderProperty;
        private Action currentItemChangedAction;
        private readonly Locker updateLocker = new Locker();

        public event EventHandler<DataLayoutControlAutoGeneratedGroupEventArgs> AutoGeneratedGroup;

        public event EventHandler AutoGeneratedUI;

        public event EventHandler<DataLayoutControlAutoGeneratingItemEventArgs> AutoGeneratingItem;

        public event ValueChangedEventHandler<object> CurrentItemChanged;

        static DataLayoutControl()
        {
            AutoGeneratedItemsLocationProperty = DependencyProperty.Register("AutoGeneratedItemsLocation", typeof(DataLayoutControlAutoGeneratedItemsLocation), typeof(DataLayoutControl), new PropertyMetadata((o, e) => ((DataLayoutControl) o).OnAutoGeneratedItemsLocationChanged()));
            CurrentItemProperty = DependencyProperty.Register("CurrentItem", typeof(object), typeof(DataLayoutControl), new PropertyMetadata((o, e) => ((DataLayoutControl) o).OnCurrentItemChanged(e.OldValue, e.NewValue)));
            IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(DataLayoutControl), new PropertyMetadata((o, e) => ((DataLayoutControl) o).OnIsReadOnlyChanged()));
            AutoGenerateItemsProperty = DependencyProperty.Register("AutoGenerateItems", typeof(bool), typeof(DataLayoutControl), new PropertyMetadata(true, (o, e) => ((DataLayoutControl) o).OnAutoGenerateItemsChanged((bool) e.OldValue, (bool) e.NewValue)));
            ItemUpdateSourceTriggerProperty = DependencyProperty.Register("ItemUpdateSourceTrigger", typeof(UpdateSourceTrigger), typeof(DataLayoutControl), new PropertyMetadata((o, e) => ((DataLayoutControl) o).OnItemUpdateSourceTriggerChanged((UpdateSourceTrigger) e.OldValue, (UpdateSourceTrigger) e.NewValue)));
            ValueProviderProperty = DependencyProperty.Register("ValueProvider", typeof(CurrentValueProvider), typeof(DataLayoutControl));
            LayoutGroup.OrientationProperty.OverrideMetadata(typeof(DataLayoutControl), new PropertyMetadata(Orientation.Vertical));
        }

        public DataLayoutControl()
        {
            this.GeneratedItems = new List<DataLayoutItem>();
            this.ValueProvider = new CurrentValueProvider();
        }

        protected void ClearItem(DataLayoutItem item)
        {
            if (!string.IsNullOrEmpty(item.Name) && this.TryFindItemName(item))
            {
                base.UnregisterName(item.Name);
            }
            item.Name = "";
            item.ClearValue(FrameworkElement.DataContextProperty);
            item.ClearValue(LayoutItem.AddColonToLabelProperty);
            item.DataLayoutControl = null;
        }

        protected void ClearUI()
        {
            base.DeleteChildren();
            base.AvailableItems.BeginUpdate();
            foreach (DataLayoutItem item in this.GeneratedItems)
            {
                base.AvailableItems.Remove(item);
            }
            base.AvailableItems.EndUpdate();
            this.GeneratedItems.ForEach(new Action<DataLayoutItem>(this.ClearItem));
            this.GeneratedItems.Clear();
        }

        private Binding CreateBinding(IEdmPropertyInfo propertyInfo)
        {
            Binding binding;
            PropertyDescriptor property = (propertyInfo != null) ? (propertyInfo.ContextObject as PropertyDescriptor) : null;
            if (property == null)
            {
                binding = new Binding(string.Empty);
            }
            else
            {
                Binding binding2;
                DependencyPropertyDescriptor descriptor2 = DependencyPropertyDescriptor.FromProperty(property);
                if ((descriptor2 == null) || !descriptor2.IsAttached)
                {
                    binding2 = new Binding(propertyInfo.Name);
                }
                else
                {
                    Binding binding1 = new Binding();
                    binding1.Path = new PropertyPath(property);
                    binding2 = binding1;
                }
                binding = binding2;
            }
            binding.UpdateSourceTrigger = this.ItemUpdateSourceTrigger;
            return binding;
        }

        protected override PanelControllerBase CreateController() => 
            new DataLayoutControlController(this);

        protected internal virtual DataLayoutItem CreateItem() => 
            new DataLayoutItem();

        protected virtual LayoutGroupInfo CreateRootGroupInfo() => 
            new LayoutGroupInfo(null, 0, new Orientation?(base.Orientation));

        protected override FrameworkElement FindControlByName(string id) => 
            base.FindControlByName(id) ?? this.GeneratedItems.FirstOrDefault<DataLayoutItem>(x => Equals(x.Name, id));

        protected internal DataLayoutItem GenerateItem(Type valueType, IEdmPropertyInfo propertyInfo, Func<DataLayoutItem> createItem = null)
        {
            Func<Func<DataLayoutItem>, DataLayoutItem> evaluator = <>c.<>9__79_0;
            if (<>c.<>9__79_0 == null)
            {
                Func<Func<DataLayoutItem>, DataLayoutItem> local1 = <>c.<>9__79_0;
                evaluator = <>c.<>9__79_0 = x => x();
            }
            DataLayoutItem item = createItem.Return<Func<DataLayoutItem>, DataLayoutItem>(evaluator, new Func<DataLayoutItem>(this.CreateItem));
            if (propertyInfo != null)
            {
                this.GenerateItemName(item, propertyInfo.Name);
            }
            item.Binding = this.CreateBinding(propertyInfo);
            Binding binding = new Binding {
                Source = this,
                Path = new PropertyPath("ValueProvider.Value", new object[0]),
                Mode = BindingMode.OneWay
            };
            item.SetBinding(FrameworkElement.DataContextProperty, binding);
            Binding binding1 = new Binding();
            binding1.Source = this;
            binding1.Path = new PropertyPath(AddColonToItemLabelsProperty);
            binding1.Mode = BindingMode.OneWay;
            item.SetBinding(LayoutItem.AddColonToLabelProperty, binding1);
            this.OnAutoGeneratingItem(valueType, propertyInfo, ref item);
            if (item != null)
            {
                this.GeneratedItems.Add(item);
                item.DataLayoutControl = this;
            }
            return item;
        }

        protected void GenerateItemName(DataLayoutItem item, string propertyName)
        {
            propertyName = propertyName.Replace(".", "_");
            item.Name = this.GetUniqueName("li" + propertyName);
            if (this.IsInVisualTree() && DevExpress.Xpf.LayoutControl.FrameworkElementExtensions.HasNameScope(this))
            {
                base.RegisterName(item.Name, item);
            }
        }

        protected void GenerateUI()
        {
            if (this.CurrentItemType.IsSimple())
            {
                this.GenerateUIForSimpleType();
            }
            else
            {
                this.GenerateUIForComplexType();
            }
            this.OnAutoGeneratedUI();
        }

        protected virtual void GenerateUIForComplexType()
        {
            base.AvailableItems.BeginUpdate();
            RuntimeEditingContext context = new RuntimeEditingContext(this, null);
            IEnumerable<IEdmPropertyInfo> currentItemTypeVisibleProperties = this.GetCurrentItemTypeVisibleProperties();
            EditorsSource.GenerateEditors(this.CreateRootGroupInfo(), currentItemTypeVisibleProperties, new LayoutGroupGenerator(context.GetRoot(), x => context.CreateModelItem(((LayoutGroup) x.GetCurrentValue()).CreateGroup(), null), x => this.OnAutoGeneratedGroup((LayoutGroup) x.GetCurrentValue()), x => new DataLayoutControlGenerator(this, y => ((LayoutGroup) x.GetCurrentValue()).Children.Add(y))), new DataLayoutControlGenerator(this, x => base.AvailableItems.Add(x)), GenerateEditorOptions.ForLayoutRuntime(), this.AutoGeneratedItemsLocation == DataLayoutControlAutoGeneratedItemsLocation.AvailableItems, true, null, false);
            base.AvailableItems.EndUpdate();
        }

        protected virtual void GenerateUIForSimpleType()
        {
            DataLayoutItem element = this.GenerateItem(this.CurrentItemType, null, null);
            if (element != null)
            {
                if (this.AutoGeneratedItemsLocation == DataLayoutControlAutoGeneratedItemsLocation.Control)
                {
                    base.Children.Add(element);
                }
                else
                {
                    base.AvailableItems.Add(element);
                }
            }
        }

        protected Type GetCurrentItemType(object currentItem) => 
            currentItem?.GetType();

        protected virtual IEnumerable<IEdmPropertyInfo> GetCurrentItemTypeSupportedProperties()
        {
            object metadataLocator = this.ValueProvider.MetadataLocator;
            return ((IEntityProperties) new ReflectionEntityProperties(TypeDescriptor.GetProperties(this.ValueProvider.Value).Cast<PropertyDescriptor>(), this.CurrentItemType, true, metadataLocator as MetadataLocator)).AllProperties;
        }

        protected virtual IEnumerable<IEdmPropertyInfo> GetCurrentItemTypeVisibleProperties() => 
            EditorsGeneratorBase.GetFilteredAndSortedProperties(this.GetCurrentItemTypeSupportedProperties(), false, false, LayoutType.DataForm);

        protected string GetUniqueName(string baseName)
        {
            string name = baseName;
            int num = 0;
            while (base.FindName(name) != null)
            {
                int num2 = num + 1;
                num = num2;
                name = baseName + num2.ToString();
            }
            return name;
        }

        protected virtual void OnAutoGeneratedGroup(LayoutGroup group)
        {
            if (this.AutoGeneratedGroup != null)
            {
                this.AutoGeneratedGroup(this, new DataLayoutControlAutoGeneratedGroupEventArgs(group));
            }
        }

        protected virtual void OnAutoGeneratedItemsLocationChanged()
        {
            this.UpdateUI();
        }

        protected virtual void OnAutoGeneratedUI()
        {
            if (this.AutoGeneratedUI != null)
            {
                this.AutoGeneratedUI(this, EventArgs.Empty);
            }
        }

        protected virtual void OnAutoGenerateItemsChanged(bool oldValue, bool newValue)
        {
            if (this.IsInDesignTool())
            {
                this.ClearUI();
                if (base.Root != null)
                {
                    base.Root.ModelChanged(new LayoutControlModelPropertyChangedEventArgs(this, "AutoGenerateItems", AutoGenerateItemsProperty));
                }
            }
            if (newValue)
            {
                this.UpdateUI();
            }
        }

        protected virtual void OnAutoGeneratingItem(Type valueType, IEdmPropertyInfo property, ref DataLayoutItem item)
        {
            if (this.AutoGeneratingItem != null)
            {
                DataLayoutControlAutoGeneratingItemEventArgs e = new DataLayoutControlAutoGeneratingItemEventArgs(property?.Name, valueType, item);
                this.AutoGeneratingItem(this, e);
                item = e.Item;
                if (e.Cancel)
                {
                    item = null;
                }
            }
        }

        protected virtual void OnCurrentItemChanged(object oldValue, object newValue)
        {
            bool currentItemTypeChanged = this.GetCurrentItemType(newValue) != this.GetCurrentItemType(oldValue);
            if (currentItemTypeChanged && this.AutoGenerateItems)
            {
                this.ClearUI();
            }
            this.ValueProvider.Source = newValue;
            this.currentItemChangedAction = delegate {
                if (currentItemTypeChanged)
                {
                    this.UpdateUI();
                }
                if (this.CurrentItemChanged != null)
                {
                    this.CurrentItemChanged(this, new ValueChangedEventArgs<object>(oldValue, newValue));
                }
            };
            if (base.IsInitialized)
            {
                this.currentItemChangedAction();
                this.currentItemChangedAction = null;
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (this.currentItemChangedAction != null)
            {
                this.currentItemChangedAction();
                this.currentItemChangedAction = null;
            }
        }

        protected virtual void OnIsReadOnlyChanged()
        {
            Action<DataLayoutItem> action = <>c.<>9__96_0;
            if (<>c.<>9__96_0 == null)
            {
                Action<DataLayoutItem> local1 = <>c.<>9__96_0;
                action = <>c.<>9__96_0 = item => item.OnDataLayoutControlIsReadOnlyChanged();
            }
            this.GeneratedItems.ForEach(action);
        }

        protected virtual void OnItemUpdateSourceTriggerChanged(UpdateSourceTrigger oldValue, UpdateSourceTrigger newValue)
        {
            this.UpdateUI();
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            if (this.AutoGenerateItems && (this.IsInDesignTool() && !this.updateLocker))
            {
                UIElement element = visualAdded as UIElement;
                if ((element != null) && !base.IsInternalElement(element))
                {
                    this.UpdateUI();
                }
            }
        }

        private bool TryFindItemName(DataLayoutItem item)
        {
            bool flag;
            try
            {
                flag = base.FindName(item.Name) == item;
            }
            catch
            {
                return false;
            }
            return flag;
        }

        protected void UpdateUI()
        {
            if (this.AutoGenerateItems)
            {
                using (this.updateLocker.Lock())
                {
                    this.ClearUI();
                    if (this.CurrentItem != null)
                    {
                        this.GenerateUI();
                    }
                }
            }
        }

        public static char GroupPathSeparator
        {
            get => 
                LayoutGroupInfoConstants.GroupPathSeparator;
            set => 
                LayoutGroupInfoConstants.GroupPathSeparator = value;
        }

        public static char BorderlessGroupMarkStart
        {
            get => 
                LayoutGroupInfoConstants.BorderlessGroupMarkStart;
            set => 
                LayoutGroupInfoConstants.BorderlessGroupMarkStart = value;
        }

        public static char BorderlessGroupMarkEnd
        {
            get => 
                LayoutGroupInfoConstants.BorderlessGroupMarkEnd;
            set => 
                LayoutGroupInfoConstants.BorderlessGroupMarkEnd = value;
        }

        public static char GroupBoxMarkStart
        {
            get => 
                LayoutGroupInfoConstants.GroupBoxMarkStart;
            set => 
                LayoutGroupInfoConstants.GroupBoxMarkStart = value;
        }

        public static char GroupBoxMarkEnd
        {
            get => 
                LayoutGroupInfoConstants.GroupBoxMarkEnd;
            set => 
                LayoutGroupInfoConstants.GroupBoxMarkEnd = value;
        }

        public static char TabbedGroupMarkStart
        {
            get => 
                LayoutGroupInfoConstants.TabbedGroupMarkStart;
            set => 
                LayoutGroupInfoConstants.TabbedGroupMarkStart = value;
        }

        public static char TabbedGroupMarkEnd
        {
            get => 
                LayoutGroupInfoConstants.TabbedGroupMarkEnd;
            set => 
                LayoutGroupInfoConstants.TabbedGroupMarkEnd = value;
        }

        public static char HorizontalGroupMark
        {
            get => 
                LayoutGroupInfoConstants.HorizontalGroupMark;
            set => 
                LayoutGroupInfoConstants.HorizontalGroupMark = value;
        }

        public static char VerticalGroupMark
        {
            get => 
                LayoutGroupInfoConstants.VerticalGroupMark;
            set => 
                LayoutGroupInfoConstants.VerticalGroupMark = value;
        }

        [Description("Gets or sets whether the colon character is appended to layout item labels.This is a dependency property.")]
        public bool AddColonToItemLabels
        {
            get => 
                (bool) base.GetValue(AddColonToItemLabelsProperty);
            set => 
                base.SetValue(AddColonToItemLabelsProperty, value);
        }

        [Description("Gets or sets the location where layout items are generated by the DataLayoutControl.This is a dependency property.")]
        public DataLayoutControlAutoGeneratedItemsLocation AutoGeneratedItemsLocation
        {
            get => 
                (DataLayoutControlAutoGeneratedItemsLocation) base.GetValue(AutoGeneratedItemsLocationProperty);
            set => 
                base.SetValue(AutoGeneratedItemsLocationProperty, value);
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public DataLayoutControlController Controller =>
            (DataLayoutControlController) base.Controller;

        [Description("Gets or sets the object whose properties are being currently displayed and edited by the DataLayoutControl.This is a dependency property.")]
        public object CurrentItem
        {
            get => 
                base.GetValue(CurrentItemProperty);
            set => 
                base.SetValue(CurrentItemProperty, value);
        }

        [Description("Gets or sets whether layout items' editors are in read-only mode.This is a dependency property.")]
        public bool IsReadOnly
        {
            get => 
                (bool) base.GetValue(IsReadOnlyProperty);
            set => 
                base.SetValue(IsReadOnlyProperty, value);
        }

        [Description("Gets or sets whether the DataLayoutControl should generate layout items automatically. This is a dependency property.")]
        public bool AutoGenerateItems
        {
            get => 
                (bool) base.GetValue(AutoGenerateItemsProperty);
            set => 
                base.SetValue(AutoGenerateItemsProperty, value);
        }

        [Description("Gets or sets a value that determines when the data layout control items are updated.")]
        public UpdateSourceTrigger ItemUpdateSourceTrigger
        {
            get => 
                (UpdateSourceTrigger) base.GetValue(ItemUpdateSourceTriggerProperty);
            set => 
                base.SetValue(ItemUpdateSourceTriggerProperty, value);
        }

        internal CurrentValueProvider ValueProvider
        {
            get => 
                (CurrentValueProvider) base.GetValue(ValueProviderProperty);
            set => 
                base.SetValue(ValueProviderProperty, value);
        }

        protected Type CurrentItemType =>
            this.GetCurrentItemType(this.ValueProvider.Value);

        protected List<DataLayoutItem> GeneratedItems { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataLayoutControl.<>c <>9 = new DataLayoutControl.<>c();
            public static Func<Func<DataLayoutItem>, DataLayoutItem> <>9__79_0;
            public static Action<DataLayoutItem> <>9__96_0;

            internal void <.cctor>b__35_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DataLayoutControl) o).OnAutoGeneratedItemsLocationChanged();
            }

            internal void <.cctor>b__35_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DataLayoutControl) o).OnCurrentItemChanged(e.OldValue, e.NewValue);
            }

            internal void <.cctor>b__35_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DataLayoutControl) o).OnIsReadOnlyChanged();
            }

            internal void <.cctor>b__35_3(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DataLayoutControl) o).OnAutoGenerateItemsChanged((bool) e.OldValue, (bool) e.NewValue);
            }

            internal void <.cctor>b__35_4(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((DataLayoutControl) o).OnItemUpdateSourceTriggerChanged((UpdateSourceTrigger) e.OldValue, (UpdateSourceTrigger) e.NewValue);
            }

            internal DataLayoutItem <GenerateItem>b__79_0(Func<DataLayoutItem> x) => 
                x();

            internal void <OnIsReadOnlyChanged>b__96_0(DataLayoutItem item)
            {
                item.OnDataLayoutControlIsReadOnlyChanged();
            }
        }

        internal class CurrentValueProvider : DependencyObject
        {
            public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(DataLayoutControl.CurrentValueProvider), new PropertyMetadata(null, new PropertyChangedCallback(DataLayoutControl.CurrentValueProvider.OnSourceChanged)));
            public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(DataLayoutControl.CurrentValueProvider));

            protected virtual void OnSourceChanged(object oldValue, object newValue)
            {
                Func<MetadataExtendedSource, object> evaluator = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<MetadataExtendedSource, object> local1 = <>c.<>9__8_0;
                    evaluator = <>c.<>9__8_0 = x => x.Source;
                }
                this.Value = (this.Source as MetadataExtendedSource).Return<MetadataExtendedSource, object>(evaluator, () => this.Source);
            }

            private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                DataLayoutControl.CurrentValueProvider provider = d as DataLayoutControl.CurrentValueProvider;
                if (provider != null)
                {
                    provider.OnSourceChanged(e.OldValue, e.NewValue);
                }
            }

            public object MetadataLocator
            {
                get
                {
                    Func<MetadataExtendedSource, DevExpress.Mvvm.DataAnnotations.MetadataLocator> evaluator = <>c.<>9__1_0;
                    if (<>c.<>9__1_0 == null)
                    {
                        Func<MetadataExtendedSource, DevExpress.Mvvm.DataAnnotations.MetadataLocator> local1 = <>c.<>9__1_0;
                        evaluator = <>c.<>9__1_0 = x => x.MetadataLocator;
                    }
                    return (this.Source as MetadataExtendedSource).Return<MetadataExtendedSource, DevExpress.Mvvm.DataAnnotations.MetadataLocator>(evaluator, (<>c.<>9__1_1 ??= ((Func<DevExpress.Mvvm.DataAnnotations.MetadataLocator>) (() => null))));
                }
            }

            public object Source
            {
                get => 
                    base.GetValue(SourceProperty);
                set => 
                    base.SetValue(SourceProperty, value);
            }

            public object Value
            {
                get => 
                    base.GetValue(ValueProperty);
                set => 
                    base.SetValue(ValueProperty, value);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DataLayoutControl.CurrentValueProvider.<>c <>9 = new DataLayoutControl.CurrentValueProvider.<>c();
                public static Func<MetadataExtendedSource, MetadataLocator> <>9__1_0;
                public static Func<MetadataLocator> <>9__1_1;
                public static Func<MetadataExtendedSource, object> <>9__8_0;

                internal MetadataLocator <get_MetadataLocator>b__1_0(MetadataExtendedSource x) => 
                    x.MetadataLocator;

                internal MetadataLocator <get_MetadataLocator>b__1_1() => 
                    null;

                internal object <OnSourceChanged>b__8_0(MetadataExtendedSource x) => 
                    x.Source;
            }
        }
    }
}

