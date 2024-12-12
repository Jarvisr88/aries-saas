namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars.Automation;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), ContentProperty("Buttons")]
    public class ButtonEdit : TextEdit
    {
        public static readonly DependencyProperty ShowEditorButtonsProperty;
        public static readonly DependencyProperty IsTextEditableProperty;
        public static readonly DependencyProperty AllowDefaultButtonProperty;
        public static readonly DependencyProperty ButtonsProperty;
        protected static readonly DependencyPropertyKey LeftButtonsPropertyKey;
        public static readonly DependencyProperty LeftButtonsProperty;
        protected static readonly DependencyPropertyKey RightButtonsPropertyKey;
        public static readonly DependencyProperty RightButtonsProperty;
        protected static readonly DependencyPropertyKey SortedButtonsPropertyKey;
        public static readonly DependencyProperty SortedButtonsProperty;
        public static readonly DependencyProperty ShowTextProperty;
        public static readonly DependencyProperty NullValueButtonPlacementProperty;
        public static readonly DependencyProperty ShowNullValueButtonOnFocusOnlyProperty;
        public static readonly DependencyProperty ButtonsSourceProperty;
        public static readonly DependencyProperty ButtonTemplateProperty;
        public static readonly DependencyProperty ButtonTemplateSelectorProperty;
        public static readonly DependencyProperty ActualShowLeftButtonsProperty;
        private static readonly DependencyPropertyKey ActualShowLeftButtonsPropertyKey;
        public static readonly DependencyProperty ActualShowRightButtonsProperty;
        private static readonly DependencyPropertyKey ActualShowRightButtonsPropertyKey;
        public static readonly RoutedEvent DefaultButtonClickEvent;
        private IList<ButtonInfoBase> actualButtons;

        [Category("Action")]
        public event RoutedEventHandler DefaultButtonClick
        {
            add
            {
                base.AddHandler(DefaultButtonClickEvent, value);
            }
            remove
            {
                base.RemoveHandler(DefaultButtonClickEvent, value);
            }
        }

        static ButtonEdit()
        {
            Type ownerType = typeof(ButtonEdit);
            ShowEditorButtonsProperty = DependencyPropertyManager.Register("ShowEditorButtons", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(ButtonEdit.OnShowEditButtonsChanged)));
            DefaultButtonClickEvent = EventManager.RegisterRoutedEvent("DefaultButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), ownerType);
            AllowDefaultButtonProperty = DependencyPropertyManager.Register("AllowDefaultButton", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ButtonEdit.OnAllowDefaultButtonChanged)));
            ShowTextProperty = DependencyPropertyManager.Register("ShowText", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            IsTextEditableProperty = DependencyPropertyManager.Register("IsTextEditable", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ButtonEdit.OnIsTextEditableChanged)));
            ButtonsProperty = DependencyPropertyManager.Register("Buttons", typeof(ButtonInfoCollection), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ButtonEdit.OnButtonsChanged)));
            LeftButtonsPropertyKey = DependencyPropertyManager.RegisterReadOnly("LeftButtons", typeof(IEnumerable<ButtonInfoBase>), ownerType, new FrameworkPropertyMetadata(null));
            LeftButtonsProperty = LeftButtonsPropertyKey.DependencyProperty;
            RightButtonsPropertyKey = DependencyPropertyManager.RegisterReadOnly("RightButtons", typeof(IEnumerable<ButtonInfoBase>), ownerType, new FrameworkPropertyMetadata(null));
            RightButtonsProperty = RightButtonsPropertyKey.DependencyProperty;
            SortedButtonsPropertyKey = DependencyPropertyManager.RegisterReadOnly("SortedButtons", typeof(IEnumerable<ButtonInfoBase>), ownerType, new FrameworkPropertyMetadata(null));
            SortedButtonsProperty = SortedButtonsPropertyKey.DependencyProperty;
            NullValueButtonPlacementProperty = DependencyPropertyManager.Register("NullValueButtonPlacement", typeof(EditorPlacement?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((ButtonEdit) d).NullValueButtonPlacementChanged((EditorPlacement?) e.NewValue)));
            ShowNullValueButtonOnFocusOnlyProperty = DependencyProperty.Register("ShowNullValueButtonOnFocusOnly", typeof(bool), ownerType, new PropertyMetadata(false, (o, args) => ((ButtonEdit) o).ShowNullValueButtonOnFocusOnlyChanged((bool) args.NewValue)));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));
            UIElement.IsEnabledProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(true, new PropertyChangedCallback(ButtonEdit.OnIsEnabledChanged)));
            ButtonsSourceProperty = DependencyPropertyManager.Register("ButtonsSource", typeof(IEnumerable), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((ButtonEdit) d).OnButtonsSourceChanged()));
            ButtonTemplateProperty = DependencyPropertyManager.Register("ButtonTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((ButtonEdit) d).OnButtonTemplateChanged()));
            ButtonTemplateSelectorProperty = DependencyPropertyManager.Register("ButtonTemplateSelector", typeof(DataTemplateSelector), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((ButtonEdit) d).OnButtonTemplateSelectorChanged()));
            ActualShowLeftButtonsPropertyKey = DependencyProperty.RegisterReadOnly("ActualShowLeftButtons", typeof(bool), ownerType, new PropertyMetadata(false));
            ActualShowLeftButtonsProperty = ActualShowLeftButtonsPropertyKey.DependencyProperty;
            ActualShowRightButtonsPropertyKey = DependencyProperty.RegisterReadOnly("ActualShowRightButtons", typeof(bool), ownerType, new PropertyMetadata(false));
            ActualShowRightButtonsProperty = ActualShowRightButtonsPropertyKey.DependencyProperty;
        }

        public ButtonEdit()
        {
            this.ButtonsUpdateLocker = new Locker();
            this.ButtonsUpdateLocker.DoLockedAction(new Action(this.EnsureButtons));
        }

        protected virtual void ActualButtonsChanged()
        {
            BarsAutomationHelper.RaiseAutomationStructureChanged(this);
        }

        private void ButtonsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateButtonInfoCollections();
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new ButtonEditPropertyProvider(this);

        private ButtonInfoCollection CreateButtonCollection() => 
            new ButtonInfoCollection();

        protected ButtonInfoBase CreateDefaultButtonInfo() => 
            this.Settings.CreateDefaultButtonInfo();

        protected override EditStrategyBase CreateEditStrategy() => 
            new ButtonEditStrategy(this);

        protected virtual ButtonInfoBase CreateNullValueButtonInfo() => 
            this.Settings.CreateNullValueButtonInfo();

        protected void EnsureButtons()
        {
            base.SetCurrentValue(ButtonsProperty, this.CreateButtonCollection());
            this.actualButtons = new List<ButtonInfoBase>();
        }

        private IList<ButtonInfoBase> GetActualButtons()
        {
            IList<ButtonInfoBase> collection = new List<ButtonInfoBase>();
            this.InsertDefaultButtonInfo(collection);
            this.InsertCommandButtonInfo(collection);
            if (this.Buttons != null)
            {
                foreach (ButtonInfoBase base2 in this.Buttons)
                {
                    collection.Add(base2);
                }
            }
            if (this.ButtonsSource != null)
            {
                foreach (ButtonInfoBase base3 in this.ButtonsSourceButtons)
                {
                    collection.Add(base3);
                }
            }
            Action<ButtonInfoBase> action = <>c.<>9__117_0;
            if (<>c.<>9__117_0 == null)
            {
                Action<ButtonInfoBase> local1 = <>c.<>9__117_0;
                action = <>c.<>9__117_0 = delegate (ButtonInfoBase x) {
                    x.IsFirst = false;
                    x.IsLast = false;
                };
            }
            collection.ForEach<ButtonInfoBase>(action);
            Func<ButtonInfoBase, int> keySelector = <>c.<>9__117_1;
            if (<>c.<>9__117_1 == null)
            {
                Func<ButtonInfoBase, int> local2 = <>c.<>9__117_1;
                keySelector = <>c.<>9__117_1 = x => x.Index;
            }
            return new ReadOnlyCollection<ButtonInfoBase>(collection.OrderBy<ButtonInfoBase, int>(keySelector).ToList<ButtonInfoBase>());
        }

        protected override ControlTemplate GetActualEditorControlTemplate() => 
            !this.PropertyProvider.IsTextEditable ? base.EditNonEditableTemplate : base.GetActualEditorControlTemplate();

        private ButtonInfoBase GetDefaultButtonInfo(IEnumerable<ButtonInfoBase> buttons)
        {
            Func<ButtonInfoBase, bool> predicate = <>c.<>9__107_0;
            if (<>c.<>9__107_0 == null)
            {
                Func<ButtonInfoBase, bool> local1 = <>c.<>9__107_0;
                predicate = <>c.<>9__107_0 = info => info.IsDefaultButton;
            }
            return buttons.FirstOrDefault<ButtonInfoBase>(predicate);
        }

        protected override ControlTemplate GetEditTemplate() => 
            this.PropertyProvider.IsTextEditable ? base.GetEditTemplate() : base.EditNonEditableTemplate;

        protected internal override bool GetShowEditorButtons() => 
            this.ShowEditorButtons;

        protected virtual void InsertCommandButtonInfo(IList<ButtonInfoBase> collection)
        {
            if (this.PropertyProvider.GetNullValueButtonPlacement() == EditorPlacement.EditBox)
            {
                ButtonInfoBase item = this.CreateNullValueButtonInfo();
                collection.Insert(0, item);
            }
        }

        internal void InsertDefaultButtonInfo(IList<ButtonInfoBase> collection)
        {
            if ((this.GetDefaultButtonInfo(collection) == null) && this.PropertyProvider.GetActualAllowDefaultButton(this))
            {
                ButtonInfoBase item = this.CreateDefaultButtonInfo();
                collection.Insert(0, item);
            }
        }

        protected override bool IsTextBlockModeCore() => 
            !this.PropertyProvider.IsTextEditable || base.IsTextBlockModeCore();

        protected virtual void NullValueButtonPlacementChanged(EditorPlacement? newValue)
        {
            this.UpdateButtonInfoCollections();
        }

        protected virtual void OnAllowDefaultButtonChanged()
        {
            this.UpdateButtonInfoCollections();
        }

        private static void OnAllowDefaultButtonChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ButtonEdit) obj).OnAllowDefaultButtonChanged();
        }

        protected virtual void OnButtonsChanged(ButtonInfoCollection oldButtons, ButtonInfoCollection newButtons)
        {
            this.UnsubscribeButtonCollection(oldButtons);
            this.SubscribeButtonCollection(newButtons);
            this.UpdateButtonInfoCollections();
        }

        private static void OnButtonsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ButtonEdit) obj).OnButtonsChanged((ButtonInfoCollection) e.OldValue, (ButtonInfoCollection) e.NewValue);
        }

        protected virtual void OnButtonsSourceChanged()
        {
            this.UpdateButtonsSourceButtons();
            this.UpdateButtonInfoCollections();
        }

        protected virtual void OnButtonTemplateChanged()
        {
            this.UpdateButtonsSourceButtons();
            this.UpdateButtonInfoCollections();
        }

        protected virtual void OnButtonTemplateSelectorChanged()
        {
            this.UpdateButtonsSourceButtons();
            this.UpdateButtonInfoCollections();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new ButtonEditAutomationPeer(this);

        protected internal virtual void OnDefaultButtonClick(object sender, RoutedEventArgs e)
        {
            this.RaiseDefaultButtonClick();
            this.Settings.RaiseDefaultButtonClick(this, e);
        }

        protected internal virtual void OnDefaultRenderButtonClick(RenderEventArgsBase args)
        {
            this.RaiseDefaultButtonClick();
            this.Settings.RaiseDefaultButtonClick(this, args.OriginalEventArgs as RoutedEventArgs);
        }

        protected override void OnEditModeChanged(EditMode oldValue, EditMode newValue)
        {
            base.OnEditModeChanged(oldValue, newValue);
            foreach (ButtonInfoBase base2 in this.ActualButtons)
            {
                base2.UpdateOnEditModeChanged();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.UpdateButtonInfoCollections();
            this.PropertyProvider.SetIsTextEditable(this);
        }

        protected virtual void OnIsEnabledChanged()
        {
            foreach (ButtonInfoBase base2 in this.ActualButtons)
            {
                base2.CoerceValue(ContentElement.IsEnabledProperty);
            }
        }

        private static void OnIsEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ButtonEdit) obj).OnIsEnabledChanged();
        }

        protected virtual void OnIsTextEditableChanged()
        {
            this.Settings.IsTextEditable = this.IsTextEditable;
            this.PropertyProvider.SetIsTextEditable(this);
            base.UpdateActualEditorControlTemplate();
        }

        private static void OnIsTextEditableChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ButtonEdit) obj).OnIsTextEditableChanged();
        }

        protected override void OnShowBorderChanged()
        {
            base.OnShowBorderChanged();
            this.UpdateButtonMargins();
        }

        protected virtual void OnShowEditButtonsChanged(bool oldValue)
        {
            this.UpdateShowEditorButtons();
            base.ContentManagementStrategy.UpdateButtonPanels();
        }

        protected static void OnShowEditButtonsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((ButtonEdit) obj).OnShowEditButtonsChanged((bool) e.OldValue);
        }

        private void ProcessNewButton(ButtonInfoBase info)
        {
            if (base.IsInitialized)
            {
                base.AddLogicalChild(info);
            }
        }

        protected virtual void ProcessNewButtonInternal(ButtonInfoBase info)
        {
        }

        private void ProcessNewButtons(IList items)
        {
            if (items != null)
            {
                foreach (ButtonInfoBase base2 in items)
                {
                    this.ProcessNewButton(base2);
                }
            }
        }

        private void ProcessOldButton(ButtonInfoBase info)
        {
            if (base.IsInitialized)
            {
                base.RemoveLogicalChild(info);
            }
        }

        private void ProcessOldButtons(IList items)
        {
            if (items != null)
            {
                foreach (ButtonInfoBase base2 in items)
                {
                    this.ProcessOldButton(base2);
                }
            }
        }

        private void RaiseDefaultButtonClick()
        {
            base.RaiseEvent(new RoutedEventArgs(DefaultButtonClickEvent));
        }

        protected internal virtual void RenderCheckChanged(bool isChecked)
        {
        }

        protected internal override void SetShowEditorButtons(bool show)
        {
            this.ShowEditorButtons = show;
        }

        private void ShowNullValueButtonOnFocusOnlyChanged(bool value)
        {
            this.EditStrategy.UpdateNullValueButtonPlacement(value);
        }

        private void SubscribeButtonCollection(ButtonInfoCollection newButtons)
        {
            if (newButtons != null)
            {
                newButtons.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ButtonsCollectionChanged);
            }
        }

        private void UnsubscribeButtonCollection(ButtonInfoCollection oldButtons)
        {
            if (oldButtons != null)
            {
                oldButtons.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.ButtonsCollectionChanged);
            }
        }

        private void UpdateActualButtons()
        {
            this.ProcessOldButtons((IList) this.ActualButtons);
            this.actualButtons = this.GetActualButtons();
            this.ProcessNewButtons((IList) this.ActualButtons);
            this.ActualButtonsChanged();
        }

        internal void UpdateButtonInfoCollections()
        {
            this.ButtonsUpdateLocker.DoLockedActionIfNotLocked(new Action(this.UpdateButtonInfoCollectionsInternal));
        }

        private void UpdateButtonInfoCollectionsInternal()
        {
            this.UpdateActualButtons();
            this.UpdateLeftAndRightButtons();
            this.UpdateSortedButtons();
            this.UpdateShowEditorButtons();
            this.UpdateButtonMargins();
        }

        private void UpdateButtonMargins()
        {
            Action<ButtonInfoBase> action = <>c.<>9__155_0;
            if (<>c.<>9__155_0 == null)
            {
                Action<ButtonInfoBase> local1 = <>c.<>9__155_0;
                action = <>c.<>9__155_0 = delegate (ButtonInfoBase x) {
                    x.IsFirst = false;
                    x.IsLast = false;
                };
            }
            this.SortedButtons.ForEach<ButtonInfoBase>(action);
            Action<IEnumerable<ButtonInfoBase>> action2 = <>c.<>9__155_1;
            if (<>c.<>9__155_1 == null)
            {
                Action<IEnumerable<ButtonInfoBase>> local2 = <>c.<>9__155_1;
                action2 = <>c.<>9__155_1 = delegate (IEnumerable<ButtonInfoBase> buttons) {
                    Action<ButtonInfoBase> action1 = <>c.<>9__155_2;
                    if (<>c.<>9__155_2 == null)
                    {
                        Action<ButtonInfoBase> local1 = <>c.<>9__155_2;
                        action1 = <>c.<>9__155_2 = x => x.IsFirst = true;
                    }
                    buttons.FirstOrDefault<ButtonInfoBase>().Do<ButtonInfoBase>(action1);
                };
            }
            this.SortedButtons.Do<IEnumerable<ButtonInfoBase>>(action2);
            Action<IEnumerable<ButtonInfoBase>> action3 = <>c.<>9__155_3;
            if (<>c.<>9__155_3 == null)
            {
                Action<IEnumerable<ButtonInfoBase>> local3 = <>c.<>9__155_3;
                action3 = <>c.<>9__155_3 = delegate (IEnumerable<ButtonInfoBase> buttons) {
                    Action<ButtonInfoBase> action1 = <>c.<>9__155_4;
                    if (<>c.<>9__155_4 == null)
                    {
                        Action<ButtonInfoBase> local1 = <>c.<>9__155_4;
                        action1 = <>c.<>9__155_4 = x => x.IsLast = true;
                    }
                    buttons.LastOrDefault<ButtonInfoBase>().Do<ButtonInfoBase>(action1);
                };
            }
            this.SortedButtons.Do<IEnumerable<ButtonInfoBase>>(action3);
            Action<ButtonInfoBase> action4 = <>c.<>9__155_5;
            if (<>c.<>9__155_5 == null)
            {
                Action<ButtonInfoBase> local4 = <>c.<>9__155_5;
                action4 = <>c.<>9__155_5 = x => x.UpdateActualMargin();
            }
            this.SortedButtons.ForEach<ButtonInfoBase>(action4);
        }

        internal override void UpdateButtonPanelsInplaceMode()
        {
            if (!this.ShowEditorButtons || !this.ShowText)
            {
                if (this.LeftButtonsControl != null)
                {
                    base.additionalInplaceModeElements.Remove(2);
                    base.RemoveVisualChild(this.LeftButtonsControl);
                    this.LeftButtonsControl = null;
                    base.InvalidateMeasure();
                }
                if (this.RightButtonsControl != null)
                {
                    base.additionalInplaceModeElements.Remove(3);
                    base.RemoveVisualChild(this.RightButtonsControl);
                    this.RightButtonsControl = null;
                    base.InvalidateMeasure();
                }
            }
            else
            {
                if (this.LeftButtonsControl == null)
                {
                    this.LeftButtonsControl = new ButtonsControl();
                    base.AddVisualChild(this.LeftButtonsControl);
                    base.additionalInplaceModeElements.Add(2, this.LeftButtonsControl);
                    DockPanel.SetDock(this.LeftButtonsControl, Dock.Left);
                    Binding binding = new Binding(ActualShowLeftButtonsProperty.Name);
                    binding.Source = this;
                    binding.Converter = DevExpress.Xpf.Editors.Helpers.BooleanToVisibilityConverter.Instance;
                    this.LeftButtonsControl.SetBinding(UIElement.VisibilityProperty, binding);
                    Binding binding2 = new Binding(LeftButtonsProperty.Name);
                    binding2.Source = this;
                    this.LeftButtonsControl.SetBinding(ItemsControl.ItemsSourceProperty, binding2);
                    base.InvalidateMeasure();
                }
                if (this.RightButtonsControl == null)
                {
                    this.RightButtonsControl = new ButtonsControl();
                    base.AddVisualChild(this.RightButtonsControl);
                    base.additionalInplaceModeElements.Add(3, this.RightButtonsControl);
                    DockPanel.SetDock(this.RightButtonsControl, Dock.Right);
                    Binding binding = new Binding(ActualShowRightButtonsProperty.Name);
                    binding.Source = this;
                    binding.Converter = DevExpress.Xpf.Editors.Helpers.BooleanToVisibilityConverter.Instance;
                    this.RightButtonsControl.SetBinding(UIElement.VisibilityProperty, binding);
                    Binding binding4 = new Binding(RightButtonsProperty.Name);
                    binding4.Source = this;
                    this.RightButtonsControl.SetBinding(ItemsControl.ItemsSourceProperty, binding4);
                    base.InvalidateMeasure();
                }
            }
        }

        private void UpdateButtonsSourceButtons()
        {
            if (this.ButtonsSource != null)
            {
                this.ButtonsSourceButtons = this.Settings.CreateButtonsSourceButtons(this.ButtonsSource, this.ButtonTemplate, this.ButtonTemplateSelector);
            }
        }

        protected override void UpdateCommands(DependencyProperty property)
        {
            base.UpdateCommands(property);
            if (this.SpinCommandsAffectingProperties.Contains(property))
            {
                this.UpdateSpinCommands();
            }
            if (ReferenceEquals(property, BaseEdit.IsReadOnlyProperty))
            {
                this.UpdateSetNullValueCommand();
            }
        }

        private void UpdateLeftAndRightButtons()
        {
            ReadOnlyItemsSource<ButtonInfoBase> source = new ReadOnlyItemsSource<ButtonInfoBase>();
            ReadOnlyItemsSource<ButtonInfoBase> source2 = new ReadOnlyItemsSource<ButtonInfoBase>();
            foreach (ButtonInfoBase base2 in this.ActualButtons)
            {
                if (base2.IsLeft)
                {
                    source.Add(base2);
                    continue;
                }
                source2.Add(base2);
            }
            this.LeftButtons = source;
            this.RightButtons = source2;
        }

        protected virtual void UpdateSetNullValueCommand()
        {
            base.UpdateCommand(base.SetNullValueCommand);
        }

        private void UpdateShowEditorButtons()
        {
            this.ActualShowLeftButtons = (this.ShowEditorButtons && (this.LeftButtons != null)) && (((IList) this.LeftButtons).Count > 0);
            this.ActualShowRightButtons = (this.ShowEditorButtons && (this.RightButtons != null)) && (((IList) this.RightButtons).Count > 0);
        }

        private void UpdateSortedButtons()
        {
            ReadOnlyItemsSource<ButtonInfoBase> source = new ReadOnlyItemsSource<ButtonInfoBase>();
            foreach (ButtonInfoBase base2 in this.LeftButtons)
            {
                source.Add(base2);
            }
            foreach (ButtonInfoBase base3 in this.RightButtons)
            {
                source.Add(base3);
            }
            this.SortedButtons = source;
        }

        protected virtual void UpdateSpinCommands()
        {
            base.UpdateCommand(base.SpinUpCommand);
            base.UpdateCommand(base.SpinDownCommand);
        }

        internal ButtonsControl LeftButtonsControl { get; set; }

        internal ButtonsControl RightButtonsControl { get; set; }

        private ButtonEditStrategy EditStrategy =>
            base.EditStrategy as ButtonEditStrategy;

        protected override IEnumerator LogicalChildren
        {
            get
            {
                List<object> list = new List<object>();
                if (base.LogicalChildren != null)
                {
                    IEnumerator logicalChildren = base.LogicalChildren;
                    while (logicalChildren.MoveNext())
                    {
                        list.Add(logicalChildren.Current);
                    }
                }
                foreach (ButtonInfoBase base2 in this.ActualButtons)
                {
                    list.Add(base2);
                }
                return list.GetEnumerator();
            }
        }

        private Locker ButtonsUpdateLocker { get; set; }

        public bool ActualShowLeftButtons
        {
            get => 
                (bool) base.GetValue(ActualShowLeftButtonsProperty);
            private set => 
                base.SetValue(ActualShowLeftButtonsPropertyKey, value);
        }

        public bool ActualShowRightButtons
        {
            get => 
                (bool) base.GetValue(ActualShowRightButtonsProperty);
            private set => 
                base.SetValue(ActualShowRightButtonsPropertyKey, value);
        }

        [Category("Behavior")]
        public EditorPlacement? NullValueButtonPlacement
        {
            get => 
                (EditorPlacement?) base.GetValue(NullValueButtonPlacementProperty);
            set => 
                base.SetValue(NullValueButtonPlacementProperty, value);
        }

        [Category("Behavior")]
        public bool ShowNullValueButtonOnFocusOnly
        {
            get => 
                (bool) base.GetValue(ShowNullValueButtonOnFocusOnlyProperty);
            set => 
                base.SetValue(ShowNullValueButtonOnFocusOnlyProperty, value);
        }

        [Description("Gets or sets whether to display editor buttons, including the default button. This is a dependency property."), Category("Behavior")]
        public bool ShowEditorButtons
        {
            get => 
                (bool) base.GetValue(ShowEditorButtonsProperty);
            set => 
                base.SetValue(ShowEditorButtonsProperty, value);
        }

        [Description("Gets or sets whether the edit box is displayed. This is a dependency property."), Category("Behavior")]
        public bool ShowText
        {
            get => 
                (bool) base.GetValue(ShowTextProperty);
            set => 
                base.SetValue(ShowTextProperty, value);
        }

        [Description("Gets or sets whether the editor's default button is displayed. This is a dependency property."), Category("Behavior")]
        public bool? AllowDefaultButton
        {
            get => 
                (bool?) base.GetValue(AllowDefaultButtonProperty);
            set => 
                base.SetValue(AllowDefaultButtonProperty, value);
        }

        [Description("Gets or sets whether end-users are allowed to edit the text displayed in the edit box. This is a dependency property."), Category("Behavior"), DefaultValue(true)]
        public bool? IsTextEditable
        {
            get => 
                (bool?) base.GetValue(IsTextEditableProperty);
            set => 
                base.SetValue(IsTextEditableProperty, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Common Properties"), Description("Returns the collection of buttons.")]
        public ButtonInfoCollection Buttons
        {
            get => 
                (ButtonInfoCollection) base.GetValue(ButtonsProperty);
            set => 
                base.SetValue(ButtonsProperty, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("Gets a collection of buttons aligned to the editor's left edge. This is a dependency property."), Browsable(false)]
        public IEnumerable<ButtonInfoBase> LeftButtons
        {
            get => 
                (IEnumerable<ButtonInfoBase>) base.GetValue(LeftButtonsProperty);
            protected set => 
                base.SetValue(LeftButtonsPropertyKey, value);
        }

        [Description("Gets a collection of buttons aligned to the editor's right edge. This is a dependency property."), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<ButtonInfoBase> RightButtons
        {
            get => 
                (IEnumerable<ButtonInfoBase>) base.GetValue(RightButtonsProperty);
            protected set => 
                base.SetValue(RightButtonsPropertyKey, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), Description("This member supports the internal infrastructure, and is not intended to be used directly from your code."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<ButtonInfoBase> SortedButtons
        {
            get => 
                (IEnumerable<ButtonInfoBase>) base.GetValue(SortedButtonsProperty);
            protected set => 
                base.SetValue(SortedButtonsPropertyKey, value);
        }

        public IEnumerable ButtonsSource
        {
            get => 
                (IEnumerable) base.GetValue(ButtonsSourceProperty);
            set => 
                base.SetValue(ButtonsSourceProperty, value);
        }

        public DataTemplate ButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ButtonTemplateProperty);
            set => 
                base.SetValue(ButtonTemplateProperty, value);
        }

        public DataTemplateSelector ButtonTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ButtonTemplateSelectorProperty);
            set => 
                base.SetValue(ButtonTemplateSelectorProperty, value);
        }

        private List<ButtonInfoBase> ButtonsSourceButtons { get; set; }

        protected internal override Type StyleSettingsType =>
            typeof(ButtonEditStyleSettings);

        private ButtonEditPropertyProvider PropertyProvider =>
            base.PropertyProvider as ButtonEditPropertyProvider;

        protected internal ButtonEditSettings Settings =>
            (ButtonEditSettings) base.Settings;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("Gets a collection of buttons aligned to the editor's right edge. This is a dependency property.")]
        public IList<ButtonInfoBase> ActualButtons =>
            this.actualButtons;

        protected virtual List<DependencyProperty> SpinCommandsAffectingProperties =>
            new List<DependencyProperty> { 
                BaseEdit.EditModeProperty,
                BaseEdit.IsReadOnlyProperty,
                UIElement.IsEnabledProperty
            };

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ButtonEdit.<>c <>9 = new ButtonEdit.<>c();
            public static Func<ButtonInfoBase, bool> <>9__107_0;
            public static Action<ButtonInfoBase> <>9__117_0;
            public static Func<ButtonInfoBase, int> <>9__117_1;
            public static Action<ButtonInfoBase> <>9__155_0;
            public static Action<ButtonInfoBase> <>9__155_2;
            public static Action<IEnumerable<ButtonInfoBase>> <>9__155_1;
            public static Action<ButtonInfoBase> <>9__155_4;
            public static Action<IEnumerable<ButtonInfoBase>> <>9__155_3;
            public static Action<ButtonInfoBase> <>9__155_5;

            internal void <.cctor>b__21_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ButtonEdit) d).NullValueButtonPlacementChanged((EditorPlacement?) e.NewValue);
            }

            internal void <.cctor>b__21_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonEdit) o).ShowNullValueButtonOnFocusOnlyChanged((bool) args.NewValue);
            }

            internal void <.cctor>b__21_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ButtonEdit) d).OnButtonsSourceChanged();
            }

            internal void <.cctor>b__21_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ButtonEdit) d).OnButtonTemplateChanged();
            }

            internal void <.cctor>b__21_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ButtonEdit) d).OnButtonTemplateSelectorChanged();
            }

            internal void <GetActualButtons>b__117_0(ButtonInfoBase x)
            {
                x.IsFirst = false;
                x.IsLast = false;
            }

            internal int <GetActualButtons>b__117_1(ButtonInfoBase x) => 
                x.Index;

            internal bool <GetDefaultButtonInfo>b__107_0(ButtonInfoBase info) => 
                info.IsDefaultButton;

            internal void <UpdateButtonMargins>b__155_0(ButtonInfoBase x)
            {
                x.IsFirst = false;
                x.IsLast = false;
            }

            internal void <UpdateButtonMargins>b__155_1(IEnumerable<ButtonInfoBase> buttons)
            {
                Action<ButtonInfoBase> action = <>9__155_2;
                if (<>9__155_2 == null)
                {
                    Action<ButtonInfoBase> local1 = <>9__155_2;
                    action = <>9__155_2 = x => x.IsFirst = true;
                }
                buttons.FirstOrDefault<ButtonInfoBase>().Do<ButtonInfoBase>(action);
            }

            internal void <UpdateButtonMargins>b__155_2(ButtonInfoBase x)
            {
                x.IsFirst = true;
            }

            internal void <UpdateButtonMargins>b__155_3(IEnumerable<ButtonInfoBase> buttons)
            {
                Action<ButtonInfoBase> action = <>9__155_4;
                if (<>9__155_4 == null)
                {
                    Action<ButtonInfoBase> local1 = <>9__155_4;
                    action = <>9__155_4 = x => x.IsLast = true;
                }
                buttons.LastOrDefault<ButtonInfoBase>().Do<ButtonInfoBase>(action);
            }

            internal void <UpdateButtonMargins>b__155_4(ButtonInfoBase x)
            {
                x.IsLast = true;
            }

            internal void <UpdateButtonMargins>b__155_5(ButtonInfoBase x)
            {
                x.UpdateActualMargin();
            }
        }
    }
}

