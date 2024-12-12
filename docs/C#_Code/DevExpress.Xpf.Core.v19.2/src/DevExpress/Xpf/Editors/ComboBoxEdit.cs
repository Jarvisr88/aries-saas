namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Design.DataAccess;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Input;
    using System.Windows.Markup;

    [DataAccessMetadata("All", SupportedProcessingModes="GridLookup", EnableBindingToEnum=true), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free), ContentProperty("Items")]
    public class ComboBoxEdit : LookUpEditBase, ISelectorEdit, IBaseEdit, IInputElement
    {
        public static readonly DependencyProperty ShowCustomItemsProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        protected internal static readonly DependencyProperty ApplyImageTemplateToSelectedItemProperty;
        public static readonly DependencyProperty ScrollUnitProperty;
        public static readonly DependencyProperty ShowPopupIfItemsSourceEmptyProperty;

        event RoutedEventHandler IBaseEdit.Loaded
        {
            add
            {
                base.Loaded += value;
            }
            remove
            {
                base.Loaded -= value;
            }
        }

        event RoutedEventHandler IBaseEdit.Unloaded
        {
            add
            {
                base.Unloaded += value;
            }
            remove
            {
                base.Unloaded -= value;
            }
        }

        static ComboBoxEdit()
        {
            Type forType = typeof(ComboBoxEdit);
            PopupBaseEdit.PopupMaxHeightProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3.0));
            ShowCustomItemsProperty = DependencyPropertyManager.Register("ShowCustomItems", typeof(bool?), forType, new FrameworkPropertyMetadata(null, (d, e) => ((ComboBoxEdit) d).ShowCustomItemsChanged((bool?) e.NewValue)));
            ApplyImageTemplateToSelectedItemProperty = DependencyPropertyManager.Register("ApplyImageTemplateToSelectedItem", typeof(bool), forType, new UIPropertyMetadata(false));
            ScrollUnitProperty = DependencyPropertyManager.Register("ScrollUnit", typeof(DevExpress.Xpf.Editors.ScrollUnit), forType, new PropertyMetadata(DevExpress.Xpf.Editors.ScrollUnit.Pixel));
            ShowPopupIfItemsSourceEmptyProperty = DependencyPropertyManager.Register("ShowPopupIfItemsSourceEmpty", typeof(bool), forType, new FrameworkPropertyMetadata(false));
        }

        public ComboBoxEdit()
        {
            this.SetDefaultStyleKey(typeof(ComboBoxEdit));
        }

        protected override bool CanShowPopupCore() => 
            this.ShowPopupIfItemsSourceEmpty || base.CanShowPopupCore();

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new ComboBoxEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new ComboBoxEditStrategy(this);

        protected internal override BaseEditStyleSettings CreateStyleSettings() => 
            new ComboBoxStyleSettings();

        protected override VisualClientOwner CreateVisualClient() => 
            new ListBoxVisualClientOwner(this);

        void IBaseEdit.ClearValue(DependencyProperty dp)
        {
            base.ClearValue(dp);
        }

        object IBaseEdit.GetValue(DependencyProperty d) => 
            base.GetValue(d);

        protected override void ItemsSourceChanged(object itemsSource)
        {
            base.ItemsSourceChanged(itemsSource);
            this.ApplyImageTemplateToSelectedItem = EnumItemsSource.IsEnumItemsSource(itemsSource);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new ComboBoxEditAutomationPeer(this);

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (base.IsPopupOpen)
            {
                Action<PopupListBox> action = <>c.<>9__33_0;
                if (<>c.<>9__33_0 == null)
                {
                    Action<PopupListBox> local1 = <>c.<>9__33_0;
                    action = <>c.<>9__33_0 = x => x.SetEditBoxMousePosition();
                }
                (this.ListBox as PopupListBox).Do<PopupListBox>(action);
            }
        }

        protected virtual void ShowCustomItemsChanged(bool? value)
        {
            this.EditStrategy.ShowCustomItemsChanged(value);
        }

        public bool ShowPopupIfItemsSourceEmpty
        {
            get => 
                (bool) base.GetValue(ShowPopupIfItemsSourceEmptyProperty);
            set => 
                base.SetValue(ShowPopupIfItemsSourceEmptyProperty, value);
        }

        [Category("Common Properties"), Description("Provides access to the collection of items when the editor is not bound to a data source."), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ListItemCollection Items =>
            this.Settings.Items;

        public ObservableCollection<System.Windows.Controls.GroupStyle> GroupStyle =>
            this.Settings.GroupStyle;

        [Category("Behavior")]
        public bool? ShowCustomItems
        {
            get => 
                (bool?) base.GetValue(ShowCustomItemsProperty);
            set => 
                base.SetValue(ShowCustomItemsProperty, value);
        }

        public DevExpress.Xpf.Editors.ScrollUnit ScrollUnit
        {
            get => 
                (DevExpress.Xpf.Editors.ScrollUnit) base.GetValue(ScrollUnitProperty);
            set => 
                base.SetValue(ScrollUnitProperty, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ApplyImageTemplateToSelectedItem
        {
            get => 
                (bool) base.GetValue(ApplyImageTemplateToSelectedItemProperty);
            protected internal set => 
                base.SetValue(ApplyImageTemplateToSelectedItemProperty, value);
        }

        protected internal ComboBoxEditSettings Settings =>
            base.Settings as ComboBoxEditSettings;

        protected internal SelectorVisualClientOwner VisualClient =>
            base.VisualClient;

        public override FrameworkElement PopupElement =>
            this.ListBox as FrameworkElement;

        protected internal ISelectorEditInnerListBox ListBox =>
            this.EditStrategy.GetVisualClient().InnerEditor as ISelectorEditInnerListBox;

        ObservableCollection<System.Windows.Controls.GroupStyle> ISelectorEdit.GroupStyle =>
            this.GroupStyle;

        SelectionEventMode ISelectorEdit.SelectionEventMode =>
            SelectionEventMode.MouseDown;

        ISelectionProvider ISelectorEdit.SelectionProvider =>
            new SelectionProvider(this.ListBox);

        ListItemCollection ISelectorEdit.Items =>
            this.Items;

        internal ComboBoxEditStrategy EditStrategy
        {
            get => 
                base.EditStrategy as ComboBoxEditStrategy;
            set => 
                base.EditStrategy = value;
        }

        object IBaseEdit.DataContext
        {
            get => 
                base.DataContext;
            set => 
                base.DataContext = value;
        }

        HorizontalAlignment IBaseEdit.HorizontalContentAlignment
        {
            get => 
                base.HorizontalContentAlignment;
            set => 
                base.HorizontalContentAlignment = value;
        }

        VerticalAlignment IBaseEdit.VerticalContentAlignment
        {
            get => 
                base.VerticalContentAlignment;
            set => 
                base.VerticalContentAlignment = value;
        }

        double IBaseEdit.MaxWidth
        {
            get => 
                base.MaxWidth;
            set => 
                base.MaxWidth = value;
        }

        bool IBaseEdit.ShowEditorButtons
        {
            get => 
                base.ShowEditorButtons;
            set => 
                base.ShowEditorButtons = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ComboBoxEdit.<>c <>9 = new ComboBoxEdit.<>c();
            public static Action<PopupListBox> <>9__33_0;

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ComboBoxEdit) d).ShowCustomItemsChanged((bool?) e.NewValue);
            }

            internal void <OnMouseLeave>b__33_0(PopupListBox x)
            {
                x.SetEditBoxMousePosition();
            }
        }
    }
}

