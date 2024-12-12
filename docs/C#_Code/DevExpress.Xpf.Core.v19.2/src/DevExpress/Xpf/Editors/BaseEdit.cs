namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm;
    using DevExpress.Xpf;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using DevExpress.XtraEditors.DXErrorProvider;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), DefaultBindingProperty("EditValue"), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public abstract class BaseEdit : Control, IBaseEdit, IInputElement, ISupportDXValidation, IExportSettings, ILogicalOwner
    {
        public static readonly DependencyProperty AllowUpdateTwoWayBoundPropertiesOnSynchronizationProperty;
        public static readonly DependencyProperty EditValueTypeProperty;
        public static readonly DependencyProperty EditValueConverterProperty;
        public static readonly DependencyProperty InputTextToEditValueConverterProperty;
        internal static readonly DependencyPropertyKey ShouldDisableExcessiveUpdatesInInplaceInactiveModePropertyKey;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty ShouldDisableExcessiveUpdatesInInplaceInactiveModeProperty;
        public static readonly DependencyProperty DisableExcessiveUpdatesInInplaceInactiveModeProperty;
        public static readonly DependencyProperty EditValuePostDelayProperty;
        public static readonly DependencyProperty EditValuePostModeProperty;
        public static readonly DependencyProperty AllowNullInputProperty;
        private static readonly DependencyPropertyKey IsNullTextVisiblePropertyKey;
        public static readonly DependencyProperty IsNullTextVisibleProperty;
        public static readonly DependencyProperty ShowNullTextProperty;
        public static readonly DependencyProperty NullTextProperty;
        public static readonly DependencyProperty NullValueProperty;
        public static readonly DependencyProperty ShowNullTextForEmptyValueProperty;
        public static readonly RoutedEvent CustomDisplayTextEvent;
        public static readonly DependencyProperty DisplayTextProperty;
        protected static readonly DependencyPropertyKey DisplayTextPropertyKey;
        public static readonly DependencyProperty DisplayFormatStringProperty;
        public static readonly DependencyProperty DisplayTextConverterProperty;
        public static readonly DependencyProperty EditValueProperty;
        public static readonly DependencyProperty ShowBorderProperty;
        public static readonly DependencyProperty ShowBorderInInplaceModeProperty;
        public static readonly DependencyProperty BorderTemplateProperty;
        public static readonly DependencyProperty EditModeProperty;
        public static readonly DependencyProperty DisplayTemplateProperty;
        public static readonly DependencyProperty EditTemplateProperty;
        public static readonly RoutedEvent EditValueChangedEvent;
        public static readonly RoutedEvent EditValueChangingEvent;
        public static readonly DependencyProperty IsReadOnlyProperty;
        public static readonly RoutedEvent EditorActivatedEvent;
        protected static readonly DependencyPropertyKey OwnerEditPropertyKey;
        public static readonly DependencyProperty OwnerEditProperty;
        public static readonly DependencyProperty ShowErrorProperty;
        public static readonly DependencyProperty ShowErrorToolTipProperty;
        private static readonly DependencyPropertyKey HasValidationErrorPropertyKey;
        public static readonly DependencyProperty HasValidationErrorProperty;
        protected internal static readonly DependencyPropertyKey ValidationErrorPropertyKey;
        public static readonly DependencyProperty ValidationErrorProperty;
        public static readonly DependencyProperty ErrorToolTipContentTemplateProperty;
        public static readonly DependencyProperty ValidateOnEnterKeyPressedProperty;
        public static readonly DependencyProperty ValidateOnTextInputProperty;
        public static readonly DependencyProperty CausesValidationProperty;
        public static readonly DependencyProperty InvalidValueBehaviorProperty;
        public static readonly DependencyProperty IsPrintingModeProperty;
        protected static readonly DependencyPropertyKey ActualEditorControlTemplatePropertyKey;
        public static readonly DependencyProperty ActualEditorControlTemplateProperty;
        public static readonly DependencyProperty TrimmedTextToolTipContentTemplateProperty;
        public static readonly DependencyProperty IsEditorActiveProperty;
        private static readonly DependencyPropertyKey IsEditorActivePropertyKey;
        public static readonly RoutedEvent ValidateEvent;
        public static readonly DependencyProperty ActualBorderTemplateProperty;
        protected static readonly DependencyPropertyKey ActualBorderTemplatePropertyKey;
        public static readonly DependencyProperty StyleSettingsProperty;
        public static readonly DependencyProperty ValidationErrorTemplateProperty;
        public static readonly DependencyProperty AllowUpdateTextBlockWhenPrintingProperty;
        private readonly Locker supportInitializeLocker;
        private BaseEditSettings settings;
        protected SortedList<int, FrameworkElement> additionalInplaceModeElements;
        private IDisplayTextProvider displayTextProvider;
        private IDisplayTextProvider totalDisplayTextProvider;
        private FrameworkElement editCore;
        private Control borderControl;
        private Control glowControl;
        private Thickness borderThickness;
        private bool isValueChanged;
        private bool IsTabStopValue;
        private bool IsTabStopValueSet;
        private Locker isTabStopLocker;
        private bool showEditButtons;
        private readonly List<object> logicalChildren;
        private WeakReference marginCorrector;

        public event CustomDisplayTextEventHandler CustomDisplayText
        {
            add
            {
                base.AddHandler(CustomDisplayTextEvent, value);
            }
            remove
            {
                base.RemoveHandler(CustomDisplayTextEvent, value);
            }
        }

        event RoutedEventHandler ILogicalOwner.Loaded
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

        public event RoutedEventHandler EditorActivated
        {
            add
            {
                base.AddHandler(EditorActivatedEvent, value);
            }
            remove
            {
                base.RemoveHandler(EditorActivatedEvent, value);
            }
        }

        public event EditValueChangedEventHandler EditValueChanged
        {
            add
            {
                base.AddHandler(EditValueChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(EditValueChangedEvent, value);
            }
        }

        public event EditValueChangingEventHandler EditValueChanging
        {
            add
            {
                base.AddHandler(EditValueChangingEvent, value);
            }
            remove
            {
                base.RemoveHandler(EditValueChangingEvent, value);
            }
        }

        public event ValidateEventHandler Validate
        {
            add
            {
                base.AddHandler(ValidateEvent, value);
            }
            remove
            {
                base.RemoveHandler(ValidateEvent, value);
            }
        }

        static BaseEdit()
        {
            Type ownerType = typeof(BaseEdit);
            AllowUpdateTwoWayBoundPropertiesOnSynchronizationProperty = DependencyPropertyManager.Register("AllowUpdateTwoWayBoundPropertiesOnSynchronization", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            EditValueTypeProperty = DependencyPropertyManager.Register("EditValueType", typeof(Type), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((BaseEdit) d).EditValueTypeChanged((Type) e.NewValue)));
            EditValueConverterProperty = DependencyPropertyManager.Register("EditValueConverter", typeof(IValueConverter), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((BaseEdit) d).EditValueConverterChanged((IValueConverter) e.NewValue)));
            InputTextToEditValueConverterProperty = DependencyPropertyManager.Register("InputTextToEditValueConverter", typeof(IValueConverter), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((BaseEdit) d).InputTextToEditValueConverterChanged((IValueConverter) e.NewValue)));
            EditValuePostDelayProperty = DependencyPropertyManager.Register("EditValuePostDelay", typeof(int), ownerType, new FrameworkPropertyMetadata(0x3e8, (d, e) => ((BaseEdit) d).EditValuePostDelayChanged((int) e.NewValue)));
            EditValuePostModeProperty = DependencyPropertyManager.Register("EditValuePostMode", typeof(PostMode), ownerType, new FrameworkPropertyMetadata(PostMode.Immediate, (d, e) => ((BaseEdit) d).EditValuePostModeChanged((PostMode) e.NewValue)));
            ShouldDisableExcessiveUpdatesInInplaceInactiveModePropertyKey = DependencyPropertyManager.RegisterReadOnly("ShouldDisableExcessiveUpdatesInInplaceInactiveMode", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((BaseEdit) d).ShouldDisableExcessiveUpdatesInInplaceInactiveModeChanged((bool) e.NewValue)));
            ShouldDisableExcessiveUpdatesInInplaceInactiveModeProperty = ShouldDisableExcessiveUpdatesInInplaceInactiveModePropertyKey.DependencyProperty;
            DisableExcessiveUpdatesInInplaceInactiveModeProperty = DependencyPropertyManager.Register("DisableExcessiveUpdatesInInplaceInactiveMode", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((BaseEdit) d).DisableExcessiveUpdatesInInplaceInactiveModeChanged((bool?) e.NewValue)));
            AllowNullInputProperty = DependencyPropertyManager.Register("AllowNullInput", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(BaseEdit.OnAllowNullInputChanged)));
            IsNullTextVisiblePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsNullTextVisible", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEdit.OnIsNullTextVisiblePropertyChanged)));
            IsNullTextVisibleProperty = IsNullTextVisiblePropertyKey.DependencyProperty;
            ShowNullTextProperty = DependencyPropertyManager.Register("ShowNullText", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BaseEdit.OnShowNullTextPropertyChanged)));
            ShowNullTextForEmptyValueProperty = DependencyPropertyManager.Register("ShowNullTextForEmptyValue", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((BaseEdit) d).OnShowNullTextForEmptyValueChanged((bool) e.NewValue)));
            NullTextProperty = DependencyPropertyManager.Register("NullText", typeof(string), ownerType, new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BaseEdit.OnNullTextPropertyChanged)));
            NullValueProperty = DependencyPropertyManager.Register("NullValue", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BaseEdit.OnNullValuePropertyChanged)));
            CustomDisplayTextEvent = EventManager.RegisterRoutedEvent("CustomDisplayText", RoutingStrategy.Direct, typeof(CustomDisplayTextEventHandler), ownerType);
            BorderTemplateProperty = DependencyPropertyManager.Register("BorderTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(BaseEdit.OnBorderTemplatePropertyChanged)));
            ShowBorderProperty = DependencyPropertyManager.Register("ShowBorder", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BaseEdit.OnShowBorderChanged)));
            ShowBorderInInplaceModeProperty = DependencyProperty.Register("ShowBorderInInplaceMode", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BaseEdit.OnShowBorderChanged)));
            ShowErrorProperty = DependencyPropertyManager.Register("ShowError", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BaseEdit.OnShowErrorChanged)));
            ShowErrorToolTipProperty = DependencyPropertyManager.Register("ShowErrorToolTip", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BaseEdit.OnShowErrorToolTipChanged)));
            DisplayTemplateProperty = DependencyPropertyManager.Register("DisplayTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BaseEdit.OnDisplayTemplateChanged)));
            EditTemplateProperty = DependencyPropertyManager.Register("EditTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEdit.OnEditTemplateChanged)));
            EditModeProperty = DependencyPropertyManager.Register("EditMode", typeof(DevExpress.Xpf.Editors.EditMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.EditMode.Standalone, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEdit.OnEditModeChanged)));
            EditValueChangedEvent = EventManager.RegisterRoutedEvent("EditValueChanged", RoutingStrategy.Direct, typeof(EditValueChangedEventHandler), ownerType);
            EditValueChangingEvent = EventManager.RegisterRoutedEvent("EditValueChanging", RoutingStrategy.Direct, typeof(EditValueChangingEventHandler), ownerType);
            ValidateEvent = EventManager.RegisterRoutedEvent("Validate", RoutingStrategy.Direct, typeof(ValidateEventHandler), ownerType);
            IsReadOnlyProperty = DependencyPropertyManager.Register("IsReadOnly", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEdit.OnIsReadOnlyChanged)));
            EditorActivatedEvent = EventManager.RegisterRoutedEvent("EditorActivatedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), ownerType);
            OwnerEditPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("OwnerEdit", ownerType, ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
            OwnerEditProperty = OwnerEditPropertyKey.DependencyProperty;
            HasValidationErrorPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("HasValidationError", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(BaseEdit.OnHasValidationErrorChanged)));
            HasValidationErrorProperty = HasValidationErrorPropertyKey.DependencyProperty;
            ValidationErrorPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("ValidationError", typeof(BaseValidationError), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BaseEdit.OnValidationErrorChanged), new CoerceValueCallback(BaseEdit.OnCoerceValidationError)));
            ValidationErrorProperty = ValidationErrorPropertyKey.DependencyProperty;
            ErrorToolTipContentTemplateProperty = DependencyPropertyManager.Register("ErrorToolTipContentTemplate", typeof(DataTemplate), ownerType);
            ValidateOnEnterKeyPressedProperty = DependencyPropertyManager.Register("ValidateOnEnterKeyPressed", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ValidateOnTextInputProperty = DependencyPropertyManager.Register("ValidateOnTextInput", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            InvalidValueBehaviorProperty = DependencyPropertyManager.Register("InvalidValueBehavior", typeof(DevExpress.Xpf.Editors.Validation.InvalidValueBehavior), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.Validation.InvalidValueBehavior.WaitForValidValue));
            CausesValidationProperty = DependencyPropertyManager.Register("CausesValidation", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ((DependencyPropertyKey) typeof(Validation).GetField("ErrorsPropertyKey", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null)).OverrideMetadata(ownerType, new FrameworkPropertyMetadata((d, e) => ((BaseEdit) d).ValidationErrorChanged((IEnumerable<System.Windows.Controls.ValidationError>) e.OldValue, (IEnumerable<System.Windows.Controls.ValidationError>) e.NewValue)));
            IsPrintingModeProperty = DependencyPropertyManager.Register("IsPrintingMode", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(BaseEdit.OnIsPrintingModePropertyChanged)));
            ActualEditorControlTemplatePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualEditorControlTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BaseEdit.OnActualEditorControlTemplateChanged)));
            ActualEditorControlTemplateProperty = ActualEditorControlTemplatePropertyKey.DependencyProperty;
            IsEditorActivePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsEditorActive", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(BaseEdit.OnIsEditorActiveChanged)));
            IsEditorActiveProperty = IsEditorActivePropertyKey.DependencyProperty;
            TrimmedTextToolTipContentTemplateProperty = DependencyPropertyManager.Register("TrimmedTextToolTipContentTemplate", typeof(DataTemplate), ownerType);
            Validation.ErrorTemplateProperty.AddOwner(typeof(BaseEdit), new FrameworkPropertyMetadata(GetDefaultErrorTemplate()));
            EditValueProperty = DependencyPropertyManager.Register("EditValue", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(BaseEdit.OnEditValueChanged), new CoerceValueCallback(BaseEdit.OnCoerceEditValue), true, UpdateSourceTrigger.LostFocus));
            Control.BorderThicknessProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(Border.BorderThicknessProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
            Control.IsTabStopProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata((d, e) => ((BaseEdit) d).OnIsTabStopPropertyChanged(e), (d, e) => ((BaseEdit) d).OnIsTabStopCoerceValue(e)));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(KeyboardNavigationMode.Local));
            KeyboardNavigation.ControlTabNavigationProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(KeyboardNavigationMode.None));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(KeyboardNavigationMode.None));
            FrameworkElement.ToolTipProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(BaseEdit.OnToolTipPropertyChanged), new CoerceValueCallback(BaseEdit.OnCoerceToolTip)));
            About.CheckLicenseShowNagScreen(typeof(BaseEdit));
            DisplayTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("DisplayText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEdit.OnDisplayTextChanged)));
            DisplayTextProperty = DisplayTextPropertyKey.DependencyProperty;
            DisplayFormatStringProperty = DependencyPropertyManager.Register("DisplayFormatString", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEdit.OnDisplayFormatStringChanged), new CoerceValueCallback(BaseEdit.OnCoerceFormatString)));
            DisplayTextConverterProperty = DependencyPropertyManager.Register("DisplayTextConverter", typeof(IValueConverter), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(BaseEdit.OnDisplayTextConverterChanged)));
            ActualBorderTemplatePropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualBorderTemplate", typeof(ControlTemplate), typeof(BaseEdit), new PropertyMetadata());
            ActualBorderTemplateProperty = ActualBorderTemplatePropertyKey.DependencyProperty;
            StyleSettingsProperty = DependencyPropertyManager.Register("StyleSettings", typeof(BaseEditStyleSettings), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((BaseEdit) d).StyleSettingsChangedInternal((BaseEditStyleSettings) e.NewValue)));
            ValidationErrorTemplateProperty = DependencyPropertyManager.Register("ValidationErrorTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((BaseEdit) d).OnValidationErrorTemplateChanged((DataTemplate) e.NewValue)));
            AllowUpdateTextBlockWhenPrintingProperty = DependencyPropertyManager.Register("AllowUpdateTextBlockWhenPrinting", typeof(bool), ownerType, new PropertyMetadata(true));
            SvgImageHelper.StateProperty.AddOwner(ownerType, new FrameworkPropertyMetadata((d, e) => ((BaseEdit) d).SvgStateChanged((string) e.NewValue)));
        }

        protected BaseEdit()
        {
            // Unresolved stack state at '00000172'
        }

        private void AddLogicalChildInternal(object child)
        {
            base.AddLogicalChild(child);
            this.logicalChildren.Add(child);
            this.EditStrategy.AddILogicalOwnerChild(child);
        }

        protected virtual void AfterSetSettings()
        {
        }

        protected override Size ArrangeOverride(Size arrangeBounds) => 
            this.ContentManagementStrategy.ArrangeOverride(arrangeBounds);

        internal unsafe Size ArrangeOverrideInplaceMode(Size arrangeSize)
        {
            Point point;
            if (this.borderControl != null)
            {
                point = new Point();
                this.borderControl.Arrange(new Rect(point, arrangeSize));
            }
            if (this.glowControl != null)
            {
                point = new Point();
                this.glowControl.Arrange(new Rect(point, arrangeSize));
            }
            arrangeSize.Width = Math.Max((double) 0.0, (double) (arrangeSize.Width - (this.borderThickness.Left + this.borderThickness.Right)));
            arrangeSize.Height = Math.Max((double) 0.0, (double) (arrangeSize.Height - (this.borderThickness.Top + this.borderThickness.Bottom)));
            Size size = DockPanelLayoutHelper.ArrangeDockPanelLayout(this, new Rect(new Point(this.borderThickness.Left, this.borderThickness.Top), arrangeSize), true, new SkipLayout(this.SkipLayoutInInplaceMode), null);
            Size* sizePtr1 = &size;
            sizePtr1.Width += this.borderThickness.Left + this.borderThickness.Right;
            Size* sizePtr2 = &size;
            sizePtr2.Height += this.borderThickness.Top + this.borderThickness.Bottom;
            return size;
        }

        internal Size ArrangeOverrideStandaloneMode(Size arrangeSize) => 
            base.ArrangeOverride(arrangeSize);

        public sealed override void BeginInit()
        {
            ((IBaseEdit) this).BeginInit(true);
        }

        protected virtual void BeginInitInternal()
        {
        }

        protected internal virtual bool BeginInplaceEditing() => 
            true;

        private bool CalcActualShowBorder() => 
            (this.PropertyProvider.EditMode == DevExpress.Xpf.Editors.EditMode.Standalone) ? this.ShowBorder : (this.ShowBorder && this.ShowBorderInInplaceMode);

        protected bool CanSetNullValueInternal(object arg) => 
            !this.IsReadOnly;

        protected virtual bool CheckAllowLostKeyboardFocus() => 
            (this.EditMode == DevExpress.Xpf.Editors.EditMode.Standalone) && !this.EditStrategy.DoValidate(UpdateEditorSource.LostFocus);

        protected virtual void CheckStyleSettings(BaseEditStyleSettings settings)
        {
            if ((settings != null) && !this.StyleSettingsType.IsAssignableFrom(settings.GetType()))
            {
                throw new ArgumentException($"The StyleSettings should be descendant of the {this.StyleSettingsType.Name} class for the {base.GetType().Name}.");
            }
        }

        public void ClearError()
        {
            this.EditStrategy.ResetValidationError();
            this.DoValidate();
        }

        protected virtual object CoerceEditValue(DependencyObject d, object value) => 
            this.EditStrategy.CoerceEditValue(value);

        protected virtual ActualPropertyProvider CreateActualPropertyProvider() => 
            new ActualPropertyProvider(this);

        protected internal virtual BaseEditSettings CreateEditorSettings() => 
            EditorSettingsProvider.Default.CreateEditorSettings(base.GetType());

        protected virtual EditStrategyBase CreateEditStrategy() => 
            new DummyEditStrategy(this);

        protected internal virtual BaseEditStyleSettings CreateStyleSettings() => 
            (BaseEditStyleSettings) Activator.CreateInstance(this.StyleSettingsType);

        void ILogicalOwner.AddChild(object child)
        {
            this.AddLogicalChildInternal(child);
        }

        void ILogicalOwner.RemoveChild(object child)
        {
            this.RemoveLogicalChildInternal(child);
        }

        void IBaseEdit.BeginInit(bool callBase)
        {
            if (callBase && !this.IsInSupportInitializing)
            {
                base.BeginInit();
            }
            this.BeginInitInternal();
            this.supportInitializeLocker.Lock();
            this.EditStrategy.SupportInitializeBeginInit();
        }

        void IBaseEdit.ClearValue(DependencyProperty dp)
        {
            base.ClearValue(dp);
        }

        void IBaseEdit.EndInit(bool callBase, bool shouldSync)
        {
            this.supportInitializeLocker.Unlock();
            if (callBase && !this.IsInSupportInitializing)
            {
                base.EndInit();
            }
            this.EndInitInternal(callBase);
            if (shouldSync)
            {
                this.EditStrategy.SupportInitializeEndInit();
            }
        }

        void IBaseEdit.FlushPendingEditActions()
        {
            this.FlushPendingEditActions(UpdateEditorSource.DoValidate);
        }

        void IBaseEdit.ForceInitialize(bool callBase)
        {
            if (callBase)
            {
                this.OnInitialized(EventArgs.Empty);
            }
            else
            {
                this.OnInitializedInternal();
            }
        }

        string IBaseEdit.GetDisplayText(object editValue, bool applyFormatting) => 
            this.GetDisplayText(editValue, applyFormatting);

        bool IBaseEdit.GetShowEditorButtons() => 
            this.GetShowEditorButtons();

        object IBaseEdit.GetValue(DependencyProperty d) => 
            base.GetValue(d);

        bool IBaseEdit.IsActivatingKey(Key key, ModifierKeys modifiers) => 
            this.IsActivatingKey(key, modifiers);

        bool IBaseEdit.IsChildElement(IInputElement element, DependencyObject root) => 
            this.IsChildElement((DependencyObject) element, root);

        bool IBaseEdit.NeedsKey(Key key, ModifierKeys modifiers) => 
            this.NeedsKey(key, modifiers);

        void IBaseEdit.ProcessActivatingKey(Key key, ModifierKeys modifiers)
        {
            this.ProcessActivatingKey(key, modifiers);
        }

        BindingExpressionBase IBaseEdit.SetBinding(DependencyProperty dp, BindingBase binding) => 
            base.SetBinding(dp, binding);

        void IBaseEdit.SetInplaceEditingProvider(IInplaceEditingProvider provider)
        {
            this.InplaceEditing = provider;
        }

        void IBaseEdit.SetSettings(BaseEditSettings settings)
        {
            this.SetSettings(settings);
            this.AfterSetSettings();
        }

        void IBaseEdit.SetShowEditorButtons(bool show)
        {
            this.SetShowEditorButtons(show);
        }

        void IBaseEdit.UpdateDisplayText()
        {
            if (this.EditStrategy != null)
            {
                this.EditStrategy.UpdateDisplayText();
            }
        }

        void IBaseEdit.UpdateErrorTooltip()
        {
            this.EditStrategy.ResetErrorProvider();
        }

        protected virtual void DisableExcessiveUpdatesInInplaceInactiveModeChanged(bool? newValue)
        {
            this.PropertyProvider.SetDisableExcessiveUpdatesInInplaceInactiveMode(newValue);
            if (!this.PropertyProvider.SuppressFeatures)
            {
                this.EditStrategy.UpdateEditorOnEditingChange(true);
            }
        }

        public virtual bool DoValidate() => 
            this.EditStrategy.DoValidate(UpdateEditorSource.DoValidate);

        private void EditorToolTipOpening(object sender, ToolTipEventArgs e)
        {
            if (!this.EditStrategy.HasToolTipContent())
            {
                e.Handled = true;
            }
        }

        protected virtual void EditValueConverterChanged(IValueConverter converter)
        {
            throw new NotSupportedException("An exception occurred due to an obsolete property/method. For the actual component structure, please refer to the technical documentation.");
        }

        protected virtual void EditValuePostDelayChanged(int value)
        {
            this.EditStrategy.EditValuePostDelayChanged(value);
        }

        protected virtual void EditValuePostModeChanged(PostMode value)
        {
            this.EditStrategy.EditValuePostModeChanged(value);
        }

        protected virtual void EditValueTypeChanged(Type type)
        {
            this.EditStrategy.EditValueTypeChanged(type);
        }

        public sealed override void EndInit()
        {
            ((IBaseEdit) this).EndInit(true, true);
        }

        protected virtual void EndInitInternal(bool callBase)
        {
        }

        internal virtual void FlushPendingEditActions(UpdateEditorSource updateEditor)
        {
        }

        protected virtual bool FocusEditCore() => 
            (this.EditCore != null) && (!this.IsInactiveMode && ((!FocusHelper.IsFocused(this.EditCore) || !FocusHelper.IsKeyboardFocused(this.EditCore)) ? this.EditCore.Focus() : false));

        protected internal virtual void ForceChangeDisplayText()
        {
            this.EditStrategy.UpdateDisplayText();
        }

        protected virtual string FormatDisplayText(object editValue, bool applyFormatting) => 
            this.EditStrategy.FormatDisplayText(editValue, applyFormatting);

        protected virtual ControlTemplate GetActualBorderTemplate() => 
            this.BorderTemplate;

        protected virtual ControlTemplate GetActualEditorControlTemplate() => 
            (this.EditMode != DevExpress.Xpf.Editors.EditMode.InplaceInactive) ? this.EditTemplate : this.DisplayTemplate;

        protected internal virtual string GetCustomDisplayText(object editValue, string displayText)
        {
            if ((this.displayTextProvider != null) || this.PropertyProvider.SuppressFeatures)
            {
                return null;
            }
            CustomDisplayTextEventArgs args = this.RaiseCustomDisplayText(editValue, displayText);
            return (args.Handled ? args.DisplayText : null);
        }

        protected virtual object GetDefaultEditValue() => 
            this.EditValue;

        private static ControlTemplate GetDefaultErrorTemplate()
        {
            ControlTemplate template = new ControlTemplate();
            template.Seal();
            return template;
        }

        protected virtual ControlTemplate GetDisplayTemplate() => 
            this.DisplayTemplate;

        protected internal virtual string GetDisplayText(object editValue, bool applyFormatting)
        {
            string displayTextCore = this.GetDisplayTextCore(editValue, applyFormatting);
            if (this.totalDisplayTextProvider != null)
            {
                string str2;
                bool? nullable = this.totalDisplayTextProvider.GetDisplayText(displayTextCore, editValue, out str2);
                this.PropertyProvider.SetHasDisplayTextProviderText(nullable.GetValueOrDefault(false));
                if (nullable.GetValueOrDefault(true))
                {
                    return str2;
                }
            }
            return displayTextCore;
        }

        protected internal virtual string GetDisplayTextConverterText(object editValue)
        {
            if (this.PropertyProvider.DisplayTextConverter == null)
            {
                return null;
            }
            object obj2 = this.PropertyProvider.DisplayTextConverter.Convert(editValue, typeof(string), null, CultureInfo.CurrentCulture);
            return (!(obj2 is string) ? ((obj2 != null) ? obj2.ToString() : string.Empty) : (obj2 as string));
        }

        protected virtual string GetDisplayTextCore(object editValue, bool applyFormatting)
        {
            this.PropertyProvider.SetHasDisplayTextProviderText(false);
            string displayText = this.FormatDisplayText(editValue, applyFormatting);
            if (applyFormatting)
            {
                string customDisplayText = this.GetCustomDisplayText(editValue, displayText);
                if (customDisplayText != null)
                {
                    return customDisplayText;
                }
                customDisplayText = this.GetDisplayTextConverterText(editValue);
                displayText = string.IsNullOrEmpty(customDisplayText) ? displayText : customDisplayText;
                if (this.displayTextProvider != null)
                {
                    string str3;
                    bool? nullable = this.displayTextProvider.GetDisplayText(displayText, editValue, out str3);
                    this.PropertyProvider.SetHasDisplayTextProviderText(nullable.GetValueOrDefault(false));
                    if (nullable.GetValueOrDefault(true))
                    {
                        return str3;
                    }
                }
            }
            return displayText;
        }

        protected virtual ControlTemplate GetEditTemplate() => 
            this.EditTemplate;

        public static bool GetHasValidationError(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (bool) element.GetValue(HasValidationErrorProperty);
        }

        public static BaseEdit GetOwnerEdit(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (BaseEdit) DependencyObjectHelper.GetValueWithInheritance(element, OwnerEditProperty);
        }

        protected internal virtual string GetPlainText() => 
            this.DisplayText;

        protected internal virtual Brush GetPrintBorderBrush() => 
            base.BorderBrush;

        protected internal virtual bool GetShowEditorButtons() => 
            this.showEditButtons;

        protected virtual string GetStateName() => 
            "stub";

        internal T GetTemplateChildInternal<T>(string childName) where T: FrameworkElement => 
            base.GetTemplateChild(childName) as T;

        public static BaseValidationError GetValidationError(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (BaseValidationError) element.GetValue(ValidationErrorProperty);
        }

        protected override Visual GetVisualChild(int index) => 
            this.ContentManagementStrategy.GetVisualChild(index);

        internal Visual GetVisualChildInplaceMode(int index)
        {
            if (index < this.additionalInplaceModeElements.Count)
            {
                return this.additionalInplaceModeElements.Values[index];
            }
            if ((index != this.additionalInplaceModeElements.Count) || (base.VisualChildrenCount <= 0))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return base.GetVisualChild(0);
        }

        internal Visual GetVisualChildStandaloneMode(int index) => 
            base.GetVisualChild(index);

        protected internal virtual bool HandlePreviewLostKeyboardFocus(DependencyObject oldFocus, DependencyObject newFocus)
        {
            bool? nullable = this.IsEditorLostFocus(oldFocus, newFocus);
            if (nullable != null)
            {
                return nullable.Value;
            }
            this.EditStrategy.PrepareForCheckAllowLostKeyboardFocus();
            bool flag = this.CheckAllowLostKeyboardFocus();
            return ((this.InvalidValueBehavior != DevExpress.Xpf.Editors.Validation.InvalidValueBehavior.AllowLeaveEditor) & flag);
        }

        protected static void HasValidationErrorPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
        }

        protected virtual void InputTextToEditValueConverterChanged(IValueConverter converter)
        {
            throw new NotSupportedException("An exception occurred due to an obsolete property/method. For the actual component structure, please refer to the technical documentation.");
        }

        protected internal bool IsActivatingKey(Key key, ModifierKeys modifiers)
        {
            GetIsActivatingKeyEventArgs e = new GetIsActivatingKeyEventArgs(key, modifiers, this, this.IsActivatingKeyCore(key, modifiers));
            this.Settings.RaiseGetIsActivatingKey(this, e);
            return e.IsActivatingKey;
        }

        private bool IsActivatingKeyCore(Key key, ModifierKeys modifiers) => 
            !this.IsReadOnly && (base.IsEnabled && this.Settings.IsActivatingKey(key, modifiers));

        protected internal virtual bool IsChildElement(DependencyObject element, DependencyObject root = null) => 
            LayoutHelper.IsChildElementEx(root ?? this, element, true);

        protected virtual bool IsEditingMode() => 
            (this.EditMode == DevExpress.Xpf.Editors.EditMode.Standalone) || (this.EditMode == DevExpress.Xpf.Editors.EditMode.InplaceActive);

        internal bool? IsEditorLostFocus(DependencyObject oldFocus, DependencyObject newFocus)
        {
            if ((newFocus == null) && (this.EditMode != DevExpress.Xpf.Editors.EditMode.Standalone))
            {
                return true;
            }
            bool flag2 = this.IsChildElement(newFocus, null);
            if (this.IsChildElement(oldFocus, null) & flag2)
            {
                return false;
            }
            if (flag2)
            {
                return false;
            }
            return null;
        }

        private void KeepIsTabStop(bool value)
        {
            this.IsTabStopValue = value;
            this.IsTabStopValueSet = true;
        }

        protected override Size MeasureOverride(Size constraint) => 
            this.ContentManagementStrategy.MeasureOverride(constraint);

        internal unsafe Size MeasureOverrideInplaceMode(Size constraint)
        {
            if (this.borderControl != null)
            {
                this.borderControl.Measure(constraint);
            }
            if (this.glowControl != null)
            {
                this.glowControl.Measure(constraint);
            }
            constraint.Width = Math.Max((double) 0.0, (double) (constraint.Width - (this.borderThickness.Left + this.borderThickness.Right)));
            constraint.Height = Math.Max((double) 0.0, (double) (constraint.Height - (this.borderThickness.Top + this.borderThickness.Bottom)));
            Size size = DockPanelLayoutHelper.MeasureDockPanelLayout(this, constraint, new SkipLayout(this.SkipLayoutInInplaceMode));
            Size* sizePtr1 = &size;
            sizePtr1.Width += this.borderThickness.Left + this.borderThickness.Right;
            Size* sizePtr2 = &size;
            sizePtr2.Height += this.borderThickness.Top + this.borderThickness.Bottom;
            return size;
        }

        internal Size MeasureOverrideStandaloneMode(Size constraint) => 
            base.MeasureOverride(constraint);

        internal static bool? NeedsBasicKey(Key key, Func<bool> needsEnterFunc)
        {
            if ((key == Key.Escape) || (key == Key.F2))
            {
                return false;
            }
            if (key == Key.Return)
            {
                return new bool?(needsEnterFunc());
            }
            return null;
        }

        protected virtual bool NeedsEnter(ModifierKeys modifiers) => 
            this.EditStrategy.NeedsEnterKey(modifiers);

        protected internal virtual bool NeedsKey(Key key, ModifierKeys modifiers)
        {
            bool? nullable = NeedsBasicKey(key, () => this.NeedsEnter(modifiers));
            return ((nullable == null) ? ((key != Key.Tab) ? (((key == Key.Left) || (key == Key.Right)) ? this.NeedsLeftRight() : (((key == Key.Up) || (key == Key.Down)) ? this.NeedsUpDown() : true)) : this.NeedsTab()) : nullable.Value);
        }

        protected virtual bool NeedsLeftRight() => 
            true;

        protected virtual bool NeedsTab() => 
            false;

        protected virtual bool NeedsUpDown() => 
            true;

        protected static void OnActualEditorControlTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
        }

        protected virtual void OnActualValidationErrorsChanged(IList errors)
        {
            if (errors != null)
            {
                SetHasValidationError(this, errors.Count > 0);
            }
            else
            {
                SetHasValidationError(this, false);
            }
            this.UpdateErrorPresenter();
            this.UpdateValidationService((errors.Count > 0) ? ((BaseValidationError) errors[0]) : null);
        }

        protected static void OnActualValidationErrorsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is BaseEdit)
            {
                ((BaseEdit) obj).OnActualValidationErrorsChanged((IList) e.NewValue);
            }
        }

        protected virtual void OnAllowNullInputChanged()
        {
        }

        private static void OnAllowNullInputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) d).OnAllowNullInputChanged();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateActualEditorControlTemplate();
            this.ContentManagementStrategy.OnEditorApplyTemplate();
            this.UpdateErrorPresenter();
            this.UpdateIsEditorActivePropertyForce(this.EditMode);
        }

        protected virtual void OnBorderTemplatePropertyChanged()
        {
            this.UpdateActualBorderTemplate();
        }

        protected static void OnBorderTemplatePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnBorderTemplatePropertyChanged();
        }

        protected static object OnCoerceEditValue(DependencyObject obj, object value) => 
            ((BaseEdit) obj).CoerceEditValue(obj, value);

        protected virtual object OnCoerceFormatString(object baseValue)
        {
            string str = (string) baseValue;
            return (((baseValue == null) || (str.Trim() == string.Empty)) ? null : str);
        }

        protected static object OnCoerceFormatString(DependencyObject obj, object baseValue) => 
            ((BaseEdit) obj).OnCoerceFormatString(baseValue);

        protected virtual object OnCoerceToolTip(object tooltip) => 
            this.EditStrategy.CoerceValidationToolTip(tooltip);

        private static object OnCoerceToolTip(DependencyObject d, object value) => 
            ((BaseEdit) d).OnCoerceToolTip(value);

        protected virtual object OnCoerceValidationError(BaseValidationError error) => 
            this.EditStrategy.CoerceBaseValidationError(error);

        private static object OnCoerceValidationError(DependencyObject d, object value) => 
            !(d is BaseEdit) ? value : ((BaseEdit) d).OnCoerceValidationError((BaseValidationError) value);

        protected virtual void OnDisplayFormatStringChanged()
        {
            this.PropertyProvider.SetDisplayFormatString(this.DisplayFormatString);
            this.Settings.DisplayFormat = this.DisplayFormatString;
            this.ForceChangeDisplayText();
        }

        protected static void OnDisplayFormatStringChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnDisplayFormatStringChanged();
        }

        protected virtual void OnDisplayTemplateChanged(ControlTemplate newTemplate)
        {
            this.UpdateActualEditorControlTemplate();
        }

        protected static void OnDisplayTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnDisplayTemplateChanged((ControlTemplate) e.NewValue);
        }

        protected virtual void OnDisplayTextChanged(string displayText)
        {
            this.EditStrategy.DisplayTextChanged(displayText);
        }

        protected static void OnDisplayTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnDisplayTextChanged((string) e.NewValue);
        }

        protected virtual void OnDisplayTextConverterChanged(IValueConverter converter)
        {
            this.PropertyProvider.SetDisplayTextConverter(converter);
            this.ForceChangeDisplayText();
        }

        protected static void OnDisplayTextConverterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnDisplayTextConverterChanged((IValueConverter) e.NewValue);
        }

        protected virtual void OnEditCoreAssigned()
        {
            if ((this.EditCore != null) && (base.IsFocused && (this.EditMode != DevExpress.Xpf.Editors.EditMode.InplaceInactive)))
            {
                this.SetupFocusAction.PerformPostpone(() => this.EditStrategy.FocusEditCore());
            }
            this.OnEditCoreAssignedInTokenMode();
            this.EditStrategy.ApplyStyleSettings(this.PropertyProvider.StyleSettings);
            this.IsEditorActive = this.IsEditingMode();
            this.EditStrategy.UpdateNullTextForeground(this.PropertyProvider.IsNullTextVisible);
            this.EditStrategy.UpdateAllowDrop(this.PropertyProvider.IsNullTextVisible && this.HasNullText);
        }

        protected virtual void OnEditCoreAssignedInTokenMode()
        {
        }

        protected virtual void OnEditModeChanged(DevExpress.Xpf.Editors.EditMode oldValue, DevExpress.Xpf.Editors.EditMode newValue)
        {
            this.PropertyProvider.SetEditMode(newValue);
            this.EditStrategy.ProcessEditModeChanged(oldValue, newValue);
            if (this.CanAcceptFocus && this.IsEditorKeyboardFocused)
            {
                base.Focus();
            }
            this.UpdateIsEditorActiveProperty(newValue);
            this.UpdateContentManagementStrategy();
            this.UpdateActualEditorControlTemplate();
            this.UpdateActualBorderTemplate();
            if (this.MarginCorrector != null)
            {
                this.MarginCorrector.EditMode = this.EditMode;
            }
        }

        protected static void OnEditModeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnEditModeChanged((DevExpress.Xpf.Editors.EditMode) e.OldValue, (DevExpress.Xpf.Editors.EditMode) e.NewValue);
        }

        private void OnEditSettingsChanged()
        {
            this.Settings.AssignToEdit(this);
        }

        protected virtual void OnEditTemplateChanged(ControlTemplate newTemplate)
        {
            this.UpdateActualEditorControlTemplate();
        }

        protected static void OnEditTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnEditTemplateChanged((ControlTemplate) e.NewValue);
        }

        protected internal void OnEditValueChanged(object oldValue, object newValue)
        {
            object editValue = this.PropertyProvider.EditValue;
            this.PropertyProvider.SetEditValue(newValue);
            this.isValueChanged = true;
            this.EditStrategy.IsValueChanged = true;
            this.EditStrategy.EditValueChanged(editValue, this.PropertyProvider.EditValue);
        }

        protected static void OnEditValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnEditValueChanged(e.OldValue, e.NewValue);
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            if (!e.GetHandled() && (e.OriginalSource == this))
            {
                e.SetHandled(this.FocusEditCore());
            }
        }

        protected virtual void OnHasValidationErrorChanged()
        {
            if (this.MarginCorrector != null)
            {
                this.MarginCorrector.HasValidationError = this.HasValidationError;
            }
        }

        private static void OnHasValidationErrorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BaseEdit)
            {
                (d as BaseEdit).OnHasValidationErrorChanged();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.OnInitializedInternal();
        }

        private void OnInitializedInternal()
        {
            if (!this.IsInSupportInitializing)
            {
                this.EditStrategy.OnInitialized();
            }
        }

        protected virtual void OnIsEditorActiveChaged(bool value)
        {
            if (value)
            {
                base.RaiseEvent(new RoutedEventArgs(EditorActivatedEvent));
            }
            this.EditStrategy.IsEditorActiveChanged(value);
        }

        private static void OnIsEditorActiveChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnIsEditorActiveChaged((bool) e.NewValue);
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);
            bool newValue = (bool) e.NewValue;
            if (newValue)
            {
                this.isTabStopLocker.DoLockedAction(() => base.SetCurrentValue(Control.IsTabStopProperty, false));
            }
            else if (!newValue && (!this.IsTabStopValueSet || (this.IsTabStopValueSet && this.IsTabStopValue)))
            {
                this.isTabStopLocker.DoLockedAction(() => base.SetCurrentValue(Control.IsTabStopProperty, true));
            }
        }

        protected virtual void OnIsNullTextVisibleChanged(bool isVisible)
        {
            this.PropertyProvider.SetIsNullTextVisible(isVisible);
            this.EditStrategy.OnIsNullTextVisibleChanged(isVisible);
        }

        private static void OnIsNullTextVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) d).OnIsNullTextVisibleChanged((bool) e.NewValue);
        }

        private static void OnIsPrintingModePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {
                ((BaseEdit) obj).EditMode = DevExpress.Xpf.Editors.EditMode.InplaceInactive;
            }
        }

        protected virtual void OnIsReadOnlyChanged()
        {
            this.EditStrategy.IsReadOnlyChanged();
        }

        protected static void OnIsReadOnlyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnIsReadOnlyChanged();
        }

        public object OnIsTabStopCoerceValue(object value)
        {
            if (!this.isTabStopLocker)
            {
                this.KeepIsTabStop((bool) value);
            }
            return value;
        }

        protected virtual void OnIsTabStopPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            ValueSource valueSource = System.Windows.DependencyPropertyHelper.GetValueSource(this, Control.IsTabStopProperty);
            if (!this.isTabStopLocker)
            {
                this.KeepIsTabStop(base.IsTabStop);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            this.EditStrategy.ProcessKeyDown(e);
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            this.ImmediateActionsManager.ExecuteActions();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.OnLoadedInternal();
            ThemeManager.AddThemeChangedHandler(this, new ThemeChangedRoutedEventHandler(this.OnThemeChanged));
        }

        protected virtual void OnLoadedInternal()
        {
            this.SetupFocusAction.Perform();
            this.EditStrategy.OnLoaded();
        }

        protected virtual void OnNullTextChanged(string nullText)
        {
            this.Settings.NullText = nullText;
            this.EditStrategy.OnNullTextChanged(nullText);
        }

        protected static void OnNullTextPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnNullTextChanged((string) e.NewValue);
        }

        protected virtual void OnNullTextTemplateChanged(ControlTemplate newTemplate)
        {
            this.UpdateActualEditorControlTemplate();
        }

        protected static void OnNullTextTemplatePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnNullTextTemplateChanged((ControlTemplate) e.NewValue);
        }

        protected virtual void OnNullValueChanged(object nullValue)
        {
            this.PropertyProvider.SetNullValue(nullValue);
            this.Settings.NullValue = nullValue;
            this.EditStrategy.OnNullValueChanged(nullValue);
        }

        protected static void OnNullValuePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnNullValueChanged(e.NewValue);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            this.EditStrategy.ProcessPreviewKeyDown(e);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            if ((this.EditCore != null) && (FocusHelper.CanBeFocused(this.EditCore) && !base.IsKeyboardFocusWithin))
            {
                this.ProcessFocusEditCore(e);
            }
            else if (!base.IsFocused && !base.IsKeyboardFocusWithin)
            {
                base.Focus();
            }
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);
            this.EditStrategy.OnMouseWheel(e);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            this.UpdateCommands(e.Property);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            this.BorderRenderer.Render(drawingContext);
        }

        protected virtual void OnShowBorderChanged()
        {
            this.UpdateActualBorderTemplate();
            if (this.MarginCorrector != null)
            {
                this.MarginCorrector.ShowBorder = this.ShowBorder;
            }
        }

        protected static void OnShowBorderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnShowBorderChanged();
        }

        protected virtual void OnShowErrorChanged()
        {
            this.UpdateErrorPresenter();
            if (this.MarginCorrector != null)
            {
                this.MarginCorrector.ShowError = this.ShowError;
            }
        }

        protected static void OnShowErrorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnShowErrorChanged();
        }

        protected virtual void OnShowErrorToolTipChanged()
        {
        }

        protected static void OnShowErrorToolTipChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnShowErrorToolTipChanged();
        }

        protected virtual void OnShowNullTextChanged(bool show)
        {
            this.PropertyProvider.SetShowNullText(show);
            this.EditStrategy.UpdateDisplayText();
        }

        protected virtual void OnShowNullTextForEmptyValueChanged(bool show)
        {
            this.PropertyProvider.SetShowNullTextForEmptyValue(show);
            this.EditStrategy.UpdateDisplayText();
        }

        protected static void OnShowNullTextPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnShowNullTextChanged((bool) e.NewValue);
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            this.EditStrategy.ThemeChanged();
        }

        private static void OnToolTipPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.OnUnloadedInternal();
            ThemeManager.RemoveThemeChangedHandler(this, new ThemeChangedRoutedEventHandler(this.OnThemeChanged));
        }

        private void OnUnloadedInternal()
        {
            this.EditStrategy.OnUnloaded();
        }

        protected virtual void OnValidationErrorChanged(BaseValidationError error)
        {
            this.UpdateErrorPresenter();
            this.UpdateValidationService(error);
        }

        protected static void OnValidationErrorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            SetHasValidationError(obj, e.NewValue != null);
            BaseEdit edit = obj as BaseEdit;
            if (edit != null)
            {
                edit.OnValidationErrorChanged((BaseValidationError) e.NewValue);
            }
        }

        private void OnValidationErrorTemplateChanged(DataTemplate newValue)
        {
            this.PropertyProvider.SetHasValidationErrorTemplate(newValue != null);
        }

        protected virtual void OnValidationModeChanged()
        {
        }

        protected static void OnValidationModeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseEdit) obj).OnValidationModeChanged();
        }

        protected internal void ProcessActivatingKey(Key key, ModifierKeys modifiers)
        {
            ProcessActivatingKeyEventArgs e = new ProcessActivatingKeyEventArgs(key, modifiers, this);
            this.Settings.RaiseProcessActivatingKey(this, e);
            if (!e.IsProcessed)
            {
                this.ProcessActivatingKeyCore(key, modifiers);
            }
        }

        protected virtual void ProcessActivatingKeyCore(Key key, ModifierKeys modifiers)
        {
        }

        protected virtual void ProcessFocusEditCore(MouseButtonEventArgs e)
        {
            this.FocusEditCore();
        }

        protected virtual CustomDisplayTextEventArgs RaiseCustomDisplayText(object editValue, string displayText)
        {
            CustomDisplayTextEventArgs e = new CustomDisplayTextEventArgs(CustomDisplayTextEvent) {
                EditValue = editValue,
                DisplayText = displayText
            };
            base.RaiseEvent(e);
            return e;
        }

        private void RefreshErrorFromBinding(IEnumerable<System.Windows.Controls.ValidationError> newValue)
        {
            System.Windows.Controls.ValidationError error = (newValue != null) ? newValue.FirstOrDefault<System.Windows.Controls.ValidationError>() : null;
            this.ErrorFromBinding = (error == null) ? null : new BaseValidationError(error.ErrorContent, error.Exception, ErrorType.Critical);
            this.EditStrategy.ResetErrorProvider();
        }

        private void RemoveLogicalChildInternal(object child)
        {
            this.EditStrategy.RemoveILogicalOwnerChild(child);
            base.RemoveLogicalChild(child);
            this.logicalChildren.Remove(child);
        }

        public virtual void SelectAll()
        {
        }

        internal void SetActualPropertyProvider(ActualPropertyProvider provider)
        {
            this.CachedPropertyProvider = provider;
        }

        internal void SetDisplayTextProvider(IDisplayTextProvider displayTextProvider)
        {
            this.displayTextProvider = displayTextProvider;
        }

        private static void SetHasValidationError(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(HasValidationErrorPropertyKey, value);
        }

        protected virtual void SetNullValueInternal(object parameter)
        {
            this.EditStrategy.SetNullValue(parameter);
        }

        internal static void SetOwnerEdit(DependencyObject element, BaseEdit value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(OwnerEditPropertyKey, value);
        }

        protected internal void SetSettings(BaseEditSettings settings)
        {
            this.UnsubscribeFromSettings(this.settings);
            this.settings = settings;
            this.SubscribeToSettings(settings);
        }

        protected internal virtual void SetShowEditorButtons(bool show)
        {
            this.showEditButtons = show;
        }

        internal void SetTotalDisplayTextProvider(IDisplayTextProvider displayTextProvider)
        {
            this.totalDisplayTextProvider = displayTextProvider;
        }

        internal static void SetValidationError(DependencyObject element, BaseValidationError value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(ValidationErrorPropertyKey, value);
        }

        internal static void SetValidationErrorTemplate(DependencyObject element, DataTemplate template)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(ValidationErrorTemplateProperty, template);
        }

        protected virtual void ShouldDisableExcessiveUpdatesInInplaceInactiveModeChanged(bool newValue)
        {
            this.PropertyProvider.SetShouldDisableExcessiveUpdatesInInplaceInactiveMode(newValue);
            if (!this.PropertyProvider.SuppressFeatures)
            {
                this.EditStrategy.UpdateEditorOnEditingChange(true);
            }
        }

        private bool SkipLayoutInInplaceMode(UIElement elem) => 
            ReferenceEquals(elem, this.borderControl) || ReferenceEquals(elem, this.glowControl);

        protected virtual void StyleSettingsChanged(BaseEditStyleSettings settings)
        {
            this.EditStrategy.StyleSettingsChanged(settings);
        }

        private void StyleSettingsChangedInternal(BaseEditStyleSettings settings)
        {
            this.PropertyProvider.SetStyleSettings(settings);
            this.CheckStyleSettings(settings);
            this.StyleSettingsChanged(settings);
        }

        protected virtual void SubscribeEditEventsCore()
        {
        }

        protected virtual void SubscribeToSettings(BaseEditSettings settings)
        {
            if (settings != null)
            {
                settings.EditSettingsChanged += this.EditSettingsChangedEventHandler.Handler;
            }
            this.PropertyProvider.CreatedFromSettings = true;
        }

        private void SvgStateChanged(string newValue)
        {
            this.UpdateErrorPresenter();
        }

        protected virtual void SyncEditCoreProperties()
        {
            this.EditStrategy.SyncEditCoreProperties();
        }

        protected virtual void UnsubscribeEditEventsCore()
        {
        }

        protected virtual void UnsubscribeFromSettings(BaseEditSettings settings)
        {
            if (settings != null)
            {
                settings.EditSettingsChanged -= this.EditSettingsChangedEventHandler.Handler;
            }
            this.PropertyProvider.CreatedFromSettings = true;
        }

        protected void UpdateActualBorderTemplate()
        {
            this.ActualBorderTemplate = this.GetActualBorderTemplate();
        }

        protected void UpdateActualEditorControlTemplate()
        {
            this.ActualEditorControlTemplate = this.GetActualEditorControlTemplate();
        }

        private void UpdateBorderCore(ref Control field, Action<Control, BaseEdit, InplaceResourceProvider> postInitializeAction, int key)
        {
            if (!this.ShowBorderInInplaceMode || !this.ShowBorder)
            {
                this.borderThickness = new Thickness();
                if (field != null)
                {
                    this.additionalInplaceModeElements.Remove(key);
                    base.RemoveVisualChild(field);
                    field = null;
                    base.InvalidateMeasure();
                }
            }
            else
            {
                if (field == null)
                {
                    Control control1 = new Control();
                    control1.HorizontalAlignment = HorizontalAlignment.Stretch;
                    control1.VerticalAlignment = VerticalAlignment.Stretch;
                    control1.Focusable = false;
                    field = control1;
                    this.additionalInplaceModeElements.Add(key, field);
                    base.AddVisualChild(field);
                    base.InvalidateMeasure();
                }
                InplaceResourceProvider resourceProvider = ThemeHelper.GetResourceProvider(this);
                postInitializeAction(field, this, resourceProvider);
            }
        }

        protected internal virtual void UpdateBorderInInplaceMode()
        {
            this.UpdateBorderCore(ref this.borderControl, delegate (Control border, BaseEdit baseEdit, InplaceResourceProvider provider) {
                if (this.UsesFlatBorderTemplate())
                {
                    border.Template = provider.GetCommonBorderDecorationTemplate(this);
                    baseEdit.borderThickness = provider.GetCommonBorderThickness(this);
                }
                else
                {
                    border.Template = provider.GetTextEditBorderDecorationTemplate(this);
                    baseEdit.borderThickness = provider.GetTextEditBorderThickness(this);
                }
            }, 0);
        }

        internal virtual void UpdateButtonPanelsInplaceMode()
        {
        }

        protected void UpdateCommand(ICommand command)
        {
            DelegateCommand<object> command2 = command as DelegateCommand<object>;
            if (command2 != null)
            {
                command2.RaiseCanExecuteChanged();
            }
        }

        protected virtual void UpdateCommands(DependencyProperty property)
        {
        }

        private void UpdateContentManagementStrategy()
        {
            if (this.EditMode != DevExpress.Xpf.Editors.EditMode.Standalone)
            {
                this.ContentManagementStrategy = new InplaceContentManagementStrategy(this);
            }
            else
            {
                this.ContentManagementStrategy = new StandaloneContentManagementStrategy(this);
            }
        }

        protected internal virtual void UpdateDataContext(DependencyObject target)
        {
            this.EditStrategy.UpdateDataContext(target);
        }

        protected virtual void UpdateErrorPresenter()
        {
            this.ContentManagementStrategy.UpdateErrorPresenter();
        }

        private void UpdateErrorPresenterState(ContentControl errorPresenter)
        {
            SvgImageHelper.SetState(errorPresenter, SvgImageHelper.GetState(this) + (this.CalcActualShowBorder() ? "_WithBorder" : "_WithoutBorder"));
        }

        protected internal virtual void UpdateGlowInInplaceMode()
        {
            this.UpdateBorderCore(ref this.glowControl, delegate (Control border, BaseEdit baseEdit, InplaceResourceProvider provider) {
                border.Template = provider.GetHoverBorderDecorationTemplate(this);
                border.IsHitTestVisible = false;
                Binding binding = new Binding();
                binding.Source = baseEdit;
                binding.Path = new PropertyPath(UIElement.IsKeyboardFocusWithinProperty);
                BindingOperations.SetBinding(border, ControlHelper.ShowFocusedStateProperty, binding);
            }, 4);
        }

        internal void UpdateInplaceErrorPresenter()
        {
            if (!this.HasValidationError)
            {
                if (this.ErrorPresenterInplace != null)
                {
                    base.RemoveVisualChild(this.ErrorPresenterInplace);
                    this.additionalInplaceModeElements.Remove(1);
                    this.ErrorPresenterInplace = null;
                    base.InvalidateMeasure();
                }
            }
            else
            {
                if (this.ErrorPresenterInplace == null)
                {
                    this.ErrorPresenterInplace = new ErrorControl();
                    DataObjectBase.SetNeedsResetEvent(this.ErrorPresenterInplace, true);
                    base.AddVisualChild(this.ErrorPresenterInplace);
                    this.additionalInplaceModeElements.Add(1, this.ErrorPresenterInplace);
                    base.InvalidateMeasure();
                }
                this.ErrorPresenterInplace.Content = this.ValidationError;
                this.UpdateErrorPresenterState(this.ErrorPresenterInplace);
            }
            this.EditStrategy.ResetErrorProvider();
        }

        private void UpdateIsEditorActiveProperty(DevExpress.Xpf.Editors.EditMode editMode)
        {
            if (Equals(this.GetEditTemplate(), this.GetDisplayTemplate()))
            {
                this.IsEditorActive = editMode != DevExpress.Xpf.Editors.EditMode.InplaceInactive;
            }
        }

        private void UpdateIsEditorActivePropertyForce(DevExpress.Xpf.Editors.EditMode editMode)
        {
            this.IsEditorActive = editMode != DevExpress.Xpf.Editors.EditMode.InplaceInactive;
        }

        protected internal void UpdateStandaloneErrorPresenter()
        {
            if (this.ErrorPresenterStandalone != null)
            {
                this.ErrorPresenterStandalone.Content = this.ValidationError;
                this.ErrorPresenterStandalone.Visibility = (!this.HasValidationError || !this.ShowError) ? Visibility.Collapsed : Visibility.Visible;
                this.UpdateErrorPresenterState(this.ErrorPresenterStandalone);
            }
        }

        private void UpdateValidationService(BaseValidationError error)
        {
            ValidationService validationService = ValidationService.GetValidationService(this);
            if (validationService != null)
            {
                if (error != null)
                {
                    validationService.UpdateEditor(this);
                }
                else
                {
                    validationService.RemoveEditor(this);
                }
            }
        }

        protected virtual void UpdateVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, this.GetStateName(), useTransitions);
        }

        protected bool UsesFlatBorderTemplate() => 
            this.Settings.UsesFlatBorderTemplate();

        private void ValidationErrorChanged(IEnumerable<System.Windows.Controls.ValidationError> oldValue, IEnumerable<System.Windows.Controls.ValidationError> newValue)
        {
            INotifyCollectionChanged changed = oldValue as INotifyCollectionChanged;
            if (changed != null)
            {
                changed.CollectionChanged -= this.ValidationErrorHandler.Handler;
            }
            INotifyCollectionChanged changed2 = newValue as INotifyCollectionChanged;
            if (changed2 != null)
            {
                changed2.CollectionChanged += this.ValidationErrorHandler.Handler;
            }
            this.RefreshErrorFromBinding(newValue);
        }

        private void ValidationErrorChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RefreshErrorFromBinding((IEnumerable<System.Windows.Controls.ValidationError>) sender);
        }

        internal bool IsInSupportInitializing =>
            this.supportInitializeLocker.IsLocked;

        [Description("Gets whether the editor is active.")]
        public bool IsEditorActive
        {
            get => 
                (bool) base.GetValue(IsEditorActiveProperty);
            protected set => 
                base.SetValue(IsEditorActivePropertyKey, value);
        }

        private WeakEventHandler<BaseEdit, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> ValidationErrorHandler { get; }

        internal BaseValidationError ErrorFromBinding { get; private set; }

        [Description("")]
        internal EditStrategyBase EditStrategy { get; set; }

        internal ContentManagementStrategyBase ContentManagementStrategy { get; private set; }

        internal ContentControl ErrorPresenterInplace { get; set; }

        internal ContentControl ErrorPresenterStandalone { get; set; }

        [Description("Provides access to an internal editor control.")]
        public FrameworkElement EditCore
        {
            get => 
                this.editCore;
            set
            {
                if (!ReferenceEquals(this.EditCore, value))
                {
                    if (this.EditCore != null)
                    {
                        this.UnsubscribeEditEventsCore();
                    }
                    this.editCore = value;
                    if (this.EditCore != null)
                    {
                        this.SubscribeEditEventsCore();
                        this.SyncEditCoreProperties();
                    }
                    this.OnEditCoreAssigned();
                }
            }
        }

        internal bool IsValueChanged
        {
            get => 
                this.isValueChanged || this.EditStrategy.IsValueChanged;
            set
            {
                this.isValueChanged = value;
                if (!value)
                {
                    this.EditStrategy.IsValueChanged = false;
                }
            }
        }

        protected internal BaseEditSettings Settings
        {
            get
            {
                this.settings ??= this.CreateEditorSettings();
                return this.settings;
            }
        }

        protected virtual bool IsInactiveMode =>
            this.EditMode == DevExpress.Xpf.Editors.EditMode.InplaceInactive;

        protected virtual bool IsInplaceMode =>
            this.EditMode != DevExpress.Xpf.Editors.EditMode.Standalone;

        public bool AllowUpdateTwoWayBoundPropertiesOnSynchronization
        {
            get => 
                (bool) base.GetValue(AllowUpdateTwoWayBoundPropertiesOnSynchronizationProperty);
            set => 
                base.SetValue(AllowUpdateTwoWayBoundPropertiesOnSynchronizationProperty, value);
        }

        [Description("Gets or sets whether end-users can set the editor's value to a null reference by pressing the CTRL+DEL or CTRL+0 key combinations. This is a dependency property."), Category("Behavior")]
        public bool AllowNullInput
        {
            get => 
                (bool) base.GetValue(AllowNullInputProperty);
            set => 
                base.SetValue(AllowNullInputProperty, value);
        }

        [Description("Gets whether the null text is displayed within the editor. This is a dependency property.")]
        public bool IsNullTextVisible
        {
            get => 
                (bool) base.GetValue(IsNullTextVisibleProperty);
            internal set => 
                base.SetValue(IsNullTextVisiblePropertyKey, value);
        }

        [Description("Gets or sets whether to display the null text. This is a dependency property.")]
        public bool ShowNullText
        {
            get => 
                (bool) base.GetValue(ShowNullTextProperty);
            set => 
                base.SetValue(ShowNullTextProperty, value);
        }

        [Description("Gets or sets whether to display the null text for the String.Empty value. This is a dependency property.")]
        public bool ShowNullTextForEmptyValue
        {
            get => 
                (bool) base.GetValue(ShowNullTextForEmptyValueProperty);
            set => 
                base.SetValue(ShowNullTextForEmptyValueProperty, value);
        }

        [Description("Gets or sets the text displayed within the edit box when the editor's value is null. This is a dependency property.")]
        public string NullText
        {
            get => 
                (string) base.GetValue(NullTextProperty);
            set => 
                base.SetValue(NullTextProperty, value);
        }

        [Description("Gets or sets a null value for the editor. This is a dependency property.")]
        public object NullValue
        {
            get => 
                base.GetValue(NullValueProperty);
            set => 
                base.SetValue(NullValueProperty, value);
        }

        [Description("Gets the editor's actual template. This is a dependency property.")]
        public ControlTemplate ActualEditorControlTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(ActualEditorControlTemplateProperty);
            protected internal set => 
                base.SetValue(ActualEditorControlTemplatePropertyKey, value);
        }

        [Description("Gets or sets whether the editor's value can be changed by end-users. This is a dependency property."), Category("Behavior")]
        public bool IsReadOnly
        {
            get => 
                (bool) base.GetValue(IsReadOnlyProperty);
            set => 
                base.SetValue(IsReadOnlyProperty, value);
        }

        [Description("Gets or sets a template that defines the editor's presentation when its value is not edited (display mode). This is a dependency property."), Browsable(false)]
        public ControlTemplate DisplayTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(DisplayTemplateProperty);
            set => 
                base.SetValue(DisplayTemplateProperty, value);
        }

        [Description("Gets or sets a template that defines the editor's presentation in edit mode. This is a dependency property."), Browsable(false)]
        public ControlTemplate EditTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(EditTemplateProperty);
            set => 
                base.SetValue(EditTemplateProperty, value);
        }

        [Description("Gets or sets a template that presents the content of an editor's error tooltip. This is a dependency property.")]
        public DataTemplate ErrorToolTipContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ErrorToolTipContentTemplateProperty);
            set => 
                base.SetValue(ErrorToolTipContentTemplateProperty, value);
        }

        [Description("Gets or sets the data template used to display the content of a tooltip invoked for the editor whose text is trimmed.This is a dependency property.")]
        public DataTemplate TrimmedTextToolTipContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(TrimmedTextToolTipContentTemplateProperty);
            set => 
                base.SetValue(TrimmedTextToolTipContentTemplateProperty, value);
        }

        [Description("Gets or sets whether the edit value should be validated when pressing the ENTER key. This is a dependency property."), Category("Behavior")]
        public bool ValidateOnEnterKeyPressed
        {
            get => 
                (bool) base.GetValue(ValidateOnEnterKeyPressedProperty);
            set => 
                base.SetValue(ValidateOnEnterKeyPressedProperty, value);
        }

        [Description("Gets or sets whether the edit value should be validated while typing within the editor's text box. This is a dependency property."), Category("Behavior")]
        public bool ValidateOnTextInput
        {
            get => 
                (bool) base.GetValue(ValidateOnTextInputProperty);
            set => 
                base.SetValue(ValidateOnTextInputProperty, value);
        }

        [Description("Gets or sets a value indicating whether validation is enabled for this editor.This is a dependency property."), Category("Behavior")]
        public bool CausesValidation
        {
            get => 
                (bool) base.GetValue(CausesValidationProperty);
            set => 
                base.SetValue(CausesValidationProperty, value);
        }

        [Description("Gets or sets the editor's value. This is a dependency property."), TypeConverter(typeof(ObjectConverter)), Category("Common Properties")]
        public object EditValue
        {
            get => 
                base.GetValue(EditValueProperty);
            set => 
                base.SetValue(EditValueProperty, value);
        }

        [Browsable(false), Description("For internal use only.")]
        public DevExpress.Xpf.Editors.EditMode EditMode
        {
            get => 
                (DevExpress.Xpf.Editors.EditMode) base.GetValue(EditModeProperty);
            set => 
                base.SetValue(EditModeProperty, value);
        }

        [Description("Gets whether an editor has a validation error. This is a dependency property.")]
        public bool HasValidationError
        {
            get => 
                (bool) base.GetValue(HasValidationErrorProperty);
            internal set => 
                base.SetValue(HasValidationErrorPropertyKey, value);
        }

        [Description("Gets or sets a template used to display the editor's border. This is a dependency property."), Browsable(false)]
        public ControlTemplate BorderTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(BorderTemplateProperty);
            set => 
                base.SetValue(BorderTemplateProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets a value that specifies the editor's response to an invalid value. This is a dependency property.")]
        public DevExpress.Xpf.Editors.Validation.InvalidValueBehavior InvalidValueBehavior
        {
            get => 
                (DevExpress.Xpf.Editors.Validation.InvalidValueBehavior) base.GetValue(InvalidValueBehaviorProperty);
            set => 
                base.SetValue(InvalidValueBehaviorProperty, value);
        }

        [Category("Appearance"), Description("Gets or sets whether the editor's background is displayed. This is a dependency property.")]
        public bool ShowBorder
        {
            get => 
                (bool) base.GetValue(ShowBorderProperty);
            set => 
                base.SetValue(ShowBorderProperty, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShowBorderInInplaceMode
        {
            get => 
                (bool) base.GetValue(ShowBorderInInplaceModeProperty);
            set => 
                base.SetValue(ShowBorderInInplaceModeProperty, value);
        }

        [Description("Gets or sets whether an error icon is displayed in the editor containing an invalid value. This is a dependency property."), Category("Behavior")]
        public bool ShowError
        {
            get => 
                (bool) base.GetValue(ShowErrorProperty);
            set => 
                base.SetValue(ShowErrorProperty, value);
        }

        [Description("Gets or sets whether a tooltip with error message is shown when the mouse pointer is over an error icon. This is a dependency property."), Category("Behavior")]
        public bool ShowErrorToolTip
        {
            get => 
                (bool) base.GetValue(ShowErrorToolTipProperty);
            set => 
                base.SetValue(ShowErrorToolTipProperty, value);
        }

        [Browsable(false)]
        public BaseEditStyleSettings StyleSettings
        {
            get => 
                (BaseEditStyleSettings) base.GetValue(StyleSettingsProperty);
            set => 
                base.SetValue(StyleSettingsProperty, value);
        }

        [Description("Gets or sets whether the editor is in printing mode."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPrintingMode
        {
            get => 
                (bool) base.GetValue(IsPrintingModeProperty);
            set => 
                base.SetValue(IsPrintingModeProperty, value);
        }

        [Description("Gets the validation error defined for the editor. This is a dependency property.")]
        public BaseValidationError ValidationError
        {
            get => 
                GetValidationError(this);
            internal set => 
                SetValidationError(this, value);
        }

        public int EditValuePostDelay
        {
            get => 
                (int) base.GetValue(EditValuePostDelayProperty);
            set => 
                base.SetValue(EditValuePostDelayProperty, value);
        }

        public PostMode EditValuePostMode
        {
            get => 
                (PostMode) base.GetValue(EditValuePostModeProperty);
            set => 
                base.SetValue(EditValuePostModeProperty, value);
        }

        public Type EditValueType
        {
            get => 
                (Type) base.GetValue(EditValueTypeProperty);
            set => 
                base.SetValue(EditValueTypeProperty, value);
        }

        [Obsolete("Use BindingBase.Converter property instead")]
        public IValueConverter EditValueConverter
        {
            get => 
                (IValueConverter) base.GetValue(EditValueConverterProperty);
            set => 
                base.SetValue(EditValueConverterProperty, value);
        }

        [Obsolete("Use BindingBase.Converter property instead")]
        public IValueConverter InputTextToEditValueConverter
        {
            get => 
                (IValueConverter) base.GetValue(InputTextToEditValueConverterProperty);
            set => 
                base.SetValue(InputTextToEditValueConverterProperty, value);
        }

        [Description("Gets the text displayed within the editor. This is a dependency property.")]
        public string DisplayText
        {
            get => 
                (string) base.GetValue(DisplayTextProperty);
            internal set => 
                base.SetValue(DisplayTextPropertyKey, value);
        }

        [Description("Gets or sets a converter used to provide the editor's display value. This is a dependency property.")]
        public IValueConverter DisplayTextConverter
        {
            get => 
                (IValueConverter) base.GetValue(DisplayTextConverterProperty);
            set => 
                base.SetValue(DisplayTextConverterProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets the pattern used to format the editor's value for display purposes. This is a dependency property.")]
        public string DisplayFormatString
        {
            get => 
                (string) base.GetValue(DisplayFormatStringProperty);
            set => 
                base.SetValue(DisplayFormatStringProperty, value);
        }

        [Description("Gets the actual border template. This is a dependency property.")]
        public ControlTemplate ActualBorderTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(ActualBorderTemplateProperty);
            protected set => 
                base.SetValue(ActualBorderTemplatePropertyKey, value);
        }

        public bool? DisableExcessiveUpdatesInInplaceInactiveMode
        {
            get => 
                (bool?) base.GetValue(DisableExcessiveUpdatesInInplaceInactiveModeProperty);
            set => 
                base.SetValue(DisableExcessiveUpdatesInInplaceInactiveModeProperty, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool ShouldDisableExcessiveUpdatesInInplaceInactiveMode
        {
            get => 
                (bool) base.GetValue(ShouldDisableExcessiveUpdatesInInplaceInactiveModeProperty);
            internal set => 
                base.SetValue(ShouldDisableExcessiveUpdatesInInplaceInactiveModePropertyKey, value);
        }

        [Description("A command that sets the editor's value to a null value.")]
        public ICommand SetNullValueCommand { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool AllowUpdateTextBlockWhenPrinting
        {
            get => 
                (bool) base.GetValue(AllowUpdateTextBlockWhenPrintingProperty);
            set => 
                base.SetValue(AllowUpdateTextBlockWhenPrintingProperty, value);
        }

        public DataTemplate ValidationErrorTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ValidationErrorTemplateProperty);
            set => 
                base.SetValue(ValidationErrorTemplateProperty, value);
        }

        internal EditorFocusManagement FocusManagement { get; }

        protected internal DevExpress.Xpf.Editors.BorderRenderer BorderRenderer { get; }

        private ActualPropertyProvider CachedPropertyProvider { get; set; }

        private EditSettingsChangedEventHandler<BaseEdit> EditSettingsChangedEventHandler { get; }

        private PostponedAction SetupFocusAction { get; }

        internal IInplaceEditingProvider InplaceEditing { get; set; }

        internal DevExpress.Xpf.Editors.ImmediateActionsManager ImmediateActionsManager { get; }

        private bool HasNullText =>
            !string.IsNullOrEmpty(this.NullText);

        protected internal ActualPropertyProvider PropertyProvider =>
            this.CachedPropertyProvider;

        protected internal virtual Type StyleSettingsType =>
            typeof(EditStyleSettings);

        protected internal virtual bool IsEditorKeyboardFocused =>
            base.IsKeyboardFocusWithin;

        protected override int VisualChildrenCount =>
            this.ContentManagementStrategy.VisualChildrenCount;

        internal int VisualChildrenCountStandaloneMode =>
            base.VisualChildrenCount;

        internal int VisualChildrenCountInplaceMode =>
            base.VisualChildrenCount + this.additionalInplaceModeElements.Count;

        bool IBaseEdit.ShowEditorButtons
        {
            get => 
                this.GetShowEditorButtons();
            set => 
                this.SetShowEditorButtons(value);
        }

        internal bool CanAcceptFocus { get; set; }

        bool IBaseEdit.ShouldDisableExcessiveUpdatesInInplaceInactiveMode
        {
            get => 
                this.ShouldDisableExcessiveUpdatesInInplaceInactiveMode;
            set => 
                this.ShouldDisableExcessiveUpdatesInInplaceInactiveMode = value;
        }

        BaseEditSettings IBaseEdit.Settings =>
            this.Settings;

        bool IBaseEdit.CanAcceptFocus
        {
            get => 
                this.CanAcceptFocus;
            set => 
                this.CanAcceptFocus = value;
        }

        bool IBaseEdit.IsValueChanged
        {
            get => 
                this.IsValueChanged;
            set => 
                this.IsValueChanged = value;
        }

        BaseValidationError IBaseEdit.ValidationError
        {
            get => 
                this.ValidationError;
            set => 
                this.ValidationError = value;
        }

        protected override IEnumerator LogicalChildren =>
            this.logicalChildren.GetEnumerator();

        Color IExportSettings.Background =>
            (base.Background == null) ? ExportSettingDefaultValue.Background : ((Color) base.Background.GetValue(SolidColorBrush.ColorProperty));

        Color IExportSettings.Foreground =>
            (base.Foreground == null) ? ExportSettingDefaultValue.Foreground : ((Color) base.Foreground.GetValue(SolidColorBrush.ColorProperty));

        Color IExportSettings.BorderColor =>
            (base.BorderBrush == null) ? ExportSettingDefaultValue.BorderColor : ((Color) base.BorderBrush.GetValue(SolidColorBrush.ColorProperty));

        Thickness IExportSettings.BorderThickness =>
            base.BorderThickness;

        BorderDashStyle IExportSettings.BorderDashStyle =>
            ExportSettingDefaultValue.BorderDashStyle;

        string IExportSettings.Url =>
            ExportSettingDefaultValue.Url;

        IOnPageUpdater IExportSettings.OnPageUpdater =>
            null;

        object IExportSettings.MergeValue =>
            null;

        FlowDirection IExportSettings.FlowDirection =>
            base.FlowDirection;

        internal EditorMarginCorrector MarginCorrector
        {
            get => 
                ((this.marginCorrector == null) || !this.marginCorrector.IsAlive) ? null : ((EditorMarginCorrector) this.marginCorrector.Target);
            set => 
                this.marginCorrector = (value != null) ? new WeakReference(value) : null;
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

        bool ILogicalOwner.IsLoaded =>
            base.IsLoaded;

        double ILogicalOwner.ActualWidth =>
            base.ActualWidth;

        double ILogicalOwner.ActualHeight =>
            base.ActualHeight;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseEdit.<>c <>9 = new BaseEdit.<>c();
            public static Action<BaseEdit, object, NotifyCollectionChangedEventArgs> <>9__325_0;
            public static Action<WeakEventHandler<BaseEdit, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler>, object> <>9__325_1;
            public static Func<WeakEventHandler<BaseEdit, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler>, NotifyCollectionChangedEventHandler> <>9__325_2;
            public static Action<BaseEdit, object, EventArgs> <>9__325_3;

            internal void <.cctor>b__57_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).EditValueTypeChanged((Type) e.NewValue);
            }

            internal void <.cctor>b__57_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).EditValueConverterChanged((IValueConverter) e.NewValue);
            }

            internal object <.cctor>b__57_10(DependencyObject d, object e) => 
                ((BaseEdit) d).OnIsTabStopCoerceValue(e);

            internal void <.cctor>b__57_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).StyleSettingsChangedInternal((BaseEditStyleSettings) e.NewValue);
            }

            internal void <.cctor>b__57_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).OnValidationErrorTemplateChanged((DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__57_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).SvgStateChanged((string) e.NewValue);
            }

            internal void <.cctor>b__57_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).InputTextToEditValueConverterChanged((IValueConverter) e.NewValue);
            }

            internal void <.cctor>b__57_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).EditValuePostDelayChanged((int) e.NewValue);
            }

            internal void <.cctor>b__57_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).EditValuePostModeChanged((PostMode) e.NewValue);
            }

            internal void <.cctor>b__57_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).ShouldDisableExcessiveUpdatesInInplaceInactiveModeChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__57_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).DisableExcessiveUpdatesInInplaceInactiveModeChanged((bool?) e.NewValue);
            }

            internal void <.cctor>b__57_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).OnShowNullTextForEmptyValueChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__57_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).ValidationErrorChanged((IEnumerable<ValidationError>) e.OldValue, (IEnumerable<ValidationError>) e.NewValue);
            }

            internal void <.cctor>b__57_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseEdit) d).OnIsTabStopPropertyChanged(e);
            }

            internal void <.ctor>b__325_0(BaseEdit x, object sender, NotifyCollectionChangedEventArgs args)
            {
                x.ValidationErrorChanged(sender, args);
            }

            internal void <.ctor>b__325_1(WeakEventHandler<BaseEdit, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> h, object sender)
            {
                ((INotifyCollectionChanged) sender).CollectionChanged -= h.Handler;
            }

            internal NotifyCollectionChangedEventHandler <.ctor>b__325_2(WeakEventHandler<BaseEdit, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> h) => 
                new NotifyCollectionChangedEventHandler(h.OnEvent);

            internal void <.ctor>b__325_3(BaseEdit owner, object o, EventArgs e)
            {
                owner.OnEditSettingsChanged();
            }
        }
    }
}

